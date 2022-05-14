Public Class insttlog
    ' This file is cloned from the CoreGUI Project // cloud Lillith Platform (C) 2018-2022 All Rights Reserved.
    '
    '
    '


    Private Sub insttlog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = init1.Icon
        Me.Text = init1.Text
        Me.MinimumSize = Me.Size
        Me.MaximumSize = Me.Size
        Me.MaximizeBox = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveLogFile(FileIO.SpecialDirectories.Temp & "\ocrwinstlog.log", TextBox1)

    End Sub

    Sub SaveLogFile(ByVal logloc As String, box As TextBox)
        Try
            My.Computer.FileSystem.WriteAllText(logloc, box.Text, True)
        Catch ex As Exception

        End Try
    End Sub
End Class