Public Class PackageInstaller
    Public filename, filepath, fileargs As String
    Private Sub PackageInstaller_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Label4.Text = Label4.Text.Replace("{PKG}", filename)
        Me.Icon = Form1.Icon
        Me.Text = "Installing Package | " & Form1.Text
        Timer1.Start()
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Timer1.Tag = 2 Then
            Timer1.Stop()
            Timer1.Tag = 0
            InstallPackage()
        Else
            Timer1.Tag = Timer1.Tag + 1
            ProgressBar1.Increment(10)
        End If
    End Sub

    Sub InstallPackage()
        Dim ipm As New ProcessStartInfo
        With ipm
            .FileName = filepath
            .Arguments = fileargs
        End With
        Dim expander As Process = Process.Start(ipm)
        expander.WaitForExit()
        Me.Close()



    End Sub


End Class