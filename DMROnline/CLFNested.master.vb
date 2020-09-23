Public Class CLFNested
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hlDMROnlineSite.NavigateUrl = ConfigurationManager.AppSettings("DMROnlineSite").ToString()
        End If
    End Sub

End Class