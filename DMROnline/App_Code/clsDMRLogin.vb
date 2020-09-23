Imports DMROnline

Public Class clsDMRLogin



    Public Shared Function isValidToken(ByVal loginToken As String) As Boolean

        Dim ValidToken As Integer = 0
        Dim ws As New wsCROMTOM.wsCROMTOMSoapClient

        ValidToken = ws.AuthenticateToken(loginToken)

        If ValidToken = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Sub GetUserAndPiID(ByVal loginToken As String, ByRef username As String, ByRef piids As String)
        Dim ws As New wsCROMTOM.wsCROMTOMSoapClient

        ws.ReturnUser(loginToken, username, piids)

    End Sub

End Class
