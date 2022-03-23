Public Class SetupComplete
    Private Sub SetupComplete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Me.Text = Form1.Text
        Me.Icon = Form1.Icon
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox1.Checked = True Then
            Form1.Show()
            Process.Start("shutdown", "-r -t 00")
            Me.Close()
        Else
            End
        End If
    End Sub
End Class