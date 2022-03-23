Public Class Installation
    Private Sub Installation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowInTaskbar = True
        Me.Visible = False
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Me.Text = Form1.Text
        Me.Icon = Form1.Icon
        log("Evaluating files required for installation. Please wait.")
        Timer1.Start() 'cooldown so window does not freeze.


    End Sub

    Sub log(ByVal txt As String)
        setuplog.AppendText(Environment.NewLine & txt)
        globs.log(txt)
    End Sub

    Sub preprepstage()
        log("Downloading files from installation media location now. This will take a few minutes depending on the speed of your disc drive.")
        log("Detected installation media location: " & globs.installationmediapath)
        log("Did the installer detect required components: " & init1.instlocfound.ToString)
        log("Creating temporary directory for installation files now.")
        If My.Computer.FileSystem.DirectoryExists(FileIO.SpecialDirectories.Temp & "\OCSetup") Then
            log("Directory already exists, continuing.")
            downloadFilesStage()
        Else
            log("Attempting creation now...")
            Try
                My.Computer.FileSystem.CreateDirectory(FileIO.SpecialDirectories.Temp & "\OCSetup")
                log("Directory created successfully.")
                preprepstage()
            Catch ex As Exception
                SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Setup was unable to copy required files for installation:" & Environment.NewLine & ex.Message.ToString
            End Try
        End If

    End Sub

    Sub downloadFilesStage()
        log("Copying files now...")
        'Attempt to delete the old directory so we can safely recreate the packages from installation media.
        If My.Computer.FileSystem.DirectoryExists(FileIO.SpecialDirectories.Temp & "\OCSetup\Pkgs") Then
            Try
                My.Computer.FileSystem.DeleteDirectory(FileIO.SpecialDirectories.Temp & "\OCSetup\Pkgs", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception
                'Crash out, we can't copy files if the folder already exists.
                SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Setup was unable to copy required files for installation:" & Environment.NewLine & ex.Message.ToString
            End Try
        End If
        'Detect the OS type and Bit for the installation. 32Bit packages will not successfully install on 64bit, vice versa.
        'XP installations use different packages and need additional KB MSU's to install.
        If OSType = "XP" Then
            'TBD here. Will likely only add 32Bit support for this since X64 XP is uncommon.
            SetupCancelled.Show() : SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & "Thank you for your support. Unfortunately, implementation of Windows XP is not yet supported." & Environment.NewLine & "Please restart setup on a machine running Windows Vista Build 5500 or greater."
            Me.Close()
        Else

            If isx64install = True Then
                'I should probably do this.
                'Proceed with 64-Bit Installation

                '=====================================
                ' PHASE 1 - VERSION DETECTION
                '=====================================

                Dim pkgsx64 = {""}
                'These are the names of the packages
                Dim pkgsnam = {""}

                Select Case globs.discVersion
                    Case "2.0"
                        'These are the package files relative to installmedia path for the version 2.0 Disc
                        pkgsx64 = {"Pkgs\dw20sharedamd64.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\x64\winss.cab", "Pkgs\x64\mpam-fe.exe", "Pkgs\x64\vista\en-gb\AV.cab", "Pkgs\x64\vista\en-gb\MPSSetup.cab", "Pkgs\x64\vista\en-gb\OCLocRes.cab", "Pkgs\x64\vista\en-gb\Upgrade.cab"}
                        'These are the names of the packages
                        pkgsnam = {"DrWatsonX64", "GTOneCare", "Idcrl", "Backup", "WinSS", "AMSigs", "AVBits", "Firewall", "OneCareResources", "Upgrade"}
                    Case "2.5"
                        'These are the package files relative to installmedia path for the version 2.0 Disc
                        pkgsx64 = {"Pkgs\ByProc\x64\dw20sharedamd64.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\ByProc\x64\winss.cab", "Pkgs\ByProc\x64\mpam-fe.exe", "Pkgs\ByProcMarket\x64\en-us\AV.cab", "Pkgs\ByProcOSMarket\x64\vista\en-us\MPSSetup.cab", "Pkgs\ByProcMarket\x64\en-gb\OCLocRes.cab", "Pkgs\ByMarket\en-us\Upgrade.cab"}
                        'These are the names of the packages
                        pkgsnam = {"DrWatsonX64", "GTOneCare", "Idcrl", "Backup", "WinSS", "AMSigs", "AVBits", "Firewall", "OneCareResources", "Upgrade"}

                    Case Else
                        SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "A version of the OneCare disc not supported by this installer was detected." & Environment.NewLine & "Please contact the developer so we can add support for this."
                        SetupCancelled.Show()
                        Me.Close()



                End Select
                '=====================================
                ' PHASE 2 - COPY PACKAGES LOCALLY
                '=====================================

                'This portion utilises the original install.xml file.
                log("Copying packages to the local disk now...")
                For i = 0 To pkgsx64.Length - 1
                    'This portion loops through all the cabinet files and MPAM-FE so we can copy them to the temporary folder for extraction.
                    log("Copying application " & pkgsnam(i).ToString & " located at " & pkgsx64(i).ToString)
                    If pkgsx64(i).Contains("mpam-fe.exe") Then ' We don't want to copy MPAM-FE as a cab file, it's an executable.
                        copyFile(pkgsnam(i), globs.installationmediapath & pkgsx64(i), FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsx64(i) & ".exe")
                    Else
                        copyFile(pkgsnam(i), globs.installationmediapath & pkgsx64(i), FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsx64(i) & ".cab")
                    End If
                    ProgressBar1.Increment(ProgressBar1.Maximum / pkgsx64.Length - 1)
                Next
                ProgressBar1.Increment(ProgressBar1.Maximum - ProgressBar1.Value)
                log("Finished copying packages.") : Label2.Text = "Download complete" : log("Beginning to expand the copied files now. This may take some time.")
                '=====================================
                ' PHASE 3 - EXPAND PACKAGES
                '=====================================
                For i = 0 To pkgsx64.Length - 1
                    If pkgsnam(i) = "AMSigs" Then 'AMSigs / MPAM-FE is not a cabinet, we do not want to extract this.
                    Else
                        log("Expanding " & pkgsnam(i).ToString)
                        Try
                            'This creates a separate folder for each package during extraction to keep things clean
                            My.Computer.FileSystem.CreateDirectory(FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsnam(i).ToString)
                            'Here we call expandCab which will allow the disk a second to sleep, then extract the cabinet.
                            expandCab(pkgsnam(i).ToString, globs.installationmediapath & pkgsx64(i).ToString, FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsnam(i).ToString)
                        Catch ex As Exception
                            'Crash out, somethings failed expanding. OneCare doesn't work correctly if all packages aren't installed in the correct order.
                            'Continuing to install would require rolling back the operating system to a previous state before the installer was ran.
                            SetupCancelled.Show() : SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Unable to create expansion folder for " & pkgsnam(i).ToString
                            Me.Close()
                        End Try
                    End If
                    ProgressBar2.Increment(5)
                Next
                log("Package Expansion finished.")

                'BInstall is now default as the installer for some reason can't execute the MSI's despite having the correct args?
                If globs.isBinstInstall = True Then
                    '=====================================
                    ' PHASE 4 - MSI REQUIRED DIRECTORIES
                    '=====================================
                    log("Binst Specified. The installer may hang whilst this processes.")
                    log("Creating folders...")
                    Try
                        'Create the folder where BOINC will be placed. We need to copy all of our packages and MPAM-FE here.
                        log("Creating OneCare folder...")
                        My.Computer.FileSystem.CreateDirectory("c:\OneCare")
                        log("Creating OneCare Live folder")
                        'Create the required folders for MSI Execution. Without these two folders, MSIEXEC will fail as it expects the logging location to exist.
                        My.Computer.FileSystem.CreateDirectory("C:\Program Files\Microsoft Windows OneCare Live")
                        log("Creating OneCare Live Logs folder...")
                        My.Computer.FileSystem.CreateDirectory("C:\Program Files\Microsoft Windows OneCare Live\Logs")
                    Catch ex As Exception
                        'Crash out, we don't want a partial installation.
                        SetupCancelled.Show()
                        SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Unable to create folders for BINST." & Environment.NewLine & "Specific Error: " & ex.Message.ToString
                        log(ex.Message.ToString)
                        Me.Close()
                    End Try






                    '=====================================
                    ' PHASE 5 - PREPARE FOR BOINC
                    '=====================================

                    Try
                        Select Case globs.discVersion
                            Case "2.0"
                                For i = 0 To pkgsnam.Length - 1
                                    log("Copying " & pkgsnam(i).ToString & ". The installer may freeze for up to 5 Minutes.")
                                    'Copy the required files for installation.
                                    Select Case pkgsnam(i).ToString
                                        Case "DrWatsonX64"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\DrWatsonX64\dw20sharedamd64.msi", "C:\Onecare\dw20shared.msi")
                                        Case "GTOneCare"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\GTOneCare\GTOneCare.msi", "C:\Onecare\GTOneCare.msi")
                                        Case "Idcrl"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Idcrl\Idcrl.msi", "C:\Onecare\Idcrl.msi")
                                        Case "Backup"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Backup\PXEngine.msi", "C:\Onecare\PXEngine.msi")
                                        Case "WinSS"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\WinSS\WinSS.msi", "C:\Onecare\WinSS.msi")
                                        Case "AMSigs"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Pkgs\x64\mpam-fe.exe.exe", "C:\Onecare\mpam-fe.exe")
                                        Case "AVBits"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\AVBits\mp_AVBits.msi", "C:\Onecare\mp_AVBits.msi")
                                        Case "Firewall"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Firewall\MPSSetup.msi", "C:\Onecare\MPSSetup.msi")
                                        Case "OneCareResources"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\OneCareResources\OCLocRes.msi", "C:\Onecare\OCLocRes.msi")
                                        Case "Upgrade"
                                            log("Upgrade caught, finished copying...")
                                            Exit Select
                                    End Select
                                    log("Finished copying file...")
                                    ProgressBar2.Increment(2.5)
                                Next
                            Case "2.5"
                                For i = 0 To pkgsnam.Length - 1
                                    log("Copying " & pkgsnam(i).ToString & ". The installer may freeze for up to 5 Minutes.")
                                    'Copy the required files for installation.
                                    Select Case pkgsnam(i).ToString
                                        Case "DrWatsonX64"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\DrWatsonX64\dw20sharedamd64.msi", "C:\Onecare\dw20shared.msi")
                                        Case "GTOneCare"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\GTOneCare\GTOneCare.msi", "C:\Onecare\GTOneCare.msi")
                                        Case "Idcrl"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Idcrl\Idcrl.msi", "C:\Onecare\Idcrl.msi")
                                        Case "Backup"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Backup\PXEngine.msi", "C:\Onecare\PXEngine.msi")
                                        Case "WinSS"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\WinSS\WinSS.msi", "C:\Onecare\WinSS.msi")
                                        Case "AMSigs"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Pkgs\ByProc\x64\mpam-fe.exe.exe", "C:\Onecare\mpam-fe.exe")
                                        Case "AVBits"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\AVBits\mp_AVBits.msi", "C:\Onecare\mp_AVBits.msi")
                                        Case "Firewall"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Firewall\MPSSetup.msi", "C:\Onecare\MPSSetup.msi")
                                        Case "OneCareResources"
                                            My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\OneCareResources\OCLocRes.msi", "C:\Onecare\OCLocRes.msi")
                                        Case "Upgrade"
                                            log("Upgrade caught, finished copying...")
                                            Exit Select
                                    End Select
                                    log("Finished copying file...")
                                    ProgressBar2.Increment(2.5)
                                Next

                        End Select

                        log("Copy section finished.")





                        '=====================================
                        ' PHASE 6 - ATTEMPT INSTALL MSI PKGS
                        '=====================================

                        log("Beginning BINST.")
                        'This is dreadful I know. The irony of it not working too eh.
                        log("Attempting an automated BOINC installation. This usually doesn't go very well.")
                        installPackage("AVBits", "c:\Windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\mp_AVBits.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\AVBitsInstall.log" & Chr(34) & " INSTALLDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Antivirus" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("MPAM-FE", "C:\OneCare\mpam-fe.exe", "/q ONECARE")
                        ProgressBar1.Increment(5)
                        installPackage("PXEngine", "c:\Windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\PxEngine.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\BackupInstall.log" & Chr(34) & " ALLUSERS=1 ARPSYSTEMCOMPONENT=1 REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("DrWatsonX86", "c:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\dw20shared.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\WatsonInstall.log" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34) & " APPGUID=D07A8E7E-D324-4945-BA8C-E532AD008FF3 REINSTALL=ALL REINSTALLMODE=vomus")
                        ProgressBar1.Increment(5)
                        installPackage("Firewall", "c:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\MPSSetup.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\FWInstall.log" & Chr(34) & " INSTALLDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Firewall" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("OCLocRes", "C:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\OCLocRes.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\ResourceInstall.log" & Chr(34) & " TARGETFOLDER=" & Chr(34) & "Microsoft Windows OneCare Live" & Chr(34) & " ALLUSERS=1 ARPSYSTEMCOMPONENT=1 REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("WinSS", "C:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\WinSS.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\WinSSInstall.log" & Chr(34) & " TARGETFOLDER=" & Chr(34) & "Microsoft Windows OneCare Live" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("Idcrl", "C:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\Idcrl.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\IdcrlInstall.log" & Chr(34) & " ALLUSERS=1 ARPSYSTEMCOMPONENT=1 TARGETDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("GTOneCare", "C:\Windows\System32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\GTOneCare.msi" & Chr(34) & " /qn SYSLANGID=en-us /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\GTOneCare.log" & Chr(34) & " INSTALLDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\GTOneCare" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        '=====================================
                        ' PHASE 7 - BOINC
                        '=====================================
                        log("BOINC")
                        Try
                            'Deploy BOINC and have InstallPackage handle it. This will pause the installer until BOINC exitst, by which point, The Windows Live OneCare service will have started.
                            My.Computer.FileSystem.WriteAllBytes("C:\OneCare\boinc.bat", My.Resources.binst_oc, False)
                            installPackage("Attempted BOINC Automation", "C:\OneCare\boinc.bat", "")
                            ProgressBar2.Increment(10)
                        Catch ex As Exception
                        End Try
                    Catch ex As Exception
                        SetupCancelled.Show()
                        SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Unable to create copy installation files.."
                        Me.Close()
                    End Try
                Else
                    'BINSTALL is now required, Installer doesn't work without it.
                    SetupCancelled.Show()
                    SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "An invalid install type was selected."
                    Me.Close()
                End If
                '========================================
                ' PHASE 8 - WINNER WINNER CHICKEN DINNER
                '========================================
                log("Breakout; finish.")
                SetupComplete.Show()
                Me.Close()

            Else
                'Proceed with 32-Bit Installation
                'These are the package files relative to installmedia path
                Dim pkgsx86 = {"Pkgs\dw20shared.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\x86\winss.cab", "Pkgs\x86\mpam-fe.exe", "Pkgs\x86\vista\en-gb\AV.cab", "Pkgs\x86\vista\en-gb\MPSSetup.cab", "Pkgs\x86\vista\en-gb\OCLocRes.cab", "Pkgs\x86\vista\en-gb\Upgrade.cab"}
                'These are the names of the packages
                Dim pkgsnam = {"DrWatsonX86", "GTOneCare", "Idcrl", "Backup", "WinSS", "AMSigs", "AVBits", "Firewall", "OneCareResources", "Upgrade"}
                'This portion utilises the original install.xml file.
                log("Copying packages to the local disk now...")
                For i = 0 To pkgsx86.Length - 1
                    'This portion loops through all the cabinet files and MPAM-FE so we can copy them to the temporary folder for extraction.
                    log("Copying application " & pkgsnam(i).ToString & " located at " & pkgsx86(i).ToString)
                    If pkgsx86(i).Contains("mpam-fe.exe") Then ' We don't want to copy MPAM-FE as a cab file, it's an executable.
                        copyFile(pkgsnam(i), globs.installationmediapath & pkgsx86(i), FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsx86(i) & ".exe")
                    Else
                        copyFile(pkgsnam(i), globs.installationmediapath & pkgsx86(i), FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsx86(i) & ".cab")
                    End If
                    ProgressBar1.Increment(ProgressBar1.Maximum / pkgsx86.Length - 1)
                Next
                ProgressBar1.Increment(ProgressBar1.Maximum - ProgressBar1.Value)
                log("Finished copying packages.") : Label2.Text = "Download complete" : log("Beginning to expand the copied files now. This may take some time.")
                'Cool, now we can begin extraction.
                For i = 0 To pkgsx86.Length - 1
                    If pkgsnam(i) = "AMSigs" Then 'AMSigs / MPAM-FE is not a cabinet, we do not want to extract this.
                    Else
                        log("Expanding " & pkgsnam(i).ToString)
                        Try
                            'This creates a separate folder for each package during extraction to keep things clean
                            My.Computer.FileSystem.CreateDirectory(FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsnam(i).ToString)
                            'Here we call expandCab which will allow the disk a second to sleep, then extract the cabinet.
                            expandCab(pkgsnam(i).ToString, globs.installationmediapath & pkgsx86(i).ToString, FileIO.SpecialDirectories.Temp & "\OCSetup\" & pkgsnam(i).ToString)
                        Catch ex As Exception
                            'Crash out, somethings failed expanding. OneCare doesn't work correctly if all packages aren't installed in the correct order.
                            'Continuing to install would require rolling back the operating system to a previous state before the installer was ran.
                            SetupCancelled.Show() : SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Unable to create expansion folder for " & pkgsnam(i).ToString
                            Me.Close()
                        End Try
                    End If
                    ProgressBar2.Increment(5)
                Next
                log("Package Expansion finished.")

                'BInstall is now default as the installer for some reason can't execute the MSI's despite having the correct args?
                If globs.isBinstInstall = True Then
                    log("Binst Specified. The installer may hang whilst this processes.")
                    log("Creating folders...")
                    Try
                        'Create the folder where BOINC will be placed. We need to copy all of our packages and MPAM-FE here.
                        log("Creating OneCare folder...")
                        My.Computer.FileSystem.CreateDirectory("c:\OneCare")
                        log("Creating OneCare Live folder")
                        'Create the required folders for MSI Execution. Without these two folders, MSIEXEC will fail as it expects the logging location to exist.
                        My.Computer.FileSystem.CreateDirectory("C:\Program Files\Microsoft Windows OneCare Live")
                        log("Creating OneCare Live Logs folder...")
                        My.Computer.FileSystem.CreateDirectory("C:\Program Files\Microsoft Windows OneCare Live\Logs")
                    Catch ex As Exception
                        'Crash out, we don't want a partial installation.
                        SetupCancelled.Show()
                        SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Unable to create folders for BINST."
                        Me.Close()
                    End Try
                    Try
                        For i = 0 To pkgsnam.Length - 1
                            log("Copying " & pkgsnam(i).ToString & ". The installer may freeze for up to 5 Minutes.")
                            'Copy the required files for installation.
                            Select Case pkgsnam(i).ToString
                                Case "DrWatsonX86"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\DrWatsonX86\dw20shared.msi", "C:\Onecare\dw20shared.msi")
                                Case "GTOneCare"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\GTOneCare\GTOneCare.msi", "C:\Onecare\GTOneCare.msi")
                                Case "Idcrl"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Idcrl\Idcrl.msi", "C:\Onecare\Idcrl.msi")
                                Case "Backup"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Backup\PXEngine.msi", "C:\Onecare\PXEngine.msi")
                                Case "WinSS"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\WinSS\WinSS.msi", "C:\Onecare\WinSS.msi")
                                Case "AMSigs"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Pkgs\x86\mpam-fe.exe.exe", "C:\Onecare\mpam-fe.exe")
                                Case "AVBits"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\AVBits\mp_AVBits.msi", "C:\Onecare\mp_AVBits.msi")
                                Case "Firewall"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\Firewall\MPSSetup.msi", "C:\Onecare\MPSSetup.msi")
                                Case "OneCareResources"
                                    My.Computer.FileSystem.CopyFile(FileIO.SpecialDirectories.Temp & "\OCSetup\OneCareResources\OCLocRes.msi", "C:\Onecare\OCLocRes.msi")
                                Case "Upgrade"
                                    Exit Select
                            End Select
                            ProgressBar2.Increment(2.5)
                        Next
                        log("Beginning BINST.")
                        'This is dreadful I know. The irony of it not working too eh.
                        log("Attempting an automated BOINC installation. This usually doesn't go very well.")
                        installPackage("AVBits", "c:\Windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\mp_AVBits.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\AVBitsInstall.log" & Chr(34) & " INSTALLDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Antivirus" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("MPAM-FE", "C:\OneCare\mpam-fe.exe", "/q ONECARE")
                        ProgressBar1.Increment(5)
                        installPackage("PXEngine", "c:\Windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\PxEngine.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\BackupInstall.log" & Chr(34) & " ALLUSERS=1 ARPSYSTEMCOMPONENT=1 REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("DrWatsonX86", "c:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\dw20shared.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\WatsonInstall.log" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34) & " APPGUID=D07A8E7E-D324-4945-BA8C-E532AD008FF3 REINSTALL=ALL REINSTALLMODE=vomus")
                        ProgressBar1.Increment(5)
                        installPackage("Firewall", "c:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\MPSSetup.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\FWInstall.log" & Chr(34) & " INSTALLDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Firewall" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("OCLocRes", "C:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\OCLocRes.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\ResourceInstall.log" & Chr(34) & " TARGETFOLDER=" & Chr(34) & "Microsoft Windows OneCare Live" & Chr(34) & " ALLUSERS=1 ARPSYSTEMCOMPONENT=1 REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("WinSS", "C:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\WinSS.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\WinSSInstall.log" & Chr(34) & " TARGETFOLDER=" & Chr(34) & "Microsoft Windows OneCare Live" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("Idcrl", "C:\windows\system32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\Idcrl.msi" & Chr(34) & " /qn /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\IdcrlInstall.log" & Chr(34) & " ALLUSERS=1 ARPSYSTEMCOMPONENT=1 TARGETDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        installPackage("GTOneCare", "C:\Windows\System32\msiexec.exe", "/i " & Chr(34) & "C:\Onecare\GTOneCare.msi" & Chr(34) & " /qn SYSLANGID=en-us /l*v " & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\Logs\GTOneCare.log" & Chr(34) & " INSTALLDIR=" & Chr(34) & "%PROGRAMFILES%\Microsoft Windows OneCare Live\GTOneCare" & Chr(34) & " REBOOT=" & Chr(34) & "ReallySuppress" & Chr(34))
                        ProgressBar1.Increment(5)
                        Try
                            'Deploy BOINC and have InstallPackage handle it. This will pause the installer until BOINC exitst, by which point, The Windows Live OneCare service will have started.
                            My.Computer.FileSystem.WriteAllBytes("C:\OneCare\boinc.bat", My.Resources.binst_oc, False)
                            installPackage("Attempted BOINC Automation", "C:\OneCare\boinc.bat", "")
                            ProgressBar2.Increment(10)
                        Catch ex As Exception
                        End Try
                    Catch ex As Exception
                        SetupCancelled.Show()
                        SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Unable to create copy installation files.."
                        Me.Close()
                    End Try
                Else
                    'BINSTALL is now required, Installer doesn't work without it.
                    SetupCancelled.Show()
                    SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "An invalid install type was selected."
                    Me.Close()
                End If
                SetupComplete.Show()
                Me.Close()
            End If
        End If
    End Sub

    Sub installPackage(ByVal filetext As String, filepath As String, args As String)
        log("Package install requested for " & filetext)
        Dim als3 As New PackageInstaller
        als3.filename = filetext
        als3.filepath = filepath
        als3.fileargs = args
        als3.Location = Me.Location
        log("InstallPackage: Calling PackageInstaller for " & filepath & Environment.NewLine & "Using args " & args)
        als3.ShowDialog()
    End Sub

    Sub copyFile(ByVal filetext As String, filepath As String, destpath As String)
        Dim als As New CF
        als.filename = filetext
        als.filepath = filepath
        als.destinationpath = destpath
        als.Location = Me.Location
        als.ShowDialog()
    End Sub

    Sub expandCab(ByVal filetext As String, cabfile As String, destpath As String)
        Dim als2 As New expansion
        als2.filename = filetext
        als2.filepath = cabfile
        als2.destpath = destpath
        als2.Location = Me.Location
        als2.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Timer1.Tag = 3 Then
            Timer1.Stop() : Timer1.Tag = 0
            ProgressBar1.Value = 0
            preprepstage()
        Else
            ProgressBar1.Increment(ProgressBar1.Maximum / 4)
            setuplog.Text = setuplog.Text & "."
            Timer1.Tag = Timer1.Tag + 1
        End If
    End Sub
End Class