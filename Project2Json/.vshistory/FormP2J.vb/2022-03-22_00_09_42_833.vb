Option Explicit On                          ' Declare everything
Imports System.Data.SqlClient               ' SQL handling
Imports System.Text.RegularExpressions      ' Regular Expressions

' my first brnach change

' General Comments
' ================
' Determine which .Net Framework versions are on the machine
' Get-ChildItem 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP' -recurse | Get-ItemProperty -name Version,Release -EA 0 | Where { $_.PSChildName -match '^(?!S)\p{L}'} | Select Case PSChildName, Version, Release

' SQL Express 2016 LocalDB (from Visual Studio) brings: (localdb)\MSSQLLocalDB as default instance
' need to launch the db: sqllocaldb start MSSQLLocalDB

' VB Basic: Always Option Base 0 => All Arrays zero based


Public Class FormP2J
    Inherits Form
    Dim myGotProjects As Boolean
    Dim myRED As Color
    Dim myGreen As Color

    Private Sub FormP2J_Load()
        MyTimer.Interval = 200       ' 200 milliseconds for first call
        MyTimer.Start()              ' Timer starts functioning
        myGotProjects = False
        myRED = Color.FromArgb(255, 50, 50)
        myGreen = Color.FromArgb(50, 255, 50)
    End Sub

    Private Function GetProjects() As Boolean
        ' This gets the projects from the database, it will be run once after the connection was established
        ' Create ADO.NET objects.
        Dim myCon As SqlConnection
        Dim myCmd As SqlCommand
        Dim myReader As SqlDataReader

        Dim myCounter As Integer

        ' Create a Connection object.
        'myCon = New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True")
        myCon = New SqlConnection("Data Source=RKAMVGEMSDBP\TEAMMATE;Database=TeamMate_PROD;Integrated Security=True")

        ' Open the connection.
        myCon.Open()

        ' Create a Command object.
        myCmd = myCon.CreateCommand
        'myCmd.CommandText = "SELECT ID, FirstName, LastName FROM Persons"
        'myCmd.CommandText = "SELECT dbo.NG_Project.ID AS P_ID, dbo.NG_Project.Title AS P_Title, dbo.NG_ProjectPhaseDefinition.Title AS P_Phase FROM (dbo.NG_Project LEFT JOIN dbo.NG_ProjectPhaseCurrent ON dbo.NG_Project.ID = dbo.NG_ProjectPhaseCurrent.ProjectID) LEFT JOIN (dbo.NG_ProjectPhase LEFT JOIN dbo.NG_ProjectPhaseDefinition ON dbo.NG_ProjectPhase.ProjectPhaseDefID = dbo.NG_ProjectPhaseDefinition.ID) ON dbo.NG_ProjectPhaseCurrent.ProjectPhaseID = dbo.NG_ProjectPhase.ID WHERE ((dbo.NG_ProjectPhaseDefinition.Title='Planned') OR (dbo.NG_ProjectPhaseDefinition.Title='On-going'));"
        myCmd.CommandText = "SELECT dbo.NG_Project.ID AS P_ID, dbo.NG_Project.Title AS P_Title, dbo.NG_ProjectPhaseDefinition.Title AS P_Phase FROM (dbo.NG_Project LEFT JOIN dbo.NG_ProjectPhaseCurrent ON dbo.NG_Project.ID = dbo.NG_ProjectPhaseCurrent.ProjectID) LEFT JOIN (dbo.NG_ProjectPhase LEFT JOIN dbo.NG_ProjectPhaseDefinition ON dbo.NG_ProjectPhase.ProjectPhaseDefID = dbo.NG_ProjectPhaseDefinition.ID) ON dbo.NG_ProjectPhaseCurrent.ProjectPhaseID = dbo.NG_ProjectPhase.ID WHERE ((dbo.NG_ProjectPhaseDefinition.Title='Planned') OR (dbo.NG_ProjectPhaseDefinition.Title='On-going') OR (dbo.NG_ProjectPhaseDefinition.Title='Finalised')) ORDER BY dbo.NG_Project.Title ASC;"
        ' Get the data in a reader
        myReader = myCmd.ExecuteReader()

        ' Add the query result to the ListBox.        
        myCounter = 0
        Do While myReader.Read() And myCounter < 500
            ListBoxProjects.Items.Add(myReader.GetInt32(0) & "-" & myReader.GetString(1) & "-" & myReader.GetString(2))
            myCounter += 1
        Loop

        myCon.Close()
        myGotProjects = True    ' Set flag to prevent repeated call, only get Projects one time
        Return myGotProjects
    End Function

    Private Function GenerateJson() As String
        Dim myCon As String
        Dim mySql As String
        'Dim mySql2 As String
        Dim myP_ID As String
        Dim myP_Title As String
        Dim myText As String
        Dim myFieldName As String
        Dim myRow As String
        Dim myRows(500) As String
        Dim myRowsCount As Integer
        Dim myCount As Integer
        Dim myImgCount As Integer
        Dim mySavePath As String

        GenerateJson = ""
        mySavePath = "G:\Shared drives\GA&RA GA Team\WX_Work in Progress\JSONs"

        ' One item is selected, get it P_ID
        myP_ID = ListBoxProjects.SelectedItem.ToString.Split("-")(0)
        myP_Title = ListBoxProjects.SelectedItem.ToString.Split("-")(1)
        myP_Title = Regex.Replace(myP_Title, "\.|:|\\|\/|<|>|\||\?|\*|""", "_")      ' Make sure title can be used on filesystem, remove invalid characters

        ' Select the target folder for json now
        ' RS: #TODO: Potentially need to check for exceptions
        ' Check for G:
        Try
            If IO.Directory.Exists("G:\") Then
                ' Create Data Table to hold the data
                Using myDataTable As New DataTable
                    ' Create a Connection object.
                    myCon = "Data Source=RKAMVGEMSDBP\TEAMMATE;Database=TeamMate_PROD;Integrated Security=True"
                    'myCon = "Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True"
                    Using mySqlConnection As New SqlConnection(myCon)

                        ' Run three queries to get the ProjectInfo, the Scope Areas, the Findings and Action plans
                        ' Get those results in a data table
                        ' Sort the list by Sequence, P_Sort, F_Number, A_Number  (as there is only one project, no sort by project ID is needed)

                        ' Get Project Info
                        mySql = "SELECT 1 AS X_Sequence, dbo.NG_Project.ID AS P_ID, dbo.NG_Project.Title AS P_Title, dbo.NG_Project.Code AS P_Code, dbo.TM_CategoryValue.Name AS P_Grade, dbo.NG_Procedure.Title AS P_Part, dbo.NG_Procedure.Text1 AS P_Part_Text, 'X' AS P_Sort, '-' AS S_Title, '-' AS F_Number, '-' AS F_Title, '-' AS F_Rating, '-' AS F_Text, '-' AS A_Number, '-' AS A_Text, '-' AS A_Responsible, '-' AS A_DueDate "
                        mySql &= "FROM ((dbo.NG_Project INNER JOIN dbo.NG_ContextualAssociation ON dbo.NG_Project.ID = dbo.NG_ContextualAssociation.ContextObjectID) INNER JOIN dbo.TM_CategoryValue ON dbo.NG_Project.Category6CID = dbo.TM_CategoryValue.CategoryID) INNER JOIN dbo.NG_Procedure ON dbo.NG_ContextualAssociation.TargetObjectID = dbo.NG_Procedure.ID "
                        mySql &= "WHERE (((dbo.NG_Project.ID)=" & myP_ID & ") AND ((dbo.NG_Procedure.Title)='Team (incl. Audit Director)' Or (dbo.NG_Procedure.Title)='Distribution List' Or (dbo.NG_Procedure.Title)='Objective & Scope' Or (dbo.NG_Procedure.Title)='Main Findings / Summary' Or (dbo.NG_Procedure.Title)='Opinion' Or (dbo.NG_Procedure.Title)='Organisational Background' Or (dbo.NG_Procedure.Title)='Report Title') AND ((dbo.NG_ContextualAssociation.ContextObjectTypeLID)=81) AND ((dbo.NG_ContextualAssociation.TargetObjectTypeLID)=48));"
                        'mySql2 = "SELECT * FROM Persons WHERE Firstname='Roland'"
                        Using mySQLDataAdapter As New SqlDataAdapter(mySql, mySqlConnection)
                            mySQLDataAdapter.Fill(myDataTable)
                        End Using

                        ' Add Sort Numbers for Project Parts in Dataset
                        For Each myDataRow As DataRow In myDataTable.Rows
                            Select Case myDataRow.Item("P_Part")
                                Case "Team (incl. Audit Director)"
                                    myDataRow.Item("P_Sort") = 2
                                Case "Distribution List"
                                    myDataRow.Item("P_Sort") = 3
                                Case "Objective & Scope"
                                    myDataRow.Item("P_Sort") = 4
                                Case "Main Findings / Summary"
                                    myDataRow.Item("P_Sort") = 5
                                Case "Opinion"
                                    myDataRow.Item("P_Sort") = 6
                                Case "Organisational Background"
                                    myDataRow.Item("P_Sort") = 7
                                Case "Report Title"
                                    myDataRow.Item("P_Sort") = 1
                            End Select
                        Next

                        ' Get Scope Areas
                        mySql = "Select 2 As X_Sequence, dbo.NG_Project.ID As P_ID, dbo.NG_Project.Title As P_Title, '-' As P_Code, '-' As P_Grade, '-' As P_Part, '-' As P_Part_Text, '-' As P_Sort, dbo.NG_Folder.Title As S_Title, '-' As F_Number, '-' As F_Title, '-' As F_Rating, '-' As F_Text, '-' As A_Number, '-' As A_Text, '-' As A_Responsible, '-' As A_DueDate "
                        mySql &= "From dbo.NG_Project INNER Join (dbo.NG_ContextualAssociation As NG_ContextualAssociation_2 INNER Join dbo.NG_Folder On NG_ContextualAssociation_2.SourceObjectID = dbo.NG_Folder.ID) ON dbo.NG_Project.ID = NG_ContextualAssociation_2.ContextObjectID "
                        mySql &= "Where (((NG_ContextualAssociation_2.SourceObjectTypeLID) = 166) And ((NG_ContextualAssociation_2.ContextObjectTypeLID) = 81) And ((dbo.NG_Folder.YesNo2) = 1)) "
                        mySql &= "Group By dbo.NG_Project.ID, dbo.NG_Project.Title, dbo.NG_Folder.Title "
                        mySql &= "Having (((dbo.NG_Project.ID)=" & myP_ID & ")) "
                        mySql &= "Order By dbo.NG_Folder.Title;"
                        'mySql2 = "SELECT * FROM Persons WHERE Firstname='Roland'"
                        Using mySQLDataAdapter As New SqlDataAdapter(mySql, mySqlConnection)
                            mySQLDataAdapter.Fill(myDataTable)
                        End Using

                        ' Get Findings and Action Plans
                        mySql = "Select 3 As X_Sequence, dbo.NG_Project.ID As P_ID, dbo.NG_Project.Title As P_Title, '-' As P_Code, '-' As P_Grade, '-' As P_Part, '-' As P_Part_Text, '-' As P_Sort, '-' As S_Title, dbo.NG_Issue.Code As F_Number, dbo.NG_Issue.Title As F_Title, dbo.TM_CategoryValue.Name As F_Rating, dbo.NG_Issue.Text1 As F_Text, dbo.NG_Recommendation.Code As A_Number, dbo.NG_Recommendation.Text1 As A_Text, dbo.NG_Recommendation.Text4 As A_Responsible, dbo.NG_Recommendation.DueDate As A_DueDate "
                        mySql &= "FROM(((dbo.NG_Project INNER JOIN dbo.NG_ContextualAssociation AS NG_ContextualAssociation_2 ON dbo.NG_Project.ID = NG_ContextualAssociation_2.ContextObjectID) INNER JOIN dbo.NG_Issue On NG_ContextualAssociation_2.SourceObjectID = dbo.NG_Issue.ID) INNER JOIN dbo.NG_Recommendation On NG_ContextualAssociation_2.TargetObjectID = dbo.NG_Recommendation.ID) INNER JOIN dbo.TM_CategoryValue On dbo.NG_Issue.Category7CID = dbo.TM_CategoryValue.CategoryID "
                        mySql &= "WHERE(((dbo.NG_Project.ID)=" & myP_ID & ") And ((NG_ContextualAssociation_2.ContextObjectTypeLID) = 81) And ((NG_ContextualAssociation_2.SourceObjectTypeLID) = 43) And ((NG_ContextualAssociation_2.TargetObjectTypeLID) = 50) And ((dbo.NG_Issue.YesNo3) = 0)) "
                        mySql &= "ORDER BY dbo.NG_Project.Title, dbo.NG_Issue.Code, dbo.NG_Recommendation.Code;"
                        Using mySQLDataAdapter As New SqlDataAdapter(mySql, mySqlConnection)
                            mySQLDataAdapter.Fill(myDataTable)
                        End Using

                        ' Sort the Default Data View and use it later (the Data itself will not be sorted!)

                        myDataTable.DefaultView.Sort = "X_Sequence ASC, P_Sort ASC, F_Number ASC, A_Number ASC"
                        'myDataTable.DefaultView.Sort = "P_Sort ASC, FirstName ASC"
                        Using myFileProtocol As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(mySavePath & "\" & myP_Title & "_Protocol.txt", False)
                            myFileProtocol.WriteLine("Protocol " & Now())

                            myImgCount = 1
                            Using mySortedTable As DataTable = myDataTable.DefaultView.ToTable
                                For Each myDataRow As DataRow In mySortedTable.Rows
                                    myRow = ""
                                    For i As Integer = 0 To myDataRow.ItemArray.Length - 1
                                        myFieldName = mySortedTable.Columns(i).ColumnName.ToString()
                                        Debug.Print(myFieldName & "-" & myDataRow.Item(i))

                                        If Not IsDBNull(myDataRow.Item(i)) Then
                                            myText = myDataRow.Item(i).ToString
                                            ' remove linebreaks & tabs from any field
                                            myText = Replace(myText, Chr(13), "") ' CR
                                            myText = Replace(myText, Chr(10), "") ' LF
                                            myText = Replace(myText, Chr(9), "")  ' Horizontal Tab
                                            myText = Replace(myText, Chr(11), "") ' Vertical Tab
                                            If Len(myText) > 6 Then
                                                If myText.Substring(0, 6) = "<html>" Then
                                                    myFileProtocol.WriteLine("Original " & myCount & ":" & myText)
                                                    myFileProtocol.WriteLine("----------------------------")

                                                    ' Find now the pictures
                                                    For Each myMatch As Match In Regex.Matches(myText, "(<img)(.+?)(\/>)", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
                                                        ' Replace only 1st occurence each time; normal Replace will replace all occurences in one go, given they link to the same picture
                                                        ' Problem with regEx: the pattern contains special characters, which results in parsing exception
                                                        ' Dim myRegExp As New Regex(myMatch.Value)
                                                        ' myText = myRegExp.Replace(myText, "#img" & myImgCount & "#", 1)
                                                        myText = Replace(myText, myMatch.Value, "#img" & myImgCount & "#",, 1)
                                                        myImgCount += 1
                                                    Next

                                                    ' Clean html (remove some tags, remove attributes from some other tags), replace soft linebreaks in paragraphs
                                                    myText = Regex.Replace(myText, "(<(span|html|grammarly|div|teammatelink|/span|/html|/div).*?>)|((<)(body|tr|td|table|p|strong|em|li)\s(.*?)(>))", "$4$5$7")
                                                    ' ATTENTION: This breaks the validity of html:
                                                    ' a) when there is "<p>text as sjkd  <strong> hsjdshdj <br /> skjdh sh sjks j </strong> dsjhdk</p> " => this is ok and valid
                                                    ' b) this becomes  "<p>text as sjkd  <strong> hsjdshdj </p><p> skjdh sh sjks j </strong> sasa</p>" => this is invalid because <strong> is closed with </p>
                                                    ' In the editor when text is produced with paragraph it codes as "<p> text sgj <strong> djhsk </strong></p><p><strong>hsdkshd ksjd skd </strong> sdhsdj </p> => this is ok as well
                                                    ' myText = Replace(myText, "<br />", "</p><p>")
                                                    ' myText = Replace(myText, "<br/>", "</p><p>")
                                                    myFileProtocol.WriteLine("Cleaned " & myCount & ":" & myText)
                                                    myFileProtocol.WriteLine("#############################")
                                                End If
                                            End If
                                        Else
                                            myText = ""
                                        End If
                                        myCount += 1

                                        ' with/without delimiter
                                        If myRow = "" Then
                                            If myText <> "" Then
                                                myRow = Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & EncodeBase64(myText) & Chr(34)
                                            Else
                                                myRow = Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & Chr(34)
                                            End If
                                        Else
                                            If myText <> "" Then
                                                myRow = myRow & "," & Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & EncodeBase64(myText) & Chr(34)
                                            Else
                                                myRow = myRow & "," & Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & Chr(34)
                                            End If
                                        End If
                                    Next
                                    myRows(myRowsCount) = myRow
                                    myRowsCount += 1
                                Next
                            End Using
                            myFileProtocol.Close()
                        End Using
                    End Using
                End Using
                Dim utf8withoutBOM = New System.Text.UTF8Encoding(False)
                Using myFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(mySavePath & "\" & myP_Title & ".json", False, utf8withoutBOM)
                    myFile.WriteLine("{" & Chr(34) & "report" & Chr(34) & ":[")
                    For i As Integer = 0 To myRowsCount - 1
                        If i = myRowsCount - 1 Then
                            myFile.WriteLine("{" & myRows(i) & "}")
                        Else
                            myFile.WriteLine("{" & myRows(i) & "},")
                        End If
                    Next i
                    myFile.WriteLine("]}")
                    myFile.Close()
                End Using
                GenerateJson = myP_Title & ".json"
            Else
                myStatus.Text = "Status: G:\ Drive not found. Please install/run Google Drive."
                myStatus.ForeColor = myRED

            End If
        Catch ex As Exception
            myStatus.Text = "Exception: " & ex.Message
            myStatus.ForeColor = myRED
        End Try
    End Function


    Public Function EncodeBase64(input As String) As String
        Return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
    End Function

    Private Sub Quit_Click(sender As Object, e As EventArgs) Handles Quit.Click
        Application.Exit()
    End Sub

    Private Sub MyTimer_Tick(sender As Object, e As EventArgs) Handles MyTimer.Tick
        ' Properties set to be enabled and runs every 5 seconds
        ' Here it tries to Ping the DB Server and if available green circle, otherwise red circle and warning text
        MyTimer.Interval = 10000       ' 10 seconds for subsequent calls

        Dim MyPing As New Net.NetworkInformation.Ping
        Dim MyPingReply As Net.NetworkInformation.PingReply
        Try
            MyPingReply = MyPing.Send("RKAMVGEMSDBP", 1000)    ' Servername, timeout ms            
            If MyPingReply.Status = Net.NetworkInformation.IPStatus.Success Then
                myStatus.Text = "Status: Connection to Server OK."
                myStatus.ForeColor = myGreen
                MyTimer.Stop()               ' No need to check for VPN any more, project list hopefully was obtained
                If Not myGotProjects Then    ' Do this only one time
                    If GetProjects() = False Then
                        myStatus.Text = "Status: Error with getting projects."
                        myStatus.ForeColor = myRED
                    End If
                End If
            Else
                myStatus.Text = "Status: Cannot find Server. Network? VPN Active?"
                myStatus.ForeColor = myRED
            End If
        Catch ex As Exception
            myStatus.Text = "Status: PING Exception. Network? VPN Active?"
            myStatus.ForeColor = myRED
        End Try
    End Sub

    Private Sub ListBoxProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxProjects.SelectedIndexChanged
        Dim myResult As String

        If ListBoxProjects.SelectedIndex >= 0 Then
            myResult = GenerateJson()
            If myResult <> "" Then
                myStatus.Text = "Status: File generated: " & myResult
                myStatus.ForeColor = myGreen
                Process.Start("C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "https://script.google.com/a/macros/roche.com/s/AKfycbw1UesP3Xr6y4axkCmzIjCVZhLwVBXldCD1jk6zMp85/dev?file=" & Net.WebUtility.UrlEncode(myResult))
            End If
        End If
    End Sub
End Class
