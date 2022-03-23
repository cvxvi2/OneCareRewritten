<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class chooseVersion
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(chooseVersion))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(338, 360)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 23)
        Me.Button1.TabIndex = 45
        Me.Button1.Text = "&Next"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(435, 360)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(91, 23)
        Me.Button2.TabIndex = 47
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox2.Location = New System.Drawing.Point(-2, 346)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(542, 58)
        Me.PictureBox2.TabIndex = 44
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(384, 78)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.UnofficialOneCareInstaller.My.Resources.Resources.banner
        Me.PictureBox1.Location = New System.Drawing.Point(-2, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(542, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 42
        Me.PictureBox1.TabStop = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(241, 360)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(91, 23)
        Me.Button3.TabIndex = 48
        Me.Button3.Text = "&Back"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(12, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 24)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Select Edition" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!)
        Me.RadioButton2.Location = New System.Drawing.Point(16, 230)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(227, 21)
        Me.RadioButton2.TabIndex = 61
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Install the 32bit Edition (Default)"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Enabled = False
        Me.RadioButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!)
        Me.RadioButton1.Location = New System.Drawing.Point(16, 203)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(168, 21)
        Me.RadioButton1.TabIndex = 60
        Me.RadioButton1.Text = "Install the 64bit Edition"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'chooseVersion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(538, 395)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "chooseVersion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "chooseVersion"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
End Class
