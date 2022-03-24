<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormP2J
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Quit = New System.Windows.Forms.Button()
        Me.myStatus = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CreateJson = New System.Windows.Forms.Button()
        Me.myProjectID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(425, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "In case of any error message please provide a screenshot to roland.schauer@roche." &
    "com"
        '
        'Quit
        '
        Me.Quit.BackColor = System.Drawing.SystemColors.Info
        Me.Quit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Quit.Location = New System.Drawing.Point(484, 8)
        Me.Quit.Name = "Quit"
        Me.Quit.Size = New System.Drawing.Size(82, 29)
        Me.Quit.TabIndex = 7
        Me.Quit.Text = "Quit"
        Me.Quit.UseVisualStyleBackColor = False
        '
        'myStatus
        '
        Me.myStatus.AutoSize = True
        Me.myStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.myStatus.Location = New System.Drawing.Point(11, 15)
        Me.myStatus.Name = "myStatus"
        Me.myStatus.Size = New System.Drawing.Size(145, 15)
        Me.myStatus.TabIndex = 1
        Me.myStatus.Text = "Status: Just started ..."
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 64)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(564, 58)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "Pick the projectId from the URL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Example: https://gas.roche.com/TeamMate/Project#" &
    "!/Project?projectId=50&assessmentId=14" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enter it (in this case 50) in the field " &
    "below and click the button."
        '
        'CreateJson
        '
        Me.CreateJson.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreateJson.Location = New System.Drawing.Point(144, 125)
        Me.CreateJson.Name = "CreateJson"
        Me.CreateJson.Size = New System.Drawing.Size(105, 24)
        Me.CreateJson.TabIndex = 6
        Me.CreateJson.Text = "Create JSON"
        Me.CreateJson.UseVisualStyleBackColor = True
        '
        'myProjectID
        '
        Me.myProjectID.Location = New System.Drawing.Point(76, 128)
        Me.myProjectID.MaxLength = 5
        Me.myProjectID.Name = "myProjectID"
        Me.myProjectID.Size = New System.Drawing.Size(50, 20)
        Me.myProjectID.TabIndex = 5
        Me.myProjectID.WordWrap = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "projectId"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(429, 125)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(120, 20)
        Me.NumericUpDown1.TabIndex = 8
        '
        'FormP2J
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 156)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.myProjectID)
        Me.Controls.Add(Me.CreateJson)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.myStatus)
        Me.Controls.Add(Me.Quit)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormP2J"
        Me.Text = "P2J - Project to Json"
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Quit As Button
    Friend WithEvents myStatus As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CreateJson As Button
    Friend WithEvents myProjectID As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
End Class
