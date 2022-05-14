Public Class expansion
    Public filename, filepath, destpath As String
    Public useHiddenWindow As Boolean
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Timer1.Tag = 2 Then
            Timer1.Stop()
            Timer1.Tag = 0
            expandPackage()
        Else
            Timer1.Tag = Timer1.Tag + 1
            ProgressBar1.Increment(10)
        End If
    End Sub

    Private Sub CF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = Installation.Location
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Label4.Text = Label4.Text.Replace("{PKG}", filename)
        Me.Icon = Form1.Icon
        Me.Text = "Expanding files... | " & Form1.Text
        Timer1.Start()
    End Sub


    Sub expandPackage()
        Dim expm As New ProcessStartInfo
        With expm
            .FileName = "c:\Windows\System32\expand.exe"
            .Arguments = filepath & " -F:* " & destpath & " -R:*.MSI"
        End With
        If useHiddenWindow = True Then
            With expm
                .CreateNoWindow = True
                .UseShellExecute = False
                .WindowStyle = ProcessWindowStyle.Hidden
            End With
        Else
        End If
        Dim expander As Process = Process.Start(expm)
        expander.WaitForExit()
        Me.Close()
    End Sub
End Class