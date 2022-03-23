Public Class preinst
    Private Sub preinst_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Me.Icon = Form1.Icon
        Me.MaximizeBox = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        init1.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        globs.isBinstInstall = True
        init1.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Process.Start("appwiz.cpl")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.SelectedTab = TabPage3


    End Sub
End Class