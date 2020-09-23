'Imports System.Web.Mail
Imports System.Net.Mail
Public Class frmErrorPage
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lblEnv As System.Web.UI.WebControls.Label
    'Protected WithEvents btnSend As System.Web.UI.WebControls.Button
    'Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtBody As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            lblEnv.Text = Session("Env")
            If Session("Env") = "Test" Then
                lblEnv.ForeColor = System.Drawing.Color.Red
            Else
                lblEnv.ForeColor = System.Drawing.Color.SeaGreen
            End If
        End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        SendMail(Session("AirDeUser") & "@state.de.us", ConfigurationManager.AppSettings("EMail1"), "", txtSubject.Text, txtBody.Text, "smtp.state.de.us")
        Response.Redirect("AirMainPage.aspx")
    End Sub
    Private Sub SendMail(ByVal FromEmailID As String, ByVal ToEmailID As String, ByVal CCList As String, ByVal Subject As String, ByVal Body As String, ByVal ServerName As String)
        If ToEmailID.Length > 0 Then
            Dim mail As New MailMessage()
            mail.From = New MailAddress(FromEmailID)
            mail.To.Add(ToEmailID)
            If Not CCList Is Nothing Then
                mail.CC.Add(CCList)
            End If
            mail.Subject = Subject
            mail.Body = Body
            mail.IsBodyHtml = True

            Dim smtpClient As New SmtpClient(ServerName)
            smtpClient.Send(mail)
            '            SmtpMail.SmtpServer = SeverName
            '           SmtpMail.Send(mail)
        End If

    End Sub
End Class
