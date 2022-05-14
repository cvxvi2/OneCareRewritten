Public Class init1
    Public instlocfound As Boolean = False
    Private Sub init1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If preinst.CheckBox1.Checked = True Then
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
        If Timer1.Tag = 2 Then
            log("Waiting for the form to settle.")
            Timer1.Stop()
            quickX64check()
        Else
            Timer1.Tag = Timer1.Tag + 1
        End If
    End Sub

    Sub quickX64check()
        log("Checking if your system is 32Bit or 64bit...")
        'Dirty check, but most likely accurate rather than polling the rather slow windows api.
        If My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)") Then
            globs.isx64install = True
            Me.Text = Me.Text & " (x64)"
        Else
            globs.isx64install = False
        End If
        log("quickX64check = " & globs.isx64install.ToString)
        log("Checking if your system is 32Bit or 64bit...done!")
        detectOS()
    End Sub

    Sub detectOS()
        log("Detecting your current Operating System...")
        'i lied, we're gonna use it anyways.
        If My.Computer.FileSystem.FileExists(FileIO.SpecialDirectories.Temp & "\OCSetup\bypassos.ini") Then
            log("OS Check is being bypassed. This is likely being ran on a system OTHER than Vista. You're now on your own, OS Type will be set to Vista.")
            detectInstallCD()
        ElseIf globs.bypassOSCheck = True Then
            log("OS Check is being bypassed. This is likely being ran on a system OTHER than Vista. You're now on your own, OS Type will be set to Vista.")
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
                log("You're running Windows XP!")
                OSType = "XP"
                detectInstallCD()
            ElseIf OSName.Contains("Vista") Then
                log("You're running Windows Vista!")
                OSType = "Vista"
                detectInstallCD()
            Else
                If My.Computer.FileSystem.FileExists("C:\BypassOS.txt") Then
                    log("OS Check is being bypassed. This is likely being ran on a system OTHER than Vista. You're now on your own, OS Type will be set to Vista.")
                    OSType = "Vista"
                    detectInstallCD()
                Else
                    log("Not sure what Operating System this is but it's certainly not supported. Let's stop here.")
                    OSType = "Other"
                    SetupCancelled.Show()
                    SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Setup could not find a compatible operating system." & Environment.NewLine & "The supported operating systems are Windows XP Service Pack 3 and Windows Vista Build 5500 or greater." & Environment.NewLine & Environment.NewLine & "The detected operating system was " & OSName.ToString
                End If
            End If
        End If
    End Sub
    Private skipToVersionCheck As Boolean = False
    Sub detectInstallCD()
        'Now for some rocket fuel
        Select Case skipToVersionCheck 'This allows us to recheck the version if the location has to be manually specified.
            Case False
                log("Detecting installation medium....")
                Dim icd = {"a", "b", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
                For i = 0 To icd.Length - 1
                    log("Checking " & icd(i) & ":\ for installation media...")
                    If My.Computer.FileSystem.FileExists(icd(i) & ":\OCSetup.exe") Then
                        log("Found it!")
                        instlocfound = True
                        globs.installationmediapath = (icd(i) & ":\")
                        Exit For
                    Else
                        'not found, no handling needed.
                    End If
                Next

            Case Else
                'do nothing, we're skipping checking for the media

        End Select

        log("Checking if medium was detected...")
        If instlocfound = True Then
            log("Verifying whether or not you have a V2.5 disc, V2.0 or 1.5 Gold.")
            'detect the installation version based on the packages on the disc.
            Dim dcpvdirs = {"Pkgs\ByMarket", "Pkgs\ByMarket\en-us", "Pkgs\ByProc", "Pkgs\ByProc\x64", "Pkgs\ByProc\x86", "Pkgs\ByProcMarket", "Pkgs\ByProcMarket\x64", "Pkgs\ByProcMarket\x86", "Pkgs\ByProcOS", "Pkgs\ByProcOS\x86", "Pkgs\ByProcOSMarket", "Pkgs\ByProcOSMarket\x64", "Pkgs\ByProcOSMarket\x86"}
            Dim v25detectedPackages As Integer = 0
            'v2.5 has extra packages on the disc that 2.0 doesn't, we can use this as a 'confidence level'.
            For i = 0 To dcpvdirs.Length - 1
                log("Checking for " & dcpvdirs(i) & " where current v25confidence = " & v25detectedPackages)
                If My.Computer.FileSystem.DirectoryExists(globs.installationmediapath & dcpvdirs(i)) Then
                    v25detectedPackages = v25detectedPackages + 1
                End If
            Next
            Dim detectedGoldPackages As Integer = 0
            Dim goldPackages = {"Pkgs\x86\vista\en-gb\PseudoBrand01-en-US", "Pkgs\x86\vista\en-gb\PseudoBrand02-fo-FO", "Pkgs\x86\vista\en-gb\PseudoBrand03-ja-JP"}
            Dim goldFiles = {"Pkgs\msxml.cab"}
            log("Checking for Gold (1.5) Packages...")
            For i = 0 To goldPackages.Length - 1
                log("Searching for " & goldPackages(i).ToString & " where Gold confidence = " & detectedGoldPackages)
                If My.Computer.FileSystem.DirectoryExists(globs.installationmediapath & goldPackages(i)) Then
                    detectedGoldPackages = detectedGoldPackages + 1
                End If
            Next
            If My.Computer.FileSystem.FileExists(goldFiles(0)) Then
                detectedGoldPackages = detectedGoldPackages + 1
            End If

            log("Ending detectInstallCD. The results are as follows:" & Environment.NewLine &
                "V2.5 Confidence: " & v25detectedPackages & Environment.NewLine &
                "V1.5 Confidence: " & detectedGoldPackages)

            If v25detectedPackages > 6 Then
                log("Version 2.5 detected. the confidence level is 6 or over.")
                globs.discVersion = "2.5" : Me.Text = " (OC V2.5)"
            ElseIf detectedGoldPackages > 2 Then
                log("Whoah, is that a Gold Disc I see? You'll need a 32Bit Operating System for that. Checking now.")
                globs.discVersion = "1.5" : Me.Text = Me.Text & " (OC 1.5 GOLD)"
                If isx64install = True Then
                    log("Oops, you have a 64 Bit Operating system. These rare V1.5 Gold Discs won't work with that.")
                    SetupCancelled.Show()
                    SetupCancelled.Label1.Text = ("Unsupported Operating System Architecture" & Environment.NewLine & Environment.NewLine & "Unfortunately, the version of OneCare you're attempting to use (A version 1.5 'Gold' Disc), is only supported on the following: " & Environment.NewLine & Environment.NewLine &
                                                    "Windows XP Service Pack 2: 32 Bit" & Environment.NewLine &
                                                    "Windows Vista: 32 Bit" & Environment.NewLine & Environment.NewLine &
                                                    "Your system is running a 64 Bit installation and thus is unsupported for installation. You'll need a Version 2.0 or Version 2.5 disc to continue.")

                    Me.Close()
                    Exit Sub
                Else
                    log("32Bit Operating System detected with a V1.5 'Gold' RTM Disc. Continuing...")
                End If
            Else
                log("Not confident this is a V2.5 disc, not confident it's a V1.5 Gold disc either...it's likely a 2.0 disc.")
                globs.discVersion = "2.0" : Me.Text = " (OC V2.0)"
            End If
            log("Disc type set to " & globs.discVersion)
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
            skipToVersionCheck = True
            detectInstallCD()
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
        Try
            Select Case TextBox1.Text.Last
                Case "\"
                    TextBox1.Text = FolderBrowserDialog1.SelectedPath
                Case Else
                    TextBox1.Text = FolderBrowserDialog1.SelectedPath & "\"
            End Select
        Catch ex As Exception
            TextBox1.Text = FolderBrowserDialog1.SelectedPath & "\"
        End Try

    End Sub
End Class