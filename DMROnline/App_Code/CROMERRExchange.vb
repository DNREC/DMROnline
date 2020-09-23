Imports System.IO
Imports System.Security.Cryptography
Imports System.Web.Services.Protocols
Imports DMROnline

Public Class CROMERRExchange
    Public Event TransferCompleted(ByVal sender As Object, ByVal e As CROMERREventArgs)
    Public Event TransferFailed(ByVal sender As Object, ByVal e As CROMERREventArgs)



    ''' <summary>
    ''' upload report to CromErr
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendReportToCROMERR(ByVal bp As Page, ByVal loginToken As String, ByVal report() As Byte, ByVal filename As String, reportid As Integer, piid As Integer, comments As String, SequenceRef As String) As Boolean
        Dim returnval As Boolean = False
        If report Is Nothing Then
            Return returnval
        End If

        Dim ws As New wsCROMTOM.wsCROMTOMSoapClient


        Dim ValidToken As Integer = 0
        Dim LocalFileHash As String = String.Empty
        Dim RemoteFileHash As String = String.Empty
        Dim RedirectPath As String = String.Empty

        Try
            'validate the token before uploading the document
            ValidToken = ws.AuthenticateToken(loginToken)


            'ws.UploadDocument(login.submissionid.ToString() & ".pdf", report)

            'RedirectPath = Convert.ToString(ws.SaveFile(login.submissionid.ToString() & ".pdf", LocalFileHash, login.loginToken, login.reportid, 0, bp.Request.UserHostAddress, CommonFunctions.getText(TypeCodeTableCache.typeCodeTableGet(TypeTables.SubmissionTypeCodes, bp), bp.submission(login.submissionid).submissionType), login.submissionid))

            RedirectPath = Convert.ToString(ws.SubmitDocument(loginToken, filename, report, reportid, piid, "", "", bp.Request.UserHostAddress.ToString(), comments, SequenceRef, 1, String.Empty))

            If ValidToken <> 0 AndAlso RedirectPath <> String.Empty AndAlso InStr(RedirectPath, "Error") = 0 Then
                RaiseTransferCompletedEvent(New CROMERREventArgs(RedirectPath))
                returnval = True
            Else
                If ValidToken = 0 Then
                    RaiseTransferFailedEvent(New CROMERREventArgs(New Exception("Login token is not valid")))
                ElseIf RedirectPath = String.Empty Then
                    RaiseTransferFailedEvent(New CROMERREventArgs(New Exception("Redirect Path is not valid")))
                Else
                    RaiseTransferFailedEvent(New CROMERREventArgs(New Exception(RedirectPath)))
                End If
                returnval = False
            End If


        Catch cromerrwserr As SoapException
            Return False
        Catch ex As Exception
            Return False
        Finally

        End Try

        Return returnval
    End Function
    ''' <summary>
    ''' raises a complete event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RaiseTransferCompletedEvent(ByVal e As CROMERREventArgs)
        RaiseEvent TransferCompleted(Me, e)
    End Sub

    ''' <summary>
    ''' raises a tansfer file failed event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RaiseTransferFailedEvent(ByVal e As CROMERREventArgs)
        RaiseEvent TransferFailed(Me, e)
    End Sub

End Class
