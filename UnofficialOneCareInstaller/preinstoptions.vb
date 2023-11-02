Public Class preinstoptions
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        preinst.Show()
        Me.Close()
    End Sub

    Private Sub preinstoptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim installocs As String() = {"c:\Program Files\Windows Live OneCare", "C:\Program Files (x86)\Windows Live OneCare"}
        Dim isfnd As Boolean = False
        For i = 0 To (installocs.Length - 1)
            If isfnd = True Then
                Exit For
            Else
                If My.Computer.FileSystem.DirectoryExists(installocs(i)) Then
                    isfnd = True
                    Button2.Enabled = True
                End If
            End If
        Next

    End Sub
End Class