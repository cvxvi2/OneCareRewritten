<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class preinstoptions
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 15.0!)
        Me.Label1.Location = New System.Drawing.Point(145, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(288, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Windows Live OneCare Rewritten"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Calibri", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(145, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(256, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Choose what you'd like to do"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Calibri", 13.0!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(145, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(439, 88)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Here you can pick what you'd like to do." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Installation is required first to ena" &
    "ble the option to install" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Firewall Signature/Definition updates from newer vers" &
    "ions."
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(149, 396)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(537, 55)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Exit"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(149, 335)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(537, 55)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Perform Maintenance"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.UnofficialOneCareInstaller.My.Resources.Resources.ocrw64
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(149, 249)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(537, 80)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Install"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'preinstoptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.UnofficialOneCareInstaller.My.Resources.Resources.bdrop
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(689, 479)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.MaximumSize = New System.Drawing.Size(705, 518)
        Me.MinimumSize = New System.Drawing.Size(705, 518)
        Me.Name = "preinstoptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Windows Live OneCare Rewritten Installer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class
