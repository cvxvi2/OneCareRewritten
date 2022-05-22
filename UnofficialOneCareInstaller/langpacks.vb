Module langpacks
    Function getLangText(ByVal langcode As String, ByVal formname As String, ByVal reqlbl As String)
        'This is for the installer only and won't effect the actual installation, however, the langcode will be changed to install respective packages for Onecare.
        Dim errtxt As String = "An error occurred loading the language pack."
        log("[getLangText] Loading language pack code: " & langcode & " for " & formname & " requesting " & reqlbl)
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
