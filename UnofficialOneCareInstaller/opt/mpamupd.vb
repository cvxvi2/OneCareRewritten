Public Class mpamupd
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("No file was selected.", MessageBoxIcon.Warning)
        Else

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            'MsgBox(OpenFileDialog1.FileName.ToString)
            If OpenFileDialog1.FileName.ToString.Contains("mpam-fe.exe") Then
                '   MsgBox("MPAM-FE Loaded. Push Next to continue.")
            Else
                MsgBox("It is not advised, but, you can continue, however the filename should remain mpam-fe. ", MsgBoxStyle.Information)
            End If
            TextBox1.Text = OpenFileDialog1.FileName.ToString
        Else

        End If

    End Sub
End Class