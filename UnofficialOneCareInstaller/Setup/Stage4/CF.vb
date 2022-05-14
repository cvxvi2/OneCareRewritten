Public Class CF
    Public filename, filepath, destinationpath As String

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Timer1.Tag = 1 Then
            Timer1.Stop()
            Timer1.Tag = 0
            copyCF()
        Else
            Timer1.Tag = Timer1.Tag + 1
            ProgressBar1.Increment(10)
        End If
    End Sub

    Private Sub CF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = Installation.Location
        Label1.Text = "Copying " & filepath & " to " & destinationpath
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Label4.Text = Label4.Text.Replace("{PKG}", filename)
        Me.Icon = Form1.Icon
        Me.Text = "Copying files... | " & Form1.Text
        Timer1.Start()
    End Sub

    Sub copyCF()
        Try
            My.Computer.FileSystem.CopyFile(filepath, destinationpath, True)
            ProgressBar1.Increment(ProgressBar1.Maximum - ProgressBar1.Value)
            Me.Close()
        Catch ex As Exception
            SetupCancelled.Show()
            SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & Environment.NewLine & "Setup was unable to locate or copy a required file: " & ex.Message.ToString
            Installation.Close()
            Me.Close()
        End Try
    End Sub

End Class