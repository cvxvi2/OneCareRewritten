Public Class tcsandcs
    Dim tandcsStage As Integer = 0



    ' TODO
    ' Add hash check of Tcs&Cs from the disc to ensure it hasn't been tampered with.
    ' This will help prevent against exploits.


    Private Sub tcsandcs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MinimumSize = Me.Size : Me.MaximumSize = Me.Size
        Me.Icon = Form1.Icon
        Me.Text = Form1.Text
        initterms()
    End Sub


    Sub initterms()
        If tandcsStage = 0 Then
            log("Stage 0, showing my terms.")
        Else
            log("Stage 1, reading " & globs.installationmediapath & "eula.txt...")
            Try
                RichTextBox1.Text = My.Computer.FileSystem.ReadAllText(globs.installationmediapath & "eula.txt") 'RTF formats incorrectly
                log("Stage 1, reading " & globs.installationmediapath & "eula.txt...done!")
            Catch ex As Exception
                MsgBox("An error occurred whilst reading EULA.TXT from the installation media. Setup cannot continue.", 0 + 16, Me.Text)
                SetupCancelled.Label1.Text = SetupCancelled.Label1.Text & Environment.NewLine & "Files required for the installation could not be found."
                SetupCancelled.Show()
                Me.Close()
            End Try
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            If tandcsStage = 0 Then
                tandcsStage = 1
                Me.Hide()
                RadioButton1.Checked = False : RadioButton2.Checked = True

                initterms()
                Me.Show()
            Else
                If globs.isx64install = True Then
                    Installation.Show() 'Removed ChooseVersion, x64/x86 are not compatible with eachother at all and require separate packages.
                    Me.Close()
                Else
                    Installation.Show()
                    Me.Close()
                End If
                'move to install page
            End If
        Else
            MsgBox("You must agree to the terms to install Windows Live OneCare Unofficial.", 0 + 16, Me.Text)
        End If


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
End Class