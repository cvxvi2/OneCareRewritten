Public Class SetupCancelled
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub SetupCancelled_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Try
            TextBox1.Text = insttlog.TextBox1.Text
        Catch ex As Exception
            TextBox1.Text = "Unable to get the installation log: " & ex.Message.ToString
        End Try
    End Sub
End Class