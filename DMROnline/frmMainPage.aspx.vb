Option Compare Text
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Imports System.Data.SqlClient
'Imports clsCROMTOM


Public Class frmMainPage
    Inherits System.Web.UI.Page
    'Dim WithEvents FileTransferBase1 As New clsBase
    'Dim WithEvents FileTransferUpload1 As New clsUpload
    Dim PathID As String = ""
    Dim PermitID As String
    Dim PiID As Int32 = 0
    Dim lstWasteWaterSitesSelectedValue As String
    Protected WithEvents dgOutfalls As System.Web.UI.WebControls.DataGrid


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        If Session("ConnStr") Is Nothing Then
            Session("ConnStr") = System.Configuration.ConfigurationManager.ConnectionStrings("ProdConnStr").ConnectionString
        End If

        If Session("Env") Is Nothing Then
            Session("Env") = "Production"
        End If

        If (Not IsPostBack) Then 'Or (FileTransferBase1.PiID <> Request.QueryString("PiID"))





            Dim logintoken As String = Request.QueryString("Tkt")

            'FileTransferBase1.LoginToken = Request.QueryString("Tkt")

            'FileTransferBase1.PiID = Request.QueryString("PiID")
            'FileTransferBase1.AuthenticateUser()



            'If FileTransferBase1.ValidToken = 0 Then
            If Not clsDMRLogin.isValidToken(logintoken) Then
                Response.Redirect("frmWelcome.aspx")
            End If
            'FileTransferBase1.GetUserAndPiID()
            Dim AuthPiID As String = String.Empty '= FileTransferBase1.AuthPiID
            Dim AuthUserName As String = String.Empty '= FileTransferBase1.AuthUserName

            clsDMRLogin.GetUserAndPiID(logintoken, AuthUserName, AuthPiID)


            Session("WasteWaterDEUser") = AuthUserName
            Dim PiIDList As New List(Of String)
            PiIDList.AddRange(AuthPiID.Split(",".ToCharArray))

            'If Not (PiIDList.Contains(FileTransferBase1.PiID)) Then
            ' Response.Redirect("frmLogin.aspx")
            ' End If

            GetSites()
            lstWasteWaterSites.SelectedIndex = 0
            'If Session("ProgID") <> "" Then
            '    GetPermits(Session("ProgID"))
            '    lstWasteWaterSites.SelectedValue = Session("ProgID")
            'Else
            GetPermits(lstWasteWaterSites.SelectedValue)
            'End If

            dgPermits.SelectedIndex = 0
            Session("ParentPermitID") = "0"
            Session("PermitID") = dgPermits.SelectedItem.Cells(0).Text
            Session("ProgID") = lstWasteWaterSites.SelectedValue
            'Set Test vs Prod label
            lblEnv.Text = Session("Env")
            If Session("Env") = "Test" Then
                lblEnv.ForeColor = System.Drawing.Color.Red
            Else
                lblEnv.ForeColor = System.Drawing.Color.SeaGreen
            End If
        End If
        lstWasteWaterSitesSelectedValue = lstWasteWaterSites.SelectedValue
    End Sub

    Sub GetSites()
        Dim myProgInterest As New clsProgInterest
        lstWasteWaterSites.DataSource = myProgInterest.GetWasteWaterSites("Y", "Y", Request.QueryString("PiID"))
        lstWasteWaterSites.DataTextField = "NPDESIDName"
        lstWasteWaterSites.DataValueField = "ProgID"
        lstWasteWaterSites.DataBind()
    End Sub

    Sub GetPermits(ByVal ProgID As String)
        Dim myPermits As New clsPermit
        dgPermits.DataSource = myPermits.GetPermitsForSite(ProgID)
        dgPermits.DataBind()
    End Sub

    'Private Sub btnSiteDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSiteDetails.Click
    '    If lstWasteWaterSites.SelectedIndex <> -1 Then
    '        Dim myProgInterest As New clsProgInterest
    '        Server.Transfer("PlantInfo.aspx?PiID=" & myProgInterest.GetPiIDForProgID(lstWasteWaterSites.SelectedItem.Value))
    '    End If
    'End Sub

    'Private Sub rblShowAll_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblShowAll.SelectedIndexChanged
    '    GetSites()
    '    lstWasteWaterSites.SelectedValue = lstWasteWaterSitesSelectedValue
    'End Sub

    'Private Sub rblSort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSort.SelectedIndexChanged
    '    GetSites()
    '    lstWasteWaterSites.SelectedValue = lstWasteWaterSitesSelectedValue
    'End Sub

    Private Sub lstWasteWaterSites_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWasteWaterSites.SelectedIndexChanged
        GetPermits(lstWasteWaterSitesSelectedValue)
        dgPermits.SelectedIndex = 0
        'Session("PermitID") = dgPermits.SelectedItem.Cells(0).Text
        Session("ParentPermitID") = dgPermits.SelectedItem.Cells(0).Text
        Session("ProgID") = lstWasteWaterSites.SelectedValue
    End Sub

    Private Sub dgPermits_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPermits.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim myUnit As New clsUnit
            Dim dgUnits As DataGrid
            dgUnits = CType(e.Item.FindControl("dgOutfalls"), DataGrid)
            dgUnits.DataSource = myUnit.GetOutfalls(DataBinder.Eval(e.Item.DataItem, "PermitID"))
            dgUnits.DataBind()

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblPermitStatus As Label = CType(e.Item.FindControl("lblPermitStatus"), Label)
                If lblPermitStatus.Text = "Active" Then
                    e.Item.Font.Bold = True
                End If

            End If
        End If
    End Sub

    Protected Sub dgPermits_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgPermits.SelectedIndexChanged

    End Sub
End Class
