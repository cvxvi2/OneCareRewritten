<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Installation
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.setuplog = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(338, 360)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 23)
        Me.Button1.TabIndex = 52
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
        Me.Button2.TabIndex = 53
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(330, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Please wait while OneCare downloads and installs on your computer." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        Me.Timer1.Tag = "0"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(9, 165)
        Me.ProgressBar1.MarqueeAnimationSpeed = 60
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(511, 23)
        Me.ProgressBar1.TabIndex = 54
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Please specify the OC V2 RTM media installation location."
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "Downloading..." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Installing..."
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(9, 225)
        Me.ProgressBar2.MarqueeAnimationSpeed = 60
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(511, 23)
        Me.ProgressBar2.TabIndex = 56
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(9, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(317, 24)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Installing Windows Live OneCare" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'setuplog
        '
        Me.setuplog.BackColor = System.Drawing.Color.White
        Me.setuplog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.setuplog.Location = New System.Drawing.Point(9, 255)
        Me.setuplog.Multiline = True
        Me.setuplog.Name = "setuplog"
        Me.setuplog.ReadOnly = True
        Me.setuplog.Size = New System.Drawing.Size(511, 85)
        Me.setuplog.TabIndex = 59
        Me.setuplog.Text = "Preparing to install..."
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox2.Location = New System.Drawing.Point(-2, 346)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(542, 58)
        Me.PictureBox2.TabIndex = 51
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.UnofficialOneCareInstaller.My.Resources.Resources.banner
        Me.PictureBox1.Location = New System.Drawing.Point(-2, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(542, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 49
        Me.PictureBox1.TabStop = False
        '
        'Installation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(538, 395)
        Me.Controls.Add(Me.setuplog)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ProgressBar2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Installation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Installation"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents Label4 As Label
    Friend WithEvents setuplog As TextBox
End Class
