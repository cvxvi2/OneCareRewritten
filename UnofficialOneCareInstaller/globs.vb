Module globs
    Public installationmediapath As String = Nothing 'Auto-detects on init1.
    Public OSType As String = "Vista" 'Detected OS type, handled by init1
    Public isx64install As Boolean = False 'handled by chooseVersion
    Public installx64insteadofx86 As Boolean = False 'handled by chooseVersion
    Public isBinstInstall As Boolean = False 'handled by preinst. Attempts to run boinc.
    Public discVersion As String = "0.00"
    Public isDebugBuild As Boolean = True
    Public bypassOSCheck As Boolean = False
    Public Sub log(ByVal data As String)
        Try
            insttlog.TextBox1.AppendText(Environment.NewLine & data.ToString)
        Catch ex As Exception
        End Try
    End Sub


    Function getLangText(ByVal langcode As String, ByVal formname As String, ByVal reqlbl As String)
        Dim errtxt As String = "An error occurred loading the language pack."
        Select Case langcode
            Case "en-gb"
                Select Case formname
                    Case "preinst"
                        Select Case reqlbl
                            Case "Label2"
                                Return "Welcome to the preinstallation environment for Windows Live OneCare Rewritten by Cobs." & Environment.NewLine & Environment.NewLine &
                                    "Installation Notes:" & Environment.NewLine & Environment.NewLine &
                                    "-If you're attempting a reinstall after a failed initial install, please delete %temp%\OCSetup" & Environment.NewLine &
                                    "and c:\OneCare" & Environment.NewLine &
                                    "-Windows XP is only supported with 2.5 discs on SP3." & Environment.NewLine &
                                    "-Backup is only functional on V1.5 Gold discs." & Environment.NewLine & Environment.NewLine & Environment.NewLine &
                                    "Requirements:" & Environment.NewLine &
                                    "-Microsoft .Net 4.0" & Environment.NewLine &
                                    "-Windows Vista or Windows XP 32-Bit (SP3)" & Environment.NewLine &
                                    "-Windows Live ID Assistant Service" & Environment.NewLine &
                                    "-Windows Live OneCare disc in optical drive or virtually mounted"
                            Case Else
                                Return errtxt
                        End Select
                    Case Else
                        Return errtxt
                End Select


            Case "fr-fr"
                Select Case formname
                    Case "preinst"
                        Select Case reqlbl
                            Case "label2"
                            Case Else
                                Return errtxt
                        End Select
                    Case Else
                        Return errtxt
                End Select
            Case Else
                Return errtxt
        End Select


    End Function
End Module
