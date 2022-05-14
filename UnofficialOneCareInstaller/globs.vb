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


    Function getInstallationMediaPath(ByVal setGlobs As Boolean)









        Select Case setGlobs
            Case True

            Case False

        End Select
    End Function



    Function detectDiscVersion(ByVal setGlobs As Boolean)



        Select Case setGlobs
            Case True

            Case False

        End Select
    End Function
End Module
