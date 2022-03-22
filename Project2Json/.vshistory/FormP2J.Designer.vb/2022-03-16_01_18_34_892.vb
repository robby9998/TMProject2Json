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
        Me.components = New System.ComponentModel.Container()
        Me.ListBoxProjects = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Quit = New System.Windows.Forms.Button()
        Me.MyTimer = New System.Windows.Forms.Timer(Me.components)
        Me.myStatus = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ListBoxProjects
        '
        Me.ListBoxProjects.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxProjects.FormattingEnabled = True
        Me.ListBoxProjects.ItemHeight = 18
        Me.ListBoxProjects.Location = New System.Drawing.Point(15, 119)
        Me.ListBoxProjects.Name = "ListBoxProjects"
        Me.ListBoxProjects.Size = New System.Drawing.Size(492, 292)
        Me.ListBoxProjects.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Label5.Location = New System.Drawing.Point(12, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(425, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "In case of any error message please provide a screenshot to roland.schauer@roche." &
    "com"
        '
        'Quit
        '
        Me.Quit.BackColor = System.Drawing.SystemColors.Info
        Me.Quit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Quit.Location = New System.Drawing.Point(425, 417)
        Me.Quit.Name = "Quit"
        Me.Quit.Size = New System.Drawing.Size(82, 29)
        Me.Quit.TabIndex = 8
        Me.Quit.Text = "Quit"
        Me.Quit.UseVisualStyleBackColor = False
        '
        'MyTimer
        '
        Me.MyTimer.Enabled = True
        Me.MyTimer.Interval = 200
        '
        'myStatus
        '
        Me.myStatus.AutoSize = True
        Me.myStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.myStatus.Location = New System.Drawing.Point(11, 15)
        Me.myStatus.Name = "myStatus"
        Me.myStatus.Size = New System.Drawing.Size(161, 15)
        Me.myStatus.TabIndex = 10
        Me.myStatus.Text = "Status: Checking VPN ..."
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(16, 79)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(491, 38)
        Me.TextBox1.TabIndex = 11
        Me.TextBox1.Text = "Click a project from the alphabetical list below to generate a corresponding json" &
    " file on G:\Shared drives\GA&RA GA Team\WX_Work in Progress\JSONs"
        '
        'FormP2J
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 450)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.myStatus)
        Me.Controls.Add(Me.Quit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ListBoxProjects)
        Me.Name = "FormP2J"
        Me.Text = "P2J - Project to Json"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBoxProjects As ListBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Quit As Button
    Friend WithEvents MyTimer As Timer
    Friend WithEvents myStatus As Label
    Friend WithEvents TextBox1 As TextBox
End Class
