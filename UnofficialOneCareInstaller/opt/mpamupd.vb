Public Class mpamupd
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("No file was selected.", MessageBoxIcon.Warning)
        Else
            Dim als = MsgBox("Update should only take a few seconds, do you want to continue?", vbYesNoCancel, "Update OneCare Antivirus Definitions")
            If als = vbYes Then
                Try
                    Dim aupd = Process.Start(OpenFileDialog1.FileName, "/Q ONECARE")
                    aupd.WaitForExit()
                    MsgBox("If no errors are displayed, the update should have succeeded. Check Event Viewer for more information.", MsgBoxStyle.Information)
                    Me.Close()
                Catch ex As Exception
                    MsgBox("An error occurred, please consider sending this to the developers:" & Environment.NewLine & ex.Message.ToString, MsgBoxStyle.Critical)
                End Try
            End If
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

    Private Sub mpamupd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = preinst.Icon
        Me.MinimumSize = Me.Size
        Me.MaximumSize = Me.Size
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
End Class