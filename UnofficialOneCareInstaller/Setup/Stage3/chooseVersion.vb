Public Class chooseVersion
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim als2 As Integer
        als2 = MsgBox("Are you sure you want to cancel the installation?" & Environment.NewLine & "Windows Live OneCare Unofficial installation is not complete. Click Yes to cancel the installation, or No to continue installing OneCare.", vbYesNoCancel, "Windows Live OneCare Unofficial")
        If als2 = vbYes Then
            SetupCancelled.Show()
            Me.Close()
        Else
            Me.BringToFront()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        tcsandcs.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton2.Checked = True Then
            globs.installx64insteadofx86 = False
        Else
            globs.installx64insteadofx86 = True
        End If
        Installation.Show()
        Me.Close()
    End Sub

    Private Sub chooseVersion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Me.Icon = Form1.Icon : Me.Text = Form1.Text
    End Sub
End Class