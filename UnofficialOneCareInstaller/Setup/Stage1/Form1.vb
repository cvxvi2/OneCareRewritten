Public Class Form1
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        tcsandcs.Show()
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If isx64install Then
            Me.Text = Me.Text & " (x64)"
        End If
        If globs.discVersion = "2.5" Then
            Me.Text = Me.Text & " (OC V2.5)"
        Else
            Me.Text = Me.Text & " (OC V2.0)"
        End If
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
    End Sub
End Class
