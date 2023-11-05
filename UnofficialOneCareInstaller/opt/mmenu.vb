Imports Microsoft.VisualBasic.Logging

Public Class mmenu
    Private basedir As String = "C:\Program Files\Microsoft Windows OneCare Live\"
    Private Sub mmenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = preinst.Icon
        If My.Computer.FileSystem.FileExists("C:\Program Files\Microsoft Windows OneCare Live\supportlog_tile.png") Then
            Try
                PictureBox1.BackgroundImage = Image.FromFile("C:\Program Files\Microsoft Windows OneCare Live\supportlog_tile.png")
                PictureBox1.BackgroundImageLayout = ImageLayout.Tile
                PictureBox1.Image = Image.FromFile("C:\Program Files\Microsoft Windows OneCare Live\supportlog_header.png")
                PictureBox1.Size = New Point(542, 48)
            Catch ex As Exception
                PictureBox1.Image = My.Resources.banner
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

            End Try

        Else

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Process.Start(basedir & "WinSSUI.exe")
        Catch ex As Exception
            MsgBox("Unable to load Windows Live OneCare, see below for further information:" &
                   Environment.NewLine & ex.Message.ToString, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Process.Start(basedir & "\Antivirus\MpCmdRun.exe", "-SignatureUpdate")
        Catch ex As Exception
            MsgBox("Unable to load Windows Live OneCare, see below for further information:" &
                   Environment.NewLine & ex.Message.ToString, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        mpamupd.ShowDialog()

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            Process.Start("explorer", basedir & "\Logs")
        Catch ex As Exception
            MsgBox("Unable to load Windows Live OneCare, see below for further information:" &
                   Environment.NewLine & ex.Message.ToString, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim scantype = 0
        If RadioButton1.Checked = True Then
            scantype = 0
        ElseIf RadioButton2.Checked = True Then
            scantype = 1
        Else
            scantype = 2
        End If
        Try
            Process.Start(basedir & "\Antivirus\MpCmdRun.exe", "-Scan -ScanType " & scantype.ToString)
            MsgBox("The scan has started. You may see a blank command prompt on-screen. Closing this will interupt the scan.", MsgBoxStyle.Information)


        Catch ex As Exception
            MsgBox("Unable to load Windows Live OneCare, see below for further information:" &
                   Environment.NewLine & ex.Message.ToString, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class