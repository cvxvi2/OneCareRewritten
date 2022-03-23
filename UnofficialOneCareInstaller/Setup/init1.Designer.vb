<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class init1
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(init1))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.mismedia = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.mismedia.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(338, 360)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 23)
        Me.Button1.TabIndex = 44
        Me.Button1.Text = "&Next"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(435, 360)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(91, 23)
        Me.Button2.TabIndex = 46
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(385, 39)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Welcome to the setup for Windows Live OneCare Rewritten by ""Cobs""." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Windows Liv" &
    "e OneCare Rewritten will guide you through the installation process." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        Me.Timer1.Tag = "0"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(15, 147)
        Me.ProgressBar1.MarqueeAnimationSpeed = 60
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(511, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 47
        '
        'mismedia
        '
        Me.mismedia.Controls.Add(Me.Button3)
        Me.mismedia.Controls.Add(Me.TextBox1)
        Me.mismedia.Controls.Add(Me.PictureBox3)
        Me.mismedia.Controls.Add(Me.Label2)
        Me.mismedia.Location = New System.Drawing.Point(12, 185)
        Me.mismedia.Name = "mismedia"
        Me.mismedia.Size = New System.Drawing.Size(514, 155)
        Me.mismedia.TabIndex = 48
        Me.mismedia.TabStop = False
        Me.mismedia.Text = "Specify Missing Installation Media Path"
        Me.mismedia.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(358, 80)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(91, 23)
        Me.Button3.TabIndex = 49
        Me.Button3.Text = "&Browse"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(47, 82)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(310, 20)
        Me.TextBox1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(326, 26)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Please insert the disc labelled 'OC V2 RTM' and specify the location" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "below to co" &
    "ntinue installation."
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Please specify the OC V2 RTM media installation location."
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.UnofficialOneCareInstaller.My.Resources.Resources.APPlauncherglows
        Me.PictureBox3.Location = New System.Drawing.Point(6, 19)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox3.TabIndex = 1
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox2.Location = New System.Drawing.Point(-2, 346)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(542, 58)
        Me.PictureBox2.TabIndex = 43
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.UnofficialOneCareInstaller.My.Resources.Resources.banner
        Me.PictureBox1.Location = New System.Drawing.Point(-2, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(542, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 41
        Me.PictureBox1.TabStop = False
        '
        'init1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(538, 395)
        Me.Controls.Add(Me.mismedia)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "init1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Windows Live OneCare Rewritten Installation"
        Me.mismedia.ResumeLayout(False)
        Me.mismedia.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents mismedia As GroupBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox1 As TextBox
End Class
