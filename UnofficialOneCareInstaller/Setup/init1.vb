Public Class init1
    Public instlocfound As Boolean = False
    Private Sub init1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If isDebugBuild = True Then
            insttlog.Show()
        End If
        log("Initialising init1. Please wait...")
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        'freeze all other forms
        SetupCancelled.MinimumSize = SetupCancelled.Size : SetupCancelled.MaximumSize = SetupCancelled.Size
        Form1.MaximumSize = Form1.Size : Form1.MinimumSize = Form1.Size
        tcsandcs.MaximumSize = tcsandcs.Size : tcsandcs.MinimumSize = tcsandcs.Size
        chooseVersion.MaximumSize = chooseVersion.Size : chooseVersion.MinimumSize = chooseVersion.Size
        CF.MaximumSize = CF.Size : CF.MinimumSize = CF.Size
        Installation.ControlBox = False : Installation.MaximumSize = Installation.Size : Installation.MinimumSize = Installation.Size
        expansion.MaximumSize = expansion.Size : expansion.MinimumSize = expansion.Size
        PackageInstaller.MaximumSize = PackageInstaller.Size : PackageInstaller.MinimumSize = PackageInstaller.Size
        SetupComplete.MaximumSize = SetupComplete.Size : SetupComplete.MinimumSize = SetupComplete.Size
        'Yes, this is bad I know. It works though /shrug.
        log("init1 setup complete, checking for existing OCSetup directories...")
        If My.Computer.FileSystem.DirectoryExists(FileIO.SpecialDirectories.Temp & "\OCSetup") Then
            log("Attempting clean now....[i1:1:22]")
            Try
                My.Computer.FileSystem.DeleteDirectory(FileIO.SpecialDirectories.Temp & "\OCSetup", FileIO.DeleteDirectoryOption.DeleteAllContents)
                log("Complete, starting cooldown.")
                Timer1.Start() 'setup cooldown
            Catch ex As Exception
                log("Failed cleaning directories. Halting setup...")
                log(ex.Message.ToString)
                SetupCancelled.Show()
                SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Setup was unable to remove a previous attempt at installation. Please manually " & Environment.NewLine & "remove %temp%\OCSetup and try again." & Environment.NewLine & ex.Message.ToString
                Me.Close()
            End Try
        Else
            Timer1.Start() 'setup cooldown
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Timer1.Tag = 3 Then
            log("init1/t1 - tick")
            Timer1.Stop()
            quickX64check()
        Else
            Timer1.Tag = Timer1.Tag + 1
        End If
    End Sub

    Sub quickX64check()
        log("quickX64check.")
        'Dirty check, but most likely accurate rather than polling the rather slow windows api.
        If My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)") Then
            globs.isx64install = True
            Me.Text = Me.Text & " (x64)"
        Else
            globs.isx64install = False
        End If
        log("quickX64check = " & globs.isx64install.ToString)
        detectOS()
    End Sub

    Sub detectOS()
        'i lied, we're gonna use it anyways.
        If My.Computer.FileSystem.FileExists(FileIO.SpecialDirectories.Temp & "\OCSetup\bypassos.ini") Then
            detectInstallCD()
        Else
            log("Detecting Operating System now...")
            Dim objCS As Management.ManagementObjectSearcher
            objCS = New Management.ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
            Dim OSName As String = Nothing
            For Each objMgmt In objCS.Get
                OSName = objMgmt("name").ToString
                OSName = OSName.Substring(0, OSName.IndexOf("|"))
            Next
            log("Detected OS as " & OSName)
            If OSName.Contains("XP") Then

                OSType = "XP"
                detectInstallCD()
            ElseIf OSName.Contains("Vista") Then
                OSType = "Vista"
                detectInstallCD()
            Else
                If My.Computer.FileSystem.FileExists("C:\BypassOS.txt") Then
                    OSType = "Vista"
                    detectInstallCD()
                Else
                    OSType = "Other"
                    SetupCancelled.Show()
                    SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Setup could not find a compatible operating system." & Environment.NewLine & "The supported operating systems are Windows XP Service Pack 3 and Windows Vista Build 5500 or greater." & Environment.NewLine & Environment.NewLine & "The detected operating system was " & OSName.ToString
                End If
            End If
        End If
    End Sub
    Sub detectInstallCD()
        log("Detecting installation medium....")
        Dim icd = {"a", "b", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
        For i = 0 To icd.Length - 1
            log("Checking " & icd(i))
            If My.Computer.FileSystem.FileExists(icd(i) & ":\OCSetup.exe") Then
                instlocfound = True
                globs.installationmediapath = (icd(i) & ":\")
                Exit For
            Else
                'not found, no handling needed.
            End If
        Next

        log("Checking if medium was detected...")
        If instlocfound = True Then
            log("Verifying OC version...")
            'detect the installation version based on the packages on the disc.
            Dim dcpvdirs = {"Pkgs\ByMarket", "Pkgs\ByMarket\en-us", "Pkgs\ByProc", "Pkgs\ByProc\x64", "Pkgs\ByProc\x86", "Pkgs\ByProcMarket", "Pkgs\ByProcMarket\x64", "Pkgs\ByProcMarket\x86", "Pkgs\ByProcOS", "Pkgs\ByProcOS\x86", "Pkgs\ByProcOSMarket", "Pkgs\ByProcOSMarket\x64", "Pkgs\ByProcOSMarket\x86"}
            Dim v25detectedPackages As Integer = 0
            For i = 0 To dcpvdirs.Length - 1
                log("Checking for " & dcpvdirs(i) & " where current v25confidence = " & v25detectedPackages)
                If My.Computer.FileSystem.DirectoryExists(globs.installationmediapath & dcpvdirs(i)) Then
                    v25detectedPackages = v25detectedPackages + 1
                End If
            Next
            If v25detectedPackages > 6 Then
                globs.discVersion = "2.5" : Me.Text = " (OC V2.5)"
            Else
                globs.discVersion = "2.0" : Me.Text = " (OC V2.0)"
            End If
            log(globs.discVersion)
            Form1.Show()
            Me.Close()
        Else
            ProgressBar1.Hide() : Button1.Show() : Button2.Show()
            Label1.Text = Label1.Text & Environment.NewLine & Environment.NewLine & "Setup could not locate your installation media. Please specify the installation disc location below."
            'prompt user for installation folder.
            mismedia.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If My.Computer.FileSystem.FileExists(TextBox1.Text & "OCSetup.exe") Then
            globs.installationmediapath = TextBox1.Text

            'detect the installation version based on the packages on the disc.
            Dim dcpvdirs = {"Pkgs\ByMarket", "Pkgs\ByMarket\en-us", "Pkgs\ByProc", "Pkgs\ByProc\x64", "Pkgs\ByProc\x86", "Pkgs\ByProcMarket", "Pkgs\ByProcMarket\x64", "Pkgs\ByProcMarket\x86", "Pkgs\ByProcOS", "Pkgs\ByProcOS\x86", "Pkgs\ByProcOSMarket", "Pkgs\ByProcOSMarket\x64", "Pkgs\ByProcOSMarket\x86"}
            Dim v25detectedPackages As Integer = 0
            For i = 0 To dcpvdirs.Length - 1
                If My.Computer.FileSystem.DirectoryExists(dcpvdirs(i)) Then
                    v25detectedPackages = v25detectedPackages + 1
                End If
            Next
            If v25detectedPackages > 6 Then
                globs.discVersion = "2.5" : Me.Text = " (OC V2.5)"
            Else
                globs.discVersion = "2.0" : Me.Text = " (OC V2.0)"
            End If
            Form1.Show()
            Me.Close()
        Else
            MsgBox("Setup could not locate setup files in the specified directory. Please check your selection and try again.", 0 + 16, Me.Text)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim als2 As Integer
        als2 = MsgBox("Are you sure you want to cancel the installation?" & Environment.NewLine & "Windows Live OneCare Unofficial installation is not complete. Click Yes to cancel the installation, or No to continue installing OneCare.", vbYesNoCancel, "Windows Live OneCare Unofficial")
        If als2 = vbYes Then
            SetupCancelled.Show()
            Me.Close()
        Else
            Me.BringToFront()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox1.Text = FolderBrowserDialog1.SelectedPath & "\"
    End Sub
End Class