Option Explicit On                          ' Declare everything
Imports System.Data.SqlClient               ' SQL handling
Imports System.Text.RegularExpressions      ' Regular Expressions

' General Comments
' ================
' Determine which .Net Framework versions are on the machine
' Get-ChildItem 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP' -recurse | Get-ItemProperty -name Version,Release -EA 0 | Where { $_.PSChildName -match '^(?!S)\p{L}'} | Select Case PSChildName, Version, Release
' VB Basic: Always Option Base 0 => All Arrays zero based


Public Class FormP2J
    Inherits Form
    Dim myRED As Color
    Dim myGreen As Color
    Private Const mySavePath = "G:\Shared drives\GA&RA GA Team\WX_Work in Progress\JSONs"       ' The json files will go there

    Private Sub FormP2J_Load()
        myRED = Color.FromArgb(255, 50, 50)
        myGreen = Color.FromArgb(50, 255, 50)
    End Sub

    Private Sub FormP2J_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' Set Focus to entry field
        myProjectID.Select()
        ShowError("Status: Enter a Project ID", "green")
    End Sub

    ' Currently not used
    Public Function EncodeBase64(input As String) As String
        Return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
    End Function

    Private Sub Quit_Click(sender As Object, e As EventArgs) Handles Quit.Click
        Application.Exit()
    End Sub

    Private Sub CreateJson_Click(sender As Object, e As EventArgs) Handles CreateJson.Click
        Dim mySql As String
        Dim myP_ID As Integer
        Dim myP_Title As String
        Dim myText As String
        Dim myFieldName As String
        Dim myFileName As String
        Dim myRow As String
        Dim myRowsAdded As Integer
        Dim myRows(500) As String
        Dim myRowsCount As Integer
        Dim myCount As Integer
        Dim myImgCount As Integer

        ShowError("Status: Started.", "green")
        myP_ID = CInt(myProjectID.Value)   ' This should be a valid integer number (0-10000) (actually Decimal > Integer)

        ' Select the target folder for json now
        ' RS: #TODO: Potentially need to check for exceptions
        ' Check for G:
        Try
            If Not (IO.Directory.Exists("G:\")) Then
                ShowError("Status: G:\ Drive not found. Please install/run Google Drive.", "red")
                Exit Sub
            End If

            ' Google Drive is available as G:
            ' Create Data Table to hold the data
            Using myDataTable As New DataTable
                ' Create a Connection object.
                Using mySqlConnection As New SqlConnection("Data Source=RKAMVGEMSDBP\TEAMMATE;Database=TeamMate_PROD;Integrated Security=True")
                    ' Run three queries to get the ProjectInfo, the Scope Areas, the Findings and Action plans
                    ' Get those results in a data table
                    ' Sort the list by Sequence, P_Sort, F_Number, A_Number  (as there is only one project, no sort by project ID is needed)

                    ' Get Project Info
                    mySql = "SELECT 1 AS X_Sequence, dbo.NG_Project.ID AS P_ID, dbo.NG_Project.Title AS P_Title, dbo.NG_Project.Code AS P_Code, ISNULL(dbo.TM_CategoryValue.Name,'GAS: not filled') AS P_Grade, dbo.NG_Procedure.Title AS P_Part, dbo.NG_Procedure.Text1 AS P_Part_Text, 'X' AS P_Sort, '-' AS S_Title, '-' AS F_Number, '-' AS F_Title, '-' AS F_Rating, '-' AS F_Text, '-' AS A_Number, '-' AS A_Text, '-' AS A_Responsible, '-' AS A_DueDate "
                    mySql &= "FROM ((dbo.NG_Project INNER JOIN dbo.NG_ContextualAssociation ON dbo.NG_Project.ID = dbo.NG_ContextualAssociation.ContextObjectID) INNER JOIN dbo.TM_CategoryValue ON dbo.NG_Project.Category6CID = dbo.TM_CategoryValue.CategoryID) INNER JOIN dbo.NG_Procedure ON dbo.NG_ContextualAssociation.TargetObjectID = dbo.NG_Procedure.ID "
                    mySql &= "WHERE (((dbo.NG_Project.ID)=" & myP_ID & ") AND ((dbo.NG_Procedure.Title)='Team (incl. Audit Director)' Or (dbo.NG_Procedure.Title)='Distribution List' Or (dbo.NG_Procedure.Title)='Objective & Scope' Or (dbo.NG_Procedure.Title)='Main Findings / Summary' Or (dbo.NG_Procedure.Title)='Opinion' Or (dbo.NG_Procedure.Title)='Organisational Background' Or (dbo.NG_Procedure.Title)='Report Title') AND ((dbo.NG_ContextualAssociation.ContextObjectTypeLID)=81) AND ((dbo.NG_ContextualAssociation.TargetObjectTypeLID)=48));"
                    Using mySQLDataAdapter As New SqlDataAdapter(mySql, mySqlConnection)
                        myRowsAdded = mySQLDataAdapter.Fill(myDataTable)
                    End Using
                    If myDataTable.Rows Is Nothing Then
                        ShowError("Status: No data for this projectId.", "red")
                        Exit Sub
                    End If

                    ' There is data for this project ID
                    ' Add Sort Numbers for Project Parts in Dataset
                    For Each myDataRow As DataRow In myDataTable.Rows
                        Select Case myDataRow.Item("P_Part").ToString()
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
                    myP_Title = myDataTable.Rows(0).Item("P_Title").ToString()
                    myP_Title = Regex.Replace(myP_Title, "\.|:|\\|\/|<|>|\||\?|\*|""", "_")      ' Make sure title can be used on filesystem, replace invalid characters
                    myP_Title = Regex.Replace(myP_Title, " |-|&|=|\+|\(|\)", "_")                ' Further replace items which do not work well in URL or have special meaning (&=)

                    ' Get Scope Areas, i.e. Folders marked "For Report"
                    mySql = "Select 2 As X_Sequence, dbo.NG_Project.ID As P_ID, dbo.NG_Project.Title As P_Title, '-' As P_Code, '-' As P_Grade, '-' As P_Part, '-' As P_Part_Text, '-' As P_Sort, dbo.NG_Folder.Title As S_Title, '-' As F_Number, '-' As F_Title, '-' As F_Rating, '-' As F_Text, '-' As A_Number, '-' As A_Text, '-' As A_Responsible, '-' As A_DueDate "
                    mySql &= "From dbo.NG_Project INNER Join (dbo.NG_ContextualAssociation As NG_ContextualAssociation_2 INNER Join dbo.NG_Folder On NG_ContextualAssociation_2.SourceObjectID = dbo.NG_Folder.ID) ON dbo.NG_Project.ID = NG_ContextualAssociation_2.ContextObjectID "
                    mySql &= "Where (((NG_ContextualAssociation_2.SourceObjectTypeLID) = 166) And ((NG_ContextualAssociation_2.ContextObjectTypeLID) = 81) And ((dbo.NG_Folder.YesNo2) = 1)) "
                    mySql &= "Group By dbo.NG_Project.ID, dbo.NG_Project.Title, dbo.NG_Folder.Title "
                    mySql &= "Having (((dbo.NG_Project.ID)=" & myP_ID & ")) "
                    mySql &= "Order By dbo.NG_Folder.Title;"
                    Using mySQLDataAdapter As New SqlDataAdapter(mySql, mySqlConnection)
                        myRowsAdded = mySQLDataAdapter.Fill(myDataTable)
                        If myRowsAdded = 0 Then
                            ShowError("Warning: No Folders for Report selected. Will continue shortly.", "red")
                            MyWait(5)
                            ShowError("Status: Continuing ...", "green")
                        End If

                    End Using

                    ' Get Findings and Action Plans
                    mySql = "Select 3 As X_Sequence, dbo.NG_Project.ID As P_ID, dbo.NG_Project.Title As P_Title, '-' As P_Code, '-' As P_Grade, '-' As P_Part, '-' As P_Part_Text, '-' As P_Sort, '-' As S_Title, dbo.NG_Issue.Code As F_Number, dbo.NG_Issue.Title As F_Title, ISNULL(dbo.TM_CategoryValue.Name,'GAS: not filled') As F_Rating, dbo.NG_Issue.Text1 As F_Text, dbo.NG_Recommendation.Code As A_Number, dbo.NG_Recommendation.Text1 As A_Text, dbo.NG_Recommendation.Text4 As A_Responsible, dbo.NG_Recommendation.DueDate As A_DueDate "
                    mySql &= "FROM(((dbo.NG_Project INNER JOIN dbo.NG_ContextualAssociation AS NG_ContextualAssociation_2 ON dbo.NG_Project.ID = NG_ContextualAssociation_2.ContextObjectID) INNER JOIN dbo.NG_Issue On NG_ContextualAssociation_2.SourceObjectID = dbo.NG_Issue.ID) INNER JOIN dbo.NG_Recommendation On NG_ContextualAssociation_2.TargetObjectID = dbo.NG_Recommendation.ID) LEFT JOIN dbo.TM_CategoryValue On dbo.NG_Issue.Category7CID = dbo.TM_CategoryValue.CategoryID "
                    mySql &= "WHERE(((dbo.NG_Project.ID)=" & myP_ID & ") And ((NG_ContextualAssociation_2.ContextObjectTypeLID) = 81) And ((NG_ContextualAssociation_2.SourceObjectTypeLID) = 43) And ((NG_ContextualAssociation_2.TargetObjectTypeLID) = 50) And ((dbo.NG_Issue.YesNo3) = 0)) "
                    mySql &= "ORDER BY dbo.NG_Project.Title, dbo.NG_Issue.Code, dbo.NG_Recommendation.Code;"
                    Using mySQLDataAdapter As New SqlDataAdapter(mySql, mySqlConnection)
                        myRowsAdded = mySQLDataAdapter.Fill(myDataTable)
                        If myRowsAdded = 0 Then
                            ShowError("Warning: No Findings found. Will continue shortly.", "red")
                            MyWait(5)
                            ShowError("Status: Continuing ...", "green")
                        End If
                    End Using

                End Using

                ' Sort the Default Data View and use it later (the Data itself will not be sorted!)
                myDataTable.DefaultView.Sort = "X_Sequence ASC, P_Sort ASC, F_Number ASC, A_Number ASC"

                myImgCount = 1
                Using mySortedTable As DataTable = myDataTable.DefaultView.ToTable
                    For Each myDataRow As DataRow In mySortedTable.Rows
                        myRow = ""
                        For i As Integer = 0 To myDataRow.ItemArray.Length - 1
                            myFieldName = mySortedTable.Columns(i).ColumnName.ToString()

                            If Not IsDBNull(myDataRow.Item(i)) Then
                                myText = myDataRow.Item(i).ToString
                                ' remove linebreaks & tabs from any field, escape special characters
                                myText = Replace(myText, Chr(92), "\\")          ' Backslash, attention this needs to be done first, before other \s are introduced
                                myText = Replace(myText, Chr(13), "")            ' CR Carriage Return
                                myText = Replace(myText, Chr(10), "")            ' LF Line Feed
                                myText = Replace(myText, Chr(12), "")            ' FF Form Feed
                                myText = Replace(myText, Chr(9), "")             ' Horizontal Tab
                                myText = Replace(myText, Chr(11), "")            ' Vertical Tab
                                myText = Replace(myText, Chr(7), "")             ' Bell
                                myText = Replace(myText, Chr(8), "")             ' Backspace
                                myText = Replace(myText, Chr(34), "\" & Chr(34)) ' Quote
                                myText = Replace(myText, Chr(47), "\/")          ' Slash
                                ' encode any remaining control characters 0-31
                                For j As Integer = 0 To 9
                                    myText = Replace(myText, Chr(i), "\000" & i)
                                Next j
                                For j As Integer = 10 To 31
                                    myText = Replace(myText, Chr(i), "\00" & i)
                                Next j
                                If Len(myText) > 6 Then
                                    If myText.Substring(0, 6) = "<html>" Then
                                        ' Find now the pictures
                                        For Each myMatch As Match In Regex.Matches(myText, "(<img)(.+?)(\/>)", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
                                            ' Replace only 1st occurence each time; normal Replace will replace all occurences in one go, given they link to the same picture
                                            ' Problem with regEx: the pattern contains special characters, which results in parsing exception
                                            ' Dim myRegExp As New Regex(myMatch.Value)
                                            ' myText = myRegExp.Replace(myText, "#img" & myImgCount & "#", 1)
                                            myText = Replace(myText, myMatch.Value, "#img" & myImgCount & "#",, 1)     ' Magic lies in the ,1 which tells to replace only 1 time
                                            myImgCount += 1
                                        Next

                                        ' Clean html (remove some tags, remove attributes from some other tags), DO NOT replace soft linebreaks in paragraphs (other wise destroys validity of html)
                                        myText = Regex.Replace(myText, "(<(span|html|grammarly|div|teammatelink|\\/span|\\/html|\\/div).*?>)|((<)(body|tr|td|table|p|strong|em|li)\s(.*?)(>))", "$4$5$7")
                                        ' ATTENTION: This broke the validity of html:
                                        ' a) when there is "<p>text as sjkd  <strong> hsjdshdj <br /> skjdh sh sjks j </strong> dsjhdk</p> " => this is ok and valid
                                        ' b) this becomes  "<p>text as sjkd  <strong> hsjdshdj </p><p> skjdh sh sjks j </strong> sasa</p>" => this is invalid because <strong> is closed with </p>
                                        ' In the editor when text is produced with paragraph it codes as "<p> text sgj <strong> djhsk </strong></p><p><strong>hsdkshd ksjd skd </strong> sdhsdj </p> => this is ok as well
                                        ' myText = Replace(myText, "<br />", "</p><p>")
                                        ' myText = Replace(myText, "<br/>", "</p><p>")
                                    End If
                                End If
                            Else
                                myText = ""
                            End If
                            myCount += 1

                            ' with/without delimiter
                            If myRow = "" Then
                                If myText <> "" Then
                                    myRow = Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & myText & Chr(34)
                                Else
                                    myRow = Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & Chr(34)
                                End If
                            Else
                                If myText <> "" Then
                                    myRow = myRow & "," & Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & myText & Chr(34)
                                Else
                                    myRow = myRow & "," & Chr(34) & myFieldName & Chr(34) & ":" & Chr(34) & Chr(34)
                                End If
                            End If
                        Next i
                        myRows(myRowsCount) = myRow
                        myRowsCount += 1
                    Next
                End Using
            End Using

            Dim utf8withoutBOM = New System.Text.UTF8Encoding(False)

            myFileName = myP_Title & ".json"
            Using myFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(mySavePath & "\" & myFileName, False, utf8withoutBOM)
                myFile.WriteLine("{" & Chr(34) & "report" & Chr(34) & ":[")
                For i As Integer = 0 To myRowsCount - 1
                    If i = myRowsCount - 1 Then
                        myFile.WriteLine("{" & myRows(i) & "}")
                    Else
                        myFile.WriteLine("{" & myRows(i) & "},")
                    End If
                Next i
                myFile.WriteLine("]}")
            End Using
            ShowError("Status: File generated. Waiting a bit for sync to gDrive.", "green")
            myFile.Text = "Last file created: " & myFileName
            MyWait(5)   ' Wait to allow gDrive to sync the file from local to cloud
            Process.Start("https://script.google.com/a/macros/roche.com/s/AKfycbw1UesP3Xr6y4axkCmzIjCVZhLwVBXldCD1jk6zMp85/dev?file=" & Net.WebUtility.UrlEncode(myFileName))
            ShowError("Status: Done.", "green")
        Catch ex As Exception
            ShowError("Exception: " & ex.Message, "red")
        End Try
    End Sub

    Private Sub MyWait(seconds As Integer)
        For i As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub

    Sub ShowError(myText As String, myColor As String)
        myStatus.Text = myText
        If myColor = "red" Then
            myStatus.ForeColor = myRED
        Else
            myStatus.ForeColor = myGreen
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start("G:\Shared drives\GA&RA GA Team\WX_Work in Progress\JSONs\")
        Catch ex As Exception
            ShowError("Exception: " & ex.Message, "red")
        End Try
    End Sub
End Class
