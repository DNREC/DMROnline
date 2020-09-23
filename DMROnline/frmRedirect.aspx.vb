
Partial Class frmRedirect
    'Inherits ProcessBasePage
    Inherits System.Web.UI.Page

    Protected Sub btnCromErr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCromErr.Click
        'Response.Redirect(Session("RedirectPath"))
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Item("CromerrLogin") & "?Tkt=" & Request.QueryString("Tkt") & "&ReportID=" & System.Configuration.ConfigurationManager.AppSettings.Item("ReportType"))

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("NewPath") = System.Configuration.ConfigurationManager.AppSettings.Item("CromerrLogin") & "?Tkt=" & Request.QueryString("Tkt") & "&ReportID=" & System.Configuration.ConfigurationManager.AppSettings.Item("ReportType")

    End Sub
End Class
