Imports System.Data
Imports System.Data.SqlClient
Partial Public Class frmWelcome
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lbluser As System.Web.UI.WebControls.Label
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    'Protected WithEvents lblMsg As System.Web.UI.WebControls.Label
    'Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    'Protected WithEvents btnWasteWaterDeProduction As System.Web.UI.WebControls.Button
    'Protected WithEvents btnWasteWaterDeTest As System.Web.UI.WebControls.Button

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
        'lbluser.Text = StrConv(CType(Session.Item("WasteWaterDeUser"), String).Replace(".", " "), VbStrConv.ProperCase)
        Dim AllowLogins As String
        AllowLogins = "Y"
        'Dim myBldMsg As New clsBldAppMessages
        'lblMsg.Text = myBldMsg.BldAppMessages(6, AllowLogins)
        If AllowLogins = "N" Then
            btnLogin.Enabled = False
            btnNewUser.Enabled = False
        Else
            btnLogin.Enabled = True
            btnNewUser.Enabled = True
        End If

    End Sub

    Private Sub btnWasteWaterDeProduction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If rblProductionTest.SelectedValue = 1 Then
            Session("ConnStr") = System.Configuration.ConfigurationManager.ConnectionStrings("ProdConnStr").ConnectionString
            Session("Env") = "Production"
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Item("CromerrLogin") & "?ReportID=" & System.Configuration.ConfigurationManager.AppSettings.Item("ReportType"))
        Else
            Session("ConnStr") = System.Configuration.ConfigurationManager.ConnectionStrings("TestConnStr").ConnectionString
            Session("Env") = "Test"
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Item("CromerrLogin") & "?ReportID=" & System.Configuration.ConfigurationManager.AppSettings.Item("ReportType"))
        End If

    End Sub

    Private Sub btnWasteWaterDeTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewUser.Click
        If rblProductionTest.SelectedValue = 1 Then
            Session("ConnStr") = System.Configuration.ConfigurationManager.ConnectionStrings("ProdConnStr").ConnectionString
            Session("Env") = "Production"
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Item("CromerrNewRegistration") & "?ReportID=" & System.Configuration.ConfigurationManager.AppSettings.Item("ReportType"))
        Else
            Session("ConnStr") = System.Configuration.ConfigurationManager.ConnectionStrings("TestConnStr").ConnectionString
            Session("Env") = "Test"
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Item("CromerrNewRegistration") & "?ReportID=" & System.Configuration.ConfigurationManager.AppSettings.Item("ReportType"))
        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("Users Guide for Online DMR Reports.pdf")
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("Users Guide for Online DMR Reports.pdf")
    End Sub
End Class
