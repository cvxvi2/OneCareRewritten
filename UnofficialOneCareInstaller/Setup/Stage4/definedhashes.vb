Module definedhashes

    'Below will check the hashes of exported CABS to make sure A) they haven't been tampered with B) the disc isn't damaged




    Function validateHash(ByVal PackageName, ByVal reportedHash)
        Installation.log("[Validate Hash] Hash check requested for " & PackageName & " currently reporting " & reportedHash)
        Dim ValidHashes As String() = {"", "", ""}
        Dim validoutput As Boolean = False
        For i = 0 To (ValidHashes.Length - 1)
            If ValidHashes(i) = reportedHash Then
                validoutput = True
                Exit For
            End If
        Next
        Select Case validoutput
            Case True
                Installation.log("[Validate Hash] Hash correct for " & PackageName & "Hash " & reportedHash & " is a match.")
                Return True
            Case Else
                Installation.log("[Validate Hash] ERROR. Incorrect for " & PackageName & "Hash " & reportedHash & " does not match stored hashes. Cancelling installation.")
                'ask user if they wish to cancel or not
                Return False
        End Select

    End Function




End Module
