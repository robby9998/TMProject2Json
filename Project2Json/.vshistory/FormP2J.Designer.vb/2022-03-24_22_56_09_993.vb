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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormP2J))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Quit = New System.Windows.Forms.Button()
        Me.myStatus = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CreateJson = New System.Windows.Forms.Button()
        Me.myProjectID = New System.Windows.Forms.NumericUpDown()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.myProjectID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Label1.Location = New System.Drawing.Point(12, 32)
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
        Me.Quit.Location = New System.Drawing.Point(697, 305)
        Me.Quit.Name = "Quit"
        Me.Quit.Size = New System.Drawing.Size(82, 29)
        Me.Quit.TabIndex = 7
        Me.Quit.Text = "Quit"
        Me.Quit.UseVisualStyleBackColor = False
        '
        'myStatus
        '
        Me.myStatus.AutoSize = True
        Me.myStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.myStatus.Location = New System.Drawing.Point(11, 15)
        Me.myStatus.Name = "myStatus"
        Me.myStatus.Size = New System.Drawing.Size(133, 13)
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
        Me.TextBox1.Size = New System.Drawing.Size(767, 234)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = resources.GetString("TextBox1.Text")
        '
        'CreateJson
        '
        Me.CreateJson.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreateJson.Location = New System.Drawing.Point(15, 309)
        Me.CreateJson.Name = "CreateJson"
        Me.CreateJson.Size = New System.Drawing.Size(230, 24)
        Me.CreateJson.TabIndex = 5
        Me.CreateJson.Text = "Create JSON and start converter"
        Me.CreateJson.UseVisualStyleBackColor = True
        '
        'myProjectID
        '
        Me.myProjectID.Location = New System.Drawing.Point(394, 64)
        Me.myProjectID.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.myProjectID.Name = "myProjectID"
        Me.myProjectID.Size = New System.Drawing.Size(78, 20)
        Me.myProjectID.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(34, 97)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(508, 28)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 340)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "-"
        '
        'FormP2J
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(791, 367)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.myProjectID)
        Me.Controls.Add(Me.CreateJson)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.myStatus)
        Me.Controls.Add(Me.Quit)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormP2J"
        Me.Text = "P2J - Project to Json"
        CType(Me.myProjectID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Quit As Button
    Friend WithEvents myStatus As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CreateJson As Button
    Friend WithEvents myProjectID As NumericUpDown
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
End Class
