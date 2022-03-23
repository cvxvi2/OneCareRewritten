Public Class insttlog
    Private Sub insttlog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = init1.Icon
        Me.Text = init1.Text
        Me.MinimumSize = Me.Size
        Me.MaximumSize = Me.Size
        Me.MaximizeBox = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            My.Computer.FileSystem.WriteAllText(FileIO.SpecialDirectories.Temp & "\ocrwinstlog.log", Environment.NewLine & Environment.NewLine & "Cobs Windows Live OneCare Rewritten installer log saved @ " & TimeOfDay & Environment.NewLine & TextBox1.Text, True)
        Catch ex As Exception

        End Try
    End Sub
End Class