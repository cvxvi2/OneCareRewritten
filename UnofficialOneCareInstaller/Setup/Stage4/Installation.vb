Public Class Installation
    Private PreferredLanguageCode As String = "en-gb"
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
        'Some debug info for us, this can probs be pulled in release.
        log("Detected installation media location: " & globs.installationmediapath)
        log("Did the installer detect required components: " & init1.instlocfound.ToString)
        log("Creating temporary directory for installation files now.")
        'We will store the extracted packages here when we're ready for BOINC installation.
        If My.Computer.FileSystem.DirectoryExists("C:\Onecare") Then
        Else
            Try
                My.Computer.FileSystem.CreateDirectory("C:\Onecare")
            Catch ex As Exception
                cancelInstallation("Unable to create required directory.", "Setup was unable to create C:\OneCare. Please validate permissions and try again.")
            End Try
        End If
        'Create temporary directory for us to download the relevant files to.
        If My.Computer.FileSystem.DirectoryExists(FileIO.SpecialDirectories.Temp & "\OCSetup") Then
            log("Directory already exists, continuing.")
            'Launch Modern Installer instead of the legacy clunky version.
            ' downloadFilesStage()
            modernInstaller()
        Else
            log("Attempting creation now...")
            Try
                My.Computer.FileSystem.CreateDirectory(FileIO.SpecialDirectories.Temp & "\OCSetup")
                log("Directory created successfully.")
                preprepstage() 'potential stack overflow here if the directory is created, then removed before it reruns constantly.
            Catch ex As Exception
                cancelInstallation("Setup was unable to copy required files for installation:", ex.Message.ToString)
            End Try
        End If

    End Sub

    Function expandCabXP(ByVal filename As String, ByVal filepath As String, ByVal destination As String, ByVal createdir As Boolean, ByVal rename2msi As Boolean)
        'Have made this a bit different for XP. It won't show a GUI for expansion.

        'Debug Info
        log("XP Mode Cabinet Extraction beginning. The current parameters are:" & Environment.NewLine &
            "filename: " & filename & Environment.NewLine &
            "filepath: " & filepath & Environment.NewLine &
            "destination: " & destination & Environment.NewLine &
            "createdir: " & createdir & Environment.NewLine &
            "rename2msi: " & rename2msi)

        Select Case createdir
            Case True
                log("Checking if dir already exists...")
                If destination.Contains(".cab") Then
                    destination = destination.Replace(".cab", Nothing)
                End If
                If destination.Contains(".msi") Then
                    destination = destination.Replace(".msi", Nothing)
                End If
                If My.Computer.FileSystem.DirectoryExists(destination) Then
                    'continue as normal, already exists.
                    log("Checking if dir already exists...it does!")
                Else
                    log("Checking if dir already exists...it doesn't, creating now...")
                    Try
                        My.Computer.FileSystem.CreateDirectory(destination)
                        log("Checking if dir already exists...created!")
                    Catch ex As Exception
                        log("Checking if dir already exists...failure! " & ex.Message.ToString)
                        SetupCancelled.Show() : SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & "An unexpected error whilst extracting packages occurred: " & ex.Message.ToString
                        Me.Close()
                    End Try
                End If
            Case Else
                log("Create Directory not specified, carrying on without.")
        End Select
        log("Checking rename2msi")
        Select Case rename2msi
            Case True
                log("Yup, starting expansion with -r MSI.")
                Dim expm As New ProcessStartInfo
                With expm
                    .FileName = "c:\Windows\System32\expand.exe"
                    .Arguments = """" & filepath & """" & " -F:* " & """" & destination & """ -R:*.MSI"
                    .CreateNoWindow = True
                    .UseShellExecute = False
                    .WindowStyle = ProcessWindowStyle.Hidden
                End With
                Dim expander As Process = Process.Start(expm)
                log("Expander starting...")
                expander.WaitForExit()
                log("Expander starting...finished!")

            Case False
                log("Nope, starting without.")
                Dim expm As New ProcessStartInfo
                With expm
                    .FileName = "c:\Windows\System32\expand.exe"
                    .Arguments = """" & filepath & """" & " -F:* " & """" & destination & """ -R:*.exe"
                    .CreateNoWindow = True
                    .UseShellExecute = False
                    .WindowStyle = ProcessWindowStyle.Hidden
                End With
                Dim expander As Process = Process.Start(expm)
                log("Expander starting...")
                expander.WaitForExit()
                log("Expander starting...finished!")

        End Select
        System.Threading.Thread.Sleep(1000)
        Return True


    End Function


    Public errorType As String = "A fatal error occurred but information was not received."
    Public errorLog As String = "No specific error log was returned."


    Sub modernInstaller()
        'pre-checks


        'For V1.5 discs:
        'XP SP2 and Vista 32BIT ONLY support, no 64bit for Vista
        '
        'For V2.0 discs:
        'XP SP2 and Vista 32/64bit ONLY, not SP3, uSP4 or SP1 support for XP.

        'For V2.5 discs
        'anything goes, as long as it's vista or XP...cough cough IdCRL package.

        Select Case downloadFiles()
            Case True
                Label2.Text = "Download files complete. You can eject your disc."
            Case False
                cancelInstallation(errorType, errorLog)
        End Select

        'sleep for 2 seconds to allow anything cached in RAM to drain a bit.
        System.Threading.Thread.Sleep(2000)
        Select Case extractFiles()
            Case True
                Label5.Text = "Extracting files complete."
            Case False
                cancelInstallation(errorType, errorLog)
        End Select
        Select Case installFiles()
            Case True
                SetupComplete.Show()
                Me.Close()
            Case False

                cancelInstallation(errorType, errorLog)
        End Select
    End Sub

    Sub cancelInstallation(ByVal errortype, errorlog)
        'This will be called when a fatal error occurs and the installation cannot continue.
        'Have left byvals in just incase we need something specific to display whilst retaining the logs, perhaps for future use.
        SetupCancelled.Label1.Text = errortype & Environment.NewLine & Environment.NewLine & errorlog
        SetupCancelled.Show()
        Me.Close()
    End Sub

    Private destfiles As String()   ' This is used in conjunction with filesToCopy from downloadFiles, but will need to be referenced by other functions here.
    Function downloadFiles()
        'Copy the relevant cabs from the disc based on disc version and OS. Each have different sets of files and locations.
        'This is ran as a function so we can return true or false based on whether or not it was successful.
        log("Preparing to download files from your disc. Currently figuring out which ones you need based on disc type and your current operating system.")
        'Annoyingly each architecture, disc version and OS version has it's own set of files that we need, so this gets a bit messy.
        'OS > Architecture > Disc Version
        'OS will initially be used for us to specify between the Vista and XP folders, Architecture to grab the 64bit versions instead if we need them,
        'and the disc type because the files are in different folders for each disc. Not sure why they didn't just keep them in the same directories.
        Dim filesToCopy As String() ' Initial array of the files that will be copied



        ' TODO:
        ' -Use PreferredLanguageCode to check if the files are available on the preferred language
        '  If not, revert to defaults, en-gb or en-us.
        '  Perhaps separate en-gb and en-us packages? Seems to be a mix.

        log("Building list of files to copy based on your OS Type of " & OSType)
        Select Case OSType
            Case "Vista"
                log("Current OS type is Vista")
                Select Case isx64install

                    Case True
                        log("Current installation type is x64.")
                        Select Case globs.discVersion
                            Case "1.5"
                                '64 Bit Installation Files
                                '1.5 Gold does not support 64bit operating systems at all.
                                log("Oops, you have a 64 Bit Operating system. These rare V1.5 Gold Discs won't work with that.")
                                SetupCancelled.Show()
                                SetupCancelled.Label1.Text = ("Unsupported Operating System Architecture" & Environment.NewLine & Environment.NewLine & "Unfortunately, the version of OneCare you're attempting to use (A version 1.5 'Gold' Disc), is only supported on the following: " & Environment.NewLine & Environment.NewLine &
                                                    "Windows XP Service Pack 2: 32 Bit" & Environment.NewLine &
                                                    "Windows Vista: 32 Bit" & Environment.NewLine & Environment.NewLine &
                                                    "Your system is running a 64 Bit installation and thus is unsupported for installation. You'll need a Version 2.0 or Version 2.5 disc to continue.")
                                Me.Close()
                                Return False
                            Case "2.0"
                                log("2.0 Disc ")
                                filesToCopy = {"Pkgs\dw20sharedamd64.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\x64\winss.cab", "Pkgs\x64\mpam-fe.exe", "Pkgs\x64\vista\en-gb\AV.cab", "Pkgs\x64\vista\en-gb\MPSSetup.cab", "Pkgs\x64\vista\en-gb\OCLocRes.cab", "Pkgs\x64\vista\en-gb\Upgrade.cab"}
                                destfiles = {"dw20sharedamd64.cab", "GTOneCare.cab", "idcrl.cab", "PxEngine.cab", "winss.cab", "mpam-fe.exe", "AV.cab", "MPSSetup.cab", "OCLocRes.cab", "Upgrade.cab"}
                            Case "2.5"
                                log("2.5 Disc")
                                filesToCopy = {"Pkgs\ByProc\x64\dw20sharedamd64.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\ByProc\x64\winss.cab", "Pkgs\ByProc\x64\mpam-fe.exe", "Pkgs\ByProcMarket\x64\en-us\AV.cab", "Pkgs\ByProcOSMarket\x64\vista\en-us\MPSSetup.cab", "Pkgs\ByProcMarket\x64\en-gb\OCLocRes.cab", "Pkgs\ByMarket\en-us\Upgrade.cab"}
                                destfiles = {"dw20sharedamd64.cab", "GTOneCare.cab", "idcrl.cab", "PxEngine.cab", "winss.cab", "mpam-fe.exe", "AV.cab", "MPSSetup.cab", "OCLesRes.cab", "Upgrade.cab"}
                        End Select

                    Case False
                        log("Current installation type is x32.")

                        Select Case globs.discVersion
                            Case "1.5"
                                filesToCopy = {"Pkgs\x86\dw20shared.cab", "Pkgs\x86\Idcrl.cab", "Pkgs\x86\mpam-fe.exe", "Pkgs\x86\msxml.cab", "Pkgs\x86\PxEngine.cab", "Pkgs\x86\winss.cab", "Pkgs\x86\vista\en-gb\AV.cab", "Pkgs\x86\vista\en-gb\MPSSetup.cab", "Pkgs\x86\vista\en-gb\OCLocRes.cab", "Pkgs\x86\vista\en-gb\Upgrade.cab"}
                                destfiles = {"dw20shared.cab", "Idcrl.cab", "mpam-fe.exe", "msxml.cab", "PxEngine.cab", "winss.cab", "AV.cab", "MPSSetup.cab", "OCLocRes.cab", "Upgrade.cab"}
                            Case "2.0"
                                filesToCopy = {"Pkgs\dw20shared.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\x86\winss.cab", "Pkgs\x86\mpam-fe.exe", "Pkgs\x64\vista\en-gb\AV.cab", "Pkgs\x86\vista\en-gb\MPSSetup.cab", "Pkgs\x86\vista\en-gb\OCLocRes.cab", "Pkgs\x86\vista\en-gb\Upgrade.cab"}
                                destfiles = {"dw20shared.cab", "GTOneCare.cab", "idcrl.cab", "PxEngine.cab", "winss.cab", "mpam-fe.exe", "AV.cab", "MPSSetup.cab", "OCLocRes.cab", "Upgrade.cab"}
                            Case "2.5"
                                filesToCopy = {"Pkgs\dw20shared.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\ByProc\x86\winss.cab", "Pkgs\ByProc\x86\mpam-fe.exe", "Pkgs\ByProcMarket\x86\en-us\AV.cab", "Pkgs\ByProcOSMarket\x86\vista\en-us\MPSSetup.cab", "Pkgs\ByProcMarket\x86\en-gb\OCLocRes.cab", "Pkgs\ByMarket\en-us\Upgrade.cab"}
                                destfiles = {"dw20shared.cab", "GTOneCare.cab", "idcrl.cab", "PxEngine.cab", "winss.cab", "mpam-fe.exe", "AV.cab", "MPSSetup.cab", "OCLocRes.cab", "Upgrade.cab"}
                        End Select

                End Select
            Case "XP"
                log("Current OS type is XP")
                Select Case isx64install
                    Case True
                        log("Current installation type is x64.")
                        SetupCancelled.Show()
                        SetupCancelled.Label1.Text = ("Unsupported Operating System " & Environment.NewLine & Environment.NewLine & "Unfortunately, Windows XP Support is currently still in beta and is not supported on 64-Bit systems.")
                        Return False
                    Case False
                        log("[installation.vb] [XP Install] [264] Current installation type is x32.")
                        log("[installation.vb] [XP Install] [265] Disc Version: " & globs.discVersion)
                        Select Case globs.discVersion
                            Case "1.5"
                                If Environment.OSVersion.VersionString.Contains("Service Pack 3") Then
                                    'Idcrl 1.5 does not support SP3, only SP2.
                                    log("Current OS type is not supported. Cancelling the installation.")
                                    log("Unsupported Operating System detected. If you're trying to use Longhorn or a beta, it might be worth trying the 'ByPass OS' Checkbox. Please note this will set it to Vista however.")
                                    cancelInstallation("Unsupported Operating System", "Unfortunately the operating system you're currently running is not compatible with the disc you have." & Environment.NewLine &
                                    "For V1.5: XP SP2 or Vista 32 Bit ONLY" & Environment.NewLine &
                                    "For V2.0: XP 32Bit SP2 or Vista 32/64 Bit ONLY" & Environment.NewLine &
                                    "For V2.5: XP 32Bit SP2 or Vista 32/64 Bit ONLY")
                                Else
                                    filesToCopy = {"Pkgs\x86\dw20shared.cab", "Pkgs\x86\Idcrl.cab", "Pkgs\x86\mpam-fe.exe", "Pkgs\x86\msxml.cab", "Pkgs\x86\PxEngine.cab", "Pkgs\x86\winss.cab", "Pkgs\x86\xp\dotnet.cab", "Pkgs\x86\xp\en-gb\AV.cab", "Pkgs\x86\xp\en-gb\KB914882.cab", "Pkgs\x86\xp\en-gb\MPSSetup.cab", "Pkgs\x86\xp\en-gb\OCLocRes.cab", "Pkgs\x86\xp\en-gb\Upgrade.cab"}
                                    destfiles = {"dw20shared.cab", "Idcrl.cab", "mpam-fe.exe", "msxml.cab", "PxEngine.cab", "winss.cab", "dotnet.cab", "AV.cab", "KB914882.cab", "MPSSetup.cab", "OCLocRes.cab", "Upgrade.cab"}
                                End If
                            Case "2.0"
                                log("Checking Service pack version...")
                                If Environment.OSVersion.VersionString.Contains("Service Pack 3") Then
                                    log("Unsupported service pack detected!")
                                    'Idcrl 2.0 does not support SP3, only SP2.
                                    SetupCancelled.Label1.Text = ("Unsupported Operating System " & Environment.NewLine & Environment.NewLine & "Unfortunately, Version 2.0 discs only support Service Pack 2 for XP. You'll need to downgrade to Service Pack 2 or use a V2.5 disc.")
                                    SetupCancelled.Show()
                                    Exit Function
                                Else
                                    log("Beginning files to copy.")
                                    filesToCopy = {"Pkgs\dw20shared.cab", "Pkgs\Idcrl.cab", "Pkgs\x86\mpam-fe.exe", "Pkgs\PxEngine.cab", "Pkgs\x86\winss.cab", "Pkgs\x86\xp\dotnet.cab", "Pkgs\x86\xp\en-gb\AV.cab", "Pkgs\x86\xp\en-gb\KB923845.cab", "Pkgs\x86\xp\en-gb\KB914882.cab", "Pkgs\x86\xp\en-gb\MPSSetup.cab", "Pkgs\x86\xp\en-gb\OCLocRes.cab", "Pkgs\x86\xp\en-gb\Upgrade.cab"}
                                    destfiles = {"dw20shared.cab", "Idcrl.cab", "mpam-fe.exe", "PxEngine.cab", "winss.cab", "dotnet.cab", "AV.cab", "KB923845.cab", "KB914882.cab", "MPSSetup.cab", "OCLocRes.cab", "Upgrade.cab"}
                                    'not finished
                                End If
                            Case "2.5"
                                log("Beginning files to copy.")
                                filesToCopy = {"Pkgs\dw20shared.cab", "Pkgs\GTOneCare.cab", "Pkgs\idcrl.cab", "Pkgs\PxEngine.cab", "Pkgs\ByProc\x86\winss.cab", "Pkgs\ByProc\x86\mpam-fe.exe", "Pkgs\ByProcMarket\x86\en-us\AV.cab", "Pkgs\ByProcOSMarket\x86\xp\en-us\MPSSetup.cab", "Pkgs\ByProcOSMarket\x86\xp\en-us\KB914882.cab", "Pkgs\ByProcOSMarket\x86\xp\en-us\KB923845.cab", "Pkgs\ByProcMarket\x86\en-gb\OCLocRes.cab", "Pkgs\ByMarket\en-us\Upgrade.cab"}
                                destfiles = {"dw20shared.cab", "GTOneCare.cab", "idcrl.cab", "PxEngine.cab", "winss.cab", "mpam-fe.exe", "AV.cab", "MPSSetup.cab", "KB914882.cab", "KB923845.cab", "OCLocRes.cab", "Upgrade.cab"}
                            Case Else
                                SetupCancelled.Show()
                                SetupCancelled.Label1.Text = ("Unsupported Operating System " & Environment.NewLine & Environment.NewLine & "Unfortunately, Windows XP Support is currently still in beta and is not supported.")
                                Return False
                        End Select

                End Select

            Case Else
                log("Current OS type is not supported. Cancelling the installation.")
                log("Unsupported Operating System detected. If you're trying to use Longhorn or a beta, it might be worth trying the 'ByPass OS' Checkbox. Please note this will set it to Vista however.")
                SetupCancelled.Label1.Text = "Unsupported Service Pack for this Operating System." & Environment.NewLine & Environment.NewLine & "Unfortunately the operating system you're currently running is not compatible with the disc you have." & Environment.NewLine &
                "For V1.5: XP SP2 or Vista 32 Bit ONLY" & Environment.NewLine &
                "For V2.0: XP 32Bit SP2 or Vista 32/64 Bit ONLY" & Environment.NewLine &
                "For V2.5: XP 32Bit SP2 or Vista 32/64 Bit ONLY"
        End Select
        'Update the progressbar to match the files that need to be copied.
        log("Updating Progress bar information")
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = filesToCopy.Length
        'Loop through the files to copy.
        log("Preparing to copy files...")
        For i = 0 To filesToCopy.Length - 1
            log("Downloading file " & destfiles(i).ToString & " from disc at " & filesToCopy(i).ToString) : Label2.Text = "Downloading file " & destfiles(i).ToString & " from disc..."
            Try
                My.Computer.FileSystem.CopyFile(globs.installationmediapath & filesToCopy(i), FileIO.SpecialDirectories.Temp & "\OCSetup\" & destfiles(i))
                ProgressBar1.Increment(1)
                log("Copy complete.")
                'Wahey, everything copied.
            Catch ex As Exception
                log("Failed to copy file " & destfiles(i).ToString & " from origin location " & filesToCopy(i).ToString) : log(ex.Message.ToString)
                errorType = "Installation failed due to a file download error from disc."
                errorLog = ("OneCare Rewritten failed to download file '" & destfiles(i).ToString & " from the origin disc location of " & filesToCopy(i).ToString & Environment.NewLine & Environment.NewLine & "The specific error was: " & ex.Message.ToString & Environment.NewLine & Environment.NewLine & "If this continues to fail, please log an error in Github.")
                'Returning false will cancel the install for us. No need to call cancelinstallation here.
                Return False
            End Try
            System.Threading.Thread.Sleep(1000)
        Next
        Label2.Text = "Download files from disc complete."
        Return True

    End Function

    Function extractFiles()
        'Extract (expand) the cabinet files from the disc, it's faster to do this from the hard drive/SSD rather than directly from the disc and also
        'easier if we need to pickup from a previously failed installation.
        'This is ran as a function so we can return true or false based on whether or not it was successful.
        Me.TopMost = True 'This prevents the CMD windows that wil flash up from breaking the GUI, this occurs because the thread gets hung whilst it continuously calls CMD to expand files.
        'Set progressbar values to represent how many files are to be extracted.
        ProgressBar3.Value = 0
        ProgressBar3.Maximum = destfiles.Length - 1
        log("Expanding files now.")
        For i = 0 To destfiles.Length - 1
            Label5.Text = "Extracting " & destfiles(i) & "..."
            log("Expanding " & i & " of " & destfiles.Length & ": " & destfiles(i).ToString)
            If destfiles(i).Contains(".exe") Then
                log("EXE detection, redirecting...")
                Me.TopMost = False
                log("Copying file now, please wait...")
                copyFile(destfiles(i), FileIO.SpecialDirectories.Temp & "\OCSetup\" & destfiles(i), "c:\OneCare\" & destfiles(i))
                Me.TopMost = True
                Me.Refresh()
                System.Threading.Thread.Sleep(1000)
            Else
                If expandCabXP(destfiles(i), FileIO.SpecialDirectories.Temp & "\OCSetup\" & destfiles(i), "c:\OneCare", True, True) = True Then
                    ProgressBar3.Increment(1)
                    log("Expanding " & i & " of " & destfiles.Length & ": " & destfiles(i).ToString & " complete!")
                Else
                    errorType = "Cabinet failed expansion." : errorLog = "An error occurred whilst expanding " & destfiles(i) & ". Please validate permissions and try again. "
                    Return False
                    Exit For
                End If
            End If

        Next
        log("All files expanded successfully.")
        Return True
    End Function


    Function installFiles()
        'BOINC will usually handle this as the below for some reason just doesn't work, despite being 'technically' correct.
        log("Beginning installation phase. Checking your disc version.")
        Select Case globs.discVersion
            Case "1.5"
                log("Version " & globs.discVersion & " detected")
                Select Case OSType
                    Case "XP"
                        If Environment.OSVersion.VersionString.Contains("Service Pack 3") Then
                            errorType = "Unsupported Service Pack"
                            errorLog = "Unfortunately, Version 1.5 'Gold' discs only support Service Pack 2 for XP. You'll need a V2.5 disc or newer to install on XP SP3, alternatively, downgrade to SP2."
                            Return False
                        Else
                            Me.TopMost = False
                            log("Installing pre-requisite KB914882 (x86)...")
                            installPackage("XP Pre-requisite Update", "C:\OneCare\WindowsXP-KB914882-x86.exe", "")
                            Me.TopMost = True
                        End If

                    Case Else
                        'Do nothing, not needed.
                End Select
                'Going to skip straight to BOINC for this.
                Try
                    My.Computer.FileSystem.WriteAllBytes("C:\Onecare\GoldBOINC.bat", My.Resources.GoldenBOINC, False)
                Catch ex As Exception
                    errorType = "Unable to copy the Gold BOINC package."
                    errorLog = "An error occurred whilst copying the Gold BOINC package to c:\Onecare. Please validate permissions and try again. The specific error tripped was: " & ex.Message.ToString
                    Return False
                End Try
                showNoWindow = True

                ProgressBar2.Style = ProgressBarStyle.Marquee
                ProgressBar2.MarqueeAnimationSpeed = 40
                Me.TopMost = False
                installPackage("V1.5 Gold BOINC Automation", "c:\OneCare\GoldBOINC.bat", Nothing)
                Return True
            Case "2.0"
                log("Version " & globs.discVersion & " detected")

                Select Case OSType
                    Case "XP"

                        If Environment.OSVersion.VersionString.Contains("Service Pack 3") Then
                            errorType = "Unsupported Service Pack"
                            errorLog = "Unfortunately, Version 2.0 discs only support Service Pack 2 for XP. You'll need to downgrade to Service Pack 2 or use a V2.5 disc."
                            Return False
                        Else
                            log("XP Detection, preparing pre-reqs...")
                            Me.TopMost = False
                            log("Installing pre-requisites...0 of 2")
                            installPackage("XP Pre-requisite Update", "C:\OneCare\WindowsXP-KB914882-x86.exe", "")
                            log("Installing pre-requisites...1 of 2")
                            installPackage("XP Pre-requisite Update 2", "C:\OneCare\WindowsXP-KB923845-x86.exe", "")
                            log("Installing pre-requisites...2 of 2")
                            Me.TopMost = True
                            log("Done with pre-requisites. Continuing now.")
                        End If



                    Case Else
                        'Do nothing, not needed.
                End Select



                Try
                    log("Writing BOINC")
                    My.Computer.FileSystem.WriteAllBytes("C:\Onecare\BOINC.bat", My.Resources.binst_oc, False)
                Catch ex As Exception
                    errorType = "Unable to copy the BOINC package."
                    errorLog = "An error occurred whilst copying the  BOINC package to c:\Onecare. Please validate permissions and try again. The specific error tripped was: " & ex.Message.ToString
                    Return False
                End Try
                showNoWindow = True

                ProgressBar2.Style = ProgressBarStyle.Marquee
                ProgressBar2.MarqueeAnimationSpeed = 40
                Me.TopMost = False
                installPackage("V2.0 BOINC Automation", "c:\OneCare\BOINC.bat", Nothing)
                Return True
            Case "2.5"
                log("Version " & globs.discVersion & " detected")

                Select Case OSType
                    Case "XP"
                        log("XP Detection, preparing pre-reqs...")
                        Me.TopMost = False
                        log("Installing pre-requisites...0 of 2")
                        installPackage("XP Pre-requisite Update", "C:\OneCare\WindowsXP-KB914882-x86.exe", "")
                        log("Installing pre-requisites...1 of 2")
                        installPackage("XP Pre-requisite Update 2", "C:\OneCare\WindowsXP-KB923845-x86.exe", "")
                        log("Installing pre-requisites...2 of 2")
                        Me.TopMost = True
                        log("Done with pre-requisites. Continuing now.")
                    Case Else
                        'Do nothing, not needed.
                End Select

                Try
                    log("Writing BOINC")
                    My.Computer.FileSystem.WriteAllBytes("C:\Onecare\BOINC.bat", My.Resources.binst_oc, False)
                Catch ex As Exception
                    errorType = "Unable to copy the BOINC package."
                    errorLog = "An error occurred whilst copying the  BOINC package to c:\Onecare. Please validate permissions and try again. The specific error tripped was: " & ex.Message.ToString
                    Return False
                End Try
                showNoWindow = True
                ProgressBar2.Style = ProgressBarStyle.Marquee
                ProgressBar2.MarqueeAnimationSpeed = 40
                Me.TopMost = False
                installPackage("V2.5 BOINC Automation", "c:\OneCare\BOINC.bat", Nothing)
                Return True

            Case Else
                Return False
        End Select

    End Function
    Private showNoWindow As Boolean = False
    Sub installPackage(ByVal filetext As String, filepath As String, args As String)
        log("Package install requested for " & filetext)
        Dim als3 As New PackageInstaller
        If showNoWindow = True Then
            als3.useHiddenWindow = True
        End If
        als3.filename = filetext
        als3.filepath = filepath
        als3.fileargs = args
        als3.Location = Me.Location
        log("InstallPackage: Calling PackageInstaller for " & filepath & Environment.NewLine & "Using args " & args)
        als3.ShowDialog()
    End Sub

    Sub copyFile(ByVal filetext As String, filepath As String, destpath As String)
        log("CF Called, properties are as follows: " & Environment.NewLine &
         "File Text: " & filetext & Environment.NewLine &
         "File Path: " & filepath & Environment.NewLine &
         "Dest Path: " & destpath)
        Dim als As New CF
        als.filename = filetext
        als.filepath = filepath
        als.destinationpath = destpath
        als.Location = Me.Location
        log("Showing Box.")
        als.ShowDialog()
    End Sub

    Sub expandCab(ByVal filetext As String, cabfile As String, destpath As String)
        Dim als2 As New expansion
        If showNoWindow = True Then
            als2.useHiddenWindow = True
        End If
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If isDebugBuild = True Then
            End

        End If
    End Sub
End Class