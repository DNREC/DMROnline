Imports System.Data.SqlClient
Imports System.Text
Imports System.Data
Imports System.Net


Imports System
Imports System.IO
Imports System.Threading
Imports System.Security.Cryptography
Imports System.ServiceModel.Dispatcher
Imports DMROnline.DMZReports

Partial Class frmDMREntry
    Inherits System.Web.UI.Page

    'Public Event TransferCompleted(ByVal CromErrDocumentURL As String)
    'Public Event TransferFailed(ByVal e As Exception)

    'Dim WithEvents FileTransferBase1 As New clsBase
    'Dim WithEvents FileTransferUpload1 As New clsUpload
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents lblPermit As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDMRMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGetData As System.Web.UI.WebControls.Button
    Protected WithEvents ddlDMRYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtMonth As System.Web.UI.WebControls.TextBox
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents revAvgQ As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents btnNoLimitDataEntry As System.Web.UI.WebControls.Button
    Protected WithEvents lblOutfall As System.Web.UI.WebControls.Label
    'Protected WithEvents btnBulkSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnPg15 As System.Web.UI.WebControls.Button
    Protected WithEvents btnPg16 As System.Web.UI.WebControls.Button
    Protected WithEvents btnPg17 As System.Web.UI.WebControls.Button
    Protected WithEvents btnPg18 As System.Web.UI.WebControls.Button
    Protected WithEvents chkSaveOnPageChg As System.Web.UI.WebControls.CheckBox

    Private DisabledColor As System.Drawing.Color = System.Drawing.Color.Gainsboro

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lblEnv As System.Web.UI.WebControls.Label
    Protected WithEvents lblReports As System.Web.UI.WebControls.Label
    Protected WithEvents lblAWM As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents ddlParam As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVMC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMUnits As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFreq As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonitorLoc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSampleType As System.Web.UI.WebControls.DropDownList

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


        If (Not IsPostBack) Then 'Or (FileTransferBase1.PiID <> Request.QueryString("PiID"))

            Dim logintoken As String = Request.QueryString("Tkt")
            Dim piid As Integer = Request.QueryString("PiID")

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
            '    Response.Redirect("frmLogin.aspx")
            'End If

            'End If



            'If (Not IsPostBack) Or (FileTransferBase1.PiID <> Request.QueryString("PiID")) Then


            Session("UnitID") = Request.QueryString("UnitID")
            Session("PermitID") = Request.QueryString("PermitID")


            'Session("Status") = "N" 'to be uncommented
            Session("strData") = ""
            Session("strData2") = ""
            'Session("SaveOverride") = "N"

            Dim myDMR As New clsDMR

            ddlDMRNoDischargeReason.DataSource = myDMR.GetNoDataIndicator
            ddlDMRNoDischargeReason.DataBind()
            ddlDMRNoDischargeReason.SelectedValue = " "

            ddlYrMonth.DataSource = CreateYearMonthDataView()
            ddlYrMonth.DataBind()

            If Request.QueryString("DMRDate") Is Nothing Then
                'Set starting monitoring period to the next month after the last month with any data
                Dim YrMonth As String = myDMR.GetNextDMRDate(Session("UnitID"))
                Dim Yr As String = YrMonth.Substring(0, 4)
                Dim Month As String = YrMonth.Substring(4)
                If Yr < Now.Year Then
                    ddlYrMonth.SelectedValue = YrMonth
                ElseIf Yr = Now.Year And Month < Now.Month Then
                    ddlYrMonth.SelectedValue = YrMonth
                ElseIf Yr = Now.Year And Month = Now.Month And Now.Month = 1 Then
                    ddlYrMonth.SelectedValue = (Now.Year - 1).ToString + "12"
                End If
            Else
                ddlYrMonth.SelectedValue = Request.QueryString("DMRDate")
            End If


            'Session("DateReceived") = txtDateReceived.Text
            'Load data for starting monitoring period
            Session("DisplayPage") = "1"
            GetData(Session("DisplayPage"))
            btnPg1.ForeColor = System.Drawing.Color.Black

            If txtDateReceived.Text = "" And Not Request.QueryString("RecDate") Is Nothing Then
                txtDateReceived.Text = Request.QueryString("RecDate")
            End If

            If txtDateReceived.Text = "" Then
                txtDateReceived.Text = Now.ToShortDateString  ' DatePart(DateInterval.Month, Now()).ToString + "/" + DatePart(DateInterval.Day, Now()).ToString + "/" + DatePart(DateInterval.Year, Now()).ToString
            End If


            'Fill in site info on header
            lblSiteOutfall.Text = myDMR.GetSiteOutfall(Session("UnitID"))

            ''Fill Other outfall list
            Dim myUnit As New clsUnit
            ddlOutfall.DataSource = myUnit.GetOutfalls(Session("PermitID"))
            ddlOutfall.DataBind()
            ddlOutfall.SelectedValue = Session("UnitID")

            'Set Test vs Prod label
            lblEnv.Text = Session("Env")
            If Session("Env") = "Test" Then
                lblEnv.ForeColor = System.Drawing.Color.Red
            Else
                lblEnv.ForeColor = System.Drawing.Color.SeaGreen
            End If
        End If

    End Sub

    Private Sub ddlOutfall_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlOutfall.SelectedIndexChanged
        Dim Token As String
        Token = HttpUtility.UrlEncode(Request.QueryString("Tkt"))

        Call BulkSave()

        Server.Transfer("frmDMREntry.aspx?Tkt=" & Token & "&PiID=" & Request.QueryString("PiID") & "&ReportID=" & Request.QueryString("ReportID") & "&UnitID=" & ddlOutfall.SelectedValue & "&DMRDate=" & ddlYrMonth.SelectedValue & "&RecDate=" & txtDateReceived.Text & "&PermitID=" & Session("PermitID"))
    End Sub

    Function CreateYearMonthDataView() As ICollection
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("YrMon", GetType(String)))
        dt.Columns.Add(New DataColumn("MonthYear", GetType(String)))

        Dim i As Integer
        Dim j As Integer
        For i = Now.Year - 1 To Now.Year
            For j = 1 To 12
                dr = dt.NewRow()

                dr(0) = i.ToString + j.ToString
                Select Case j
                    Case 1
                        dr(1) = "Jan " + i.ToString
                    Case 2
                        dr(1) = "Feb " + i.ToString
                    Case 3
                        dr(1) = "Mar " + i.ToString
                    Case 4
                        dr(1) = "Apr " + i.ToString
                    Case 5
                        dr(1) = "May " + i.ToString
                    Case 6
                        dr(1) = "Jun " + i.ToString
                    Case 7
                        dr(1) = "Jul " + i.ToString
                    Case 8
                        dr(1) = "Aug " + i.ToString
                    Case 9
                        dr(1) = "Sep " + i.ToString
                    Case 10
                        dr(1) = "Oct " + i.ToString
                    Case 11
                        dr(1) = "Nov " + i.ToString
                    Case 12
                        dr(1) = "Dec " + i.ToString
                End Select
                If i = Now.Year And j = Now.Month Then
                    Exit For
                Else
                    dt.Rows.Add(dr)
                End If
            Next j
        Next i

        Dim dv As New DataView(dt)
        Return dv
    End Function

    Sub SetUpDataEntry(ByVal txtbox As TextBox, ByVal ddl As DropDownList,
     ByVal ddlValue As String, ByVal LimitVMC As String, ByVal HasLimit As Boolean,
    ByVal RequiredThisMonth As Boolean)
        Dim myDMR As New clsDMR


        ddl.Items.FindByValue(ddlValue).Selected = True
        'Gray out data entry if no limit present
        If HasLimit = True And LimitVMC <> "Z" And RequiredThisMonth = True Then
            ddl.Enabled = True
            ddl.Visible = True
            txtbox.Enabled = True
            txtbox.Visible = True
        Else
            ddl.TabIndex = -1
            ddl.BackColor = DisabledColor
            ddl.Enabled = False
            ddl.Visible = False
            txtbox.TabIndex = -1
            txtbox.BackColor = DisabledColor
            txtbox.Enabled = False
            txtbox.Visible = False
        End If
    End Sub

    Public Sub dgDMR_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim myDMR As New clsDMR
            Dim ddlNDI As DropDownList = CType(e.Item.FindControl("ddlNDI"), DropDownList)
            ddlNDI.DataSource = myDMR.GetNoDataIndicatorCodes
            ddlNDI.DataBind()
            ddlNDI.Items.FindByValue(CType(e.Item.FindControl("lblNDI"), Label).Text).Selected = True
            Dim RequiredThisMonth As Boolean
            If CType(e.Item.FindControl("lblLimitMonthsRequired"), Label).Text.Substring(CInt(ddlYrMonth.SelectedValue.Substring(4)) - 1, 1) = "N" Then
                RequiredThisMonth = False
            Else
                RequiredThisMonth = True
            End If

            '*** AvgQuant
            Call SetUpDataEntry(CType(e.Item.FindControl("txtAvgQ"), TextBox),
                CType(e.Item.FindControl("ddlAvgQVMC"), DropDownList),
                CType(e.Item.FindControl("lblAvgQVMC"), Label).Text,
                 CType(e.Item.FindControl("lblLimitAvgQVMC"), Label).Text,
                CType(e.Item.FindControl("lblAvgQuantLimitID"), Label).Text.Length > 0,
                 RequiredThisMonth)
            '*** MaxQuant
            Call SetUpDataEntry(CType(e.Item.FindControl("txtMaxQ"), TextBox),
                CType(e.Item.FindControl("ddlMaxQVMC"), DropDownList),
                CType(e.Item.FindControl("lblMaxQVMC"), Label).Text,
                CType(e.Item.FindControl("lblLimitMaxQVMC"), Label).Text,
                CType(e.Item.FindControl("lblMaxQuantLimitID"), Label).Text.Length > 0,
                 RequiredThisMonth)
            '*** MinConc
            Call SetUpDataEntry(CType(e.Item.FindControl("txtMinC"), TextBox),
                CType(e.Item.FindControl("ddlMinCVMC"), DropDownList),
                CType(e.Item.FindControl("lblMinCVMC"), Label).Text,
                CType(e.Item.FindControl("lblLimitMinCVMC"), Label).Text,
                CType(e.Item.FindControl("lblMinConcLimitID"), Label).Text.Length > 0,
                 RequiredThisMonth)
            '*** AvgConc
            Call SetUpDataEntry(CType(e.Item.FindControl("txtAvgC"), TextBox),
                CType(e.Item.FindControl("ddlAvgCVMC"), DropDownList),
                CType(e.Item.FindControl("lblAvgCVMC"), Label).Text,
                CType(e.Item.FindControl("lblLimitAvgCVMC"), Label).Text,
                CType(e.Item.FindControl("lblAvgConcLimitID"), Label).Text.Length > 0,
                 RequiredThisMonth)
            '*** MaxConc
            Call SetUpDataEntry(CType(e.Item.FindControl("txtMaxC"), TextBox),
                CType(e.Item.FindControl("ddlMaxCVMC"), DropDownList),
                CType(e.Item.FindControl("lblMaxCVMC"), Label).Text,
                CType(e.Item.FindControl("lblLimitMaxCVMC"), Label).Text,
                CType(e.Item.FindControl("lblMaxConcLimitID"), Label).Text.Length > 0,
                 RequiredThisMonth)
            ''*** Frequency
            'Call SetUpDataEntry(CType(e.Item.FindControl("txtMaxC"), TextBox), _
            '    CType(e.Item.FindControl("ddlMaxCVMC"), DropDownList), _
            '    CType(e.Item.FindControl("lblMaxCVMC"), Label).Text, _
            '    CType(e.Item.FindControl("lblLimitMaxCVMC"), Label).Text, _
            '    CType(e.Item.FindControl("lblMaxConcLimitID"), Label).Text.Length > 0, _
            '    RequiredThisMonth)
            ''*** SampleType
            'Call SetUpDataEntry(CType(e.Item.FindControl("txtMaxC"), TextBox), _
            '    CType(e.Item.FindControl("ddlMaxCVMC"), DropDownList), _
            '    CType(e.Item.FindControl("lblMaxCVMC"), Label).Text, _
            '    CType(e.Item.FindControl("lblLimitMaxCVMC"), Label).Text, _
            '    CType(e.Item.FindControl("lblMaxConcLimitID"), Label).Text.Length > 0, _
            '    RequiredThisMonth)

            '*** if no data entry is required for any text box (i.e. tabindex=-1) in this row then disable NDI and Excursion text boxes
            If (CType(e.Item.FindControl("txtMaxC"), TextBox).TabIndex = -1 And
                CType(e.Item.FindControl("txtAvgC"), TextBox).TabIndex = -1 And
                CType(e.Item.FindControl("txtMinC"), TextBox).TabIndex = -1 And
                CType(e.Item.FindControl("txtMaxQ"), TextBox).TabIndex = -1 And
                CType(e.Item.FindControl("txtAvgQ"), TextBox).TabIndex = -1) _
                Then
                CType(e.Item.FindControl("txtNumberOfExcursions"), TextBox).Enabled = False
                ddlNDI.Enabled = False
            End If

            'Set display of limits
            CType(e.Item.FindControl("lblAvgQuantLimit"), Label).Visible = chkShowLimits.Checked
            CType(e.Item.FindControl("lblMaxQuantLimit"), Label).Visible = chkShowLimits.Checked
            CType(e.Item.FindControl("lblMinConcLimit"), Label).Visible = chkShowLimits.Checked
            CType(e.Item.FindControl("lblAvgConcLimit"), Label).Visible = chkShowLimits.Checked
            CType(e.Item.FindControl("lblMaxConcLimit"), Label).Visible = chkShowLimits.Checked


            'added the below code to avoid sessions in multiple tabs.
            'Dim ActID As Integer
            'Dim CurrentStatus As String = String.Empty
            'If CType(e.Item.FindControl("lblActID"), Label).Text <> String.Empty Then
            '    ActID = CType(e.Item.FindControl("lblActID"), Label).Text
            '    CurrentStatus = CType(e.Item.FindControl("lblCurrentStatus"), Label).Text
            'End If


            'If Session("ActID") Is Nothing OrElse Session("ActID") <> ActID Then
            '        Session("ActID") = ActID
            '        Session("Status") = CurrentStatus
            '    End If
        End If
    End Sub

    Private Sub GetPageData(ByRef strData As String, ByRef strData2 As String, ByRef DateReceived As String, ByRef Comment As String)
        Dim dgi As DataGridItem
        Dim strbldData As StringBuilder = New StringBuilder(500)
        Dim strbldData2 As StringBuilder = New StringBuilder(500)

        DateReceived = txtDateReceived.Text
        Comment = txtComments.Text

        For Each dgi In dgDMR.Items
            If dgi.Visible = True Then
                'Check if there is any real data and save only if there is
                If CType(dgi.FindControl("lblActID"), Label).Text <> "" _
                    Or CType(dgi.FindControl("txtMinC"), TextBox).Text <> "" _
                    Or CType(dgi.FindControl("ddlNDI"), DropDownList).SelectedItem.Text <> " " _
                    Or CType(dgi.FindControl("ddlMinCVMC"), DropDownList).SelectedItem.Text <> " " _
                    Or CType(dgi.FindControl("txtAvgC"), TextBox).Text <> "" _
                    Or CType(dgi.FindControl("ddlAvgCVMC"), DropDownList).SelectedItem.Text <> " " _
                    Or CType(dgi.FindControl("txtMaxC"), TextBox).Text <> "" _
                    Or CType(dgi.FindControl("ddlMaxCVMC"), DropDownList).SelectedItem.Text <> " " _
                    Or CType(dgi.FindControl("txtAvgQ"), TextBox).Text <> "" _
                    Or CType(dgi.FindControl("ddlAvgQVMC"), DropDownList).SelectedItem.Text <> " " _
                    Or CType(dgi.FindControl("txtMaxQ"), TextBox).Text <> "" _
                    Or CType(dgi.FindControl("ddlMaxQVMC"), DropDownList).SelectedItem.Text <> " " _
                     Then

                    Dim NDR As String = CType(dgi.FindControl("ddlNDI"), DropDownList).SelectedItem.Text

                    Dim MinCNDI As String = ""
                    Dim AvgCNDI As String = ""
                    Dim MaxCNDI As String = ""
                    Dim AvgQNDI As String = ""
                    Dim MaxQNDI As String = ""
                    If CType(dgi.FindControl("txtMinC"), TextBox).TabIndex > -1 Then
                        MinCNDI = NDR
                    End If
                    If CType(dgi.FindControl("txtAvgC"), TextBox).TabIndex > -1 Then
                        AvgCNDI = NDR
                    End If
                    If CType(dgi.FindControl("txtMaxC"), TextBox).TabIndex > -1 Then
                        MaxCNDI = NDR
                    End If
                    If CType(dgi.FindControl("txtAvgQ"), TextBox).TabIndex > -1 Then
                        AvgQNDI = NDR
                    End If
                    If CType(dgi.FindControl("txtMaxQ"), TextBox).TabIndex > -1 Then
                        MaxQNDI = NDR
                    End If


                    If strData.Length < 7500 Then
                        Session("ActID") = CType(dgi.FindControl("lblActID"), Label).Text
                        Session("Status") = CType(dgi.FindControl("lblCurrentStatus"), Label).Text
                        strbldData.Append(CType(dgi.FindControl("lblActResultsSetID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblLimitMonitorLocCode"), Label).Text)
                        strbldData.Append("|")
                        'strbldData.Append(CType(dgi.FindControl("lblLimitSampleTypeCode"), Label).Text)
                        strbldData.Append(CType(dgi.FindControl("ddlSampleType"), DropDownList).SelectedValue.ToString)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblLimitGroup"), Label).Text)
                        strbldData.Append("|")
                        'strbldData.Append(CType(dgi.FindControl("lblLimitSampleFreqID"), Label).Text)
                        strbldData.Append(CType(dgi.FindControl("ddlSampleFreq"), DropDownList).SelectedValue.ToString)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblParamID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblMinConcActResultsID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("txtMinC"), TextBox).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("ddlMinCVMC"), DropDownList).SelectedItem.Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblMinConcLimitID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(MinCNDI)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblAvgConcActResultsID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("txtAvgC"), TextBox).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("ddlAvgCVMC"), DropDownList).SelectedItem.Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblAvgConcLimitID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(AvgCNDI)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblMaxConcActResultsID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("txtMaxC"), TextBox).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("ddlMaxCVMC"), DropDownList).SelectedItem.Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblMaxConcLimitID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(MaxCNDI)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblConcMUnitsID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblAvgQuantActResultsID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("txtAvgQ"), TextBox).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("ddlAvgQVMC"), DropDownList).SelectedItem.Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblAvgQuantLimitID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(AvgQNDI)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblMaxQuantActResultsID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("txtMaxQ"), TextBox).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("ddlMaxQVMC"), DropDownList).SelectedItem.Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblMaxQuantLimitID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(MaxQNDI)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("lblQuantMUnitsID"), Label).Text)
                        strbldData.Append("|")
                        strbldData.Append(CType(dgi.FindControl("txtNumberOfExcursions"), TextBox).Text)
                        strbldData.Append("@")
                    Else
                        strbldData2.Append(CType(dgi.FindControl("lblActResultsSetID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblLimitMonitorLocCode"), Label).Text)
                        strbldData2.Append("|")
                        'strbldData2.Append(CType(dgi.FindControl("lblLimitSampleTypeCode"), Label).Text)
                        strbldData2.Append(CType(dgi.FindControl("ddlSampleType"), DropDownList).SelectedValue.ToString)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblLimitGroup"), Label).Text)
                        strbldData2.Append("|")
                        'strbldData2.Append(CType(dgi.FindControl("lblLimitSampleFreqID"), Label).Text)
                        strbldData2.Append(CType(dgi.FindControl("ddlSampleFreq"), DropDownList).SelectedValue.ToString)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblParamID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblMinConcActResultsID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("txtMinC"), TextBox).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("ddlMinCVMC"), DropDownList).SelectedItem.Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblMinConcLimitID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(MinCNDI)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblAvgConcActResultsID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("txtAvgC"), TextBox).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("ddlAvgCVMC"), DropDownList).SelectedItem.Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblAvgConcLimitID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(AvgCNDI)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblMaxConcActResultsID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("txtMaxC"), TextBox).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("ddlMaxCVMC"), DropDownList).SelectedItem.Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblMaxConcLimitID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(MaxCNDI)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblConcMUnitsID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblAvgQuantActResultsID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("txtAvgQ"), TextBox).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("ddlAvgQVMC"), DropDownList).SelectedItem.Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblAvgQuantLimitID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(AvgQNDI)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblMaxQuantActResultsID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("txtMaxQ"), TextBox).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("ddlMaxQVMC"), DropDownList).SelectedItem.Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblMaxQuantLimitID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(MaxQNDI)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("lblQuantMUnitsID"), Label).Text)
                        strbldData2.Append("|")
                        strbldData2.Append(CType(dgi.FindControl("txtNumberOfExcursions"), TextBox).Text)
                        strbldData2.Append("@")
                    End If
                End If
            End If
        Next
        strData = strbldData.ToString
        strData2 = strbldData2.ToString
    End Sub

    Private Sub BulkSave(ByVal Optional IsSubmit As Boolean = False)

        Dim myDMR As New clsDMR
        Dim strData, strData2, DateReceived, Comment As String
        Dim Results As String = ""
        strData = ""
        strData2 = ""
        DateReceived = ""
        Comment = ""
        'Comment added to GetPageData for verficiation of change 6/21/2016, CJM.
        Call GetPageData(strData, strData2, DateReceived, Comment)
        'check if loaded data is different than current data and save only if it is
        If Session("Status") <> "D" And Session("Status") <> "S" Then
            If Not DateReceived.Equals(Session("DateReceived")) Or Not strData.Equals(Session("strData")) Or Not strData2.Equals(Session("strData2")) Or Not Comment.Equals(Session("Comment")) Then   'Or (Session("SaveOverride") = "Y")
                If Session("Status") = String.Empty Then
                    Session("Status") = "N"
                End If
                If strData.Length > 0 Then
                    myDMR.DMRBulkUpdate(Session("UnitID"),
                  Session("ActID"),
                   txtDateReceived.Text,
                   txtStartDate.Text,
                   txtEndDate.Text,
                   Session("WasteWaterDEUser"),
                   strData,
                   strData2)

                    myDMR.GetActID(Request.QueryString("PermitID"), txtStartDate.Text, Session("ActID"))

                    If IsSubmit = True Then
                        Session("Status") = "S"
                    End If

                    myDMR.DMRStatusUpdate(Session("ActID"),
                  Session("WasteWaterDEUser"),
                  Session("Status"),
                  txtComments.Text,
                  txtStartDate.Text)

                    'Session("SaveOverride") = "N"

                    myDMR.GetOutsideLimits(Session("DisplayPage"), txtStartDate.Text, Request.QueryString("PiID"), Request.QueryString("UnitID"), Results)

                    If Len(Results) > 0 Then
                        Try
                            MesgBox("Warning: Some of your Data is outside the assigned Limits. " & "\n" & Results)
                        Catch ex As Exception
                            'Nothing
                        End Try
                    End If

                    'GetData(Session("DisplayPage")) 'Get fresh copy of data with ID's 
                End If
            Else
                If Session("Status") = "N" Or Session("Status") = "R" Or Session("Status") = "X" Then
                    If IsSubmit = True Then
                        Session("Status") = "S"

                        myDMR.DMRStatusUpdate(Session("ActID"),
                  Session("WasteWaterDEUser"),
                  Session("Status"),
                  txtComments.Text,
                  txtStartDate.Text)
                    End If
                End If
            End If
        End If
        GetData("1") 'Get fresh copy of data with ID's 
    End Sub

    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)
    End Sub

    Private Sub btnStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStore.Click
        Page.Validate()

        If Page.IsValid = True Then
            Call BulkSave()
        End If
    End Sub

    Private Sub chkShowMetadata_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowMetadata.CheckedChanged
        Dim RecDate As String = txtDateReceived.Text

        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            GetData(Session("DisplayPage"))
            If txtDateReceived.Text = "" Then
                txtDateReceived.Text = RecDate
            End If
        End If

    End Sub

    Private Sub chkShowLimits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowLimits.CheckedChanged
        Dim RecDate As String = txtDateReceived.Text
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            GetData(Session("DisplayPage"))
            If txtDateReceived.Text = "" Then
                txtDateReceived.Text = RecDate
            End If
        End If

    End Sub

    Sub GetData(ByVal DisplayPage As String)
        Dim myDMR As New clsDMR
        Dim DMRComplete As String = ""
        Dim Signed As String = "N"
        dgDMR.Columns(11).Visible = chkShowMetadata.Checked
        txtStartDate.Text = ddlYrMonth.SelectedValue.Substring(4) + "/1/" + ddlYrMonth.SelectedValue.Substring(0, 4)
        txtEndDate.Text = Convert.ToDateTime(txtStartDate.Text).AddMonths(1).AddDays(-1).ToShortDateString
        'Set up Pg buttons based on data
        Call HideShowPgButtons(Session("UnitID"), txtStartDate.Text, Session("PermitID"))
        'Dim myDMR As New clsDMR
        dgDMR.DataSource = myDMR.GetDMRLimitsForUnitDateT(Session("UnitID"), txtStartDate.Text, Session("PermitID"), Session("DisplayPage"), Session("Status"))
        dgDMR.DataBind()
        myDMR.GetActID(Request.QueryString("PermitID"), txtStartDate.Text, Session("ActID"))

        If Session("Status") = "N" Or Session("Status") = "R" Or Session("Status") = "X" Then
            dgDMR.Enabled = True
            ddlDMRNoDischargeReason.Enabled = True
            btnStore.Visible = True
            btnSubmit.Visible = True
            Label16.Visible = True
            btnReverse.Visible = False
            lblReverse.Visible = False
            'btnPrint.Visible = True
            lblStatus.Text = "This Report is Available for Editing and Submission."
        Else

            If Session("ActID") > 0 Then
                myDMR.DMRStatusCheckSigned_Ext(Session("ActID"), Signed)
                If Signed = "Y" Then
                    btnReverse.Visible = False
                    lblReverse.Visible = False
                    lblStatus.Text = "This Report has already been Submitted and Digitally Signed, and can not be edited."
                Else
                    btnReverse.Visible = True
                    lblReverse.Visible = True
                    lblStatus.Text = "This Report has already been Submitted, and can not be edited."
                End If
            Else
                btnReverse.Visible = False
                lblReverse.Visible = False
                lblStatus.Text = ""
            End If

            If Session("Status") = "D" Then
                If Session("ActID") = 0 Then
                    lblStatus.Text = "DMR Online Data is not available. The Report has been entered directly into DEN."
                Else
                    lblStatus.Text = "This Report has already been Digitally Signed, and has been loaded into the Enviromental Navigator for EPA submission."
                End If

            End If

            dgDMR.Enabled = False
            ddlDMRNoDischargeReason.Enabled = False
            btnStore.Visible = False
            btnSubmit.Visible = False
            Label16.Visible = False
            'btnPrint.Visible = False
        End If

        txtDateReceived.Text = myDMR.GetDMRReceivedDate(Session("UnitID"), txtStartDate.Text)

        Dim DMRComments As String = ""
        Dim DMRRejection As String = ""
        myDMR.GetDMRComments(Session("UnitID"), Session("PermitID"), txtStartDate.Text, DMRComments, DMRRejection)
        txtComments.Text = DMRComments
        txtReasonRejected.Text = DMRRejection
        If DMRRejection = "" Then
            Label12.Visible = False
            txtReasonRejected.Visible = False
        Else
            Label12.Visible = True
            txtReasonRejected.Visible = True
        End If

        Session("AllowNoLimitDataEntry") = "N"
        'save copy of data for checking if data is dirty on save
        Dim strData, strData2, DateReceived, Comment As String
        strData = ""
        strData2 = ""
        DateReceived = ""
        Comment = ""
        Call GetPageData(strData, strData2, DateReceived, Comment)
        'Save data for comparision
        Session("DateReceived") = DateReceived
        Session("strData") = strData
        Session("strData2") = strData2
        Session("Comment") = Comment
    End Sub

    Private Sub ddlYrMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlYrMonth.SelectedIndexChanged
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Session("ActID") = 0
            GetData(Session("DisplayPage"))
        End If

    End Sub

    'Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
    '    GetData(Session("DisplayPage"))
    'End Sub

    'Private Sub ddlOutfall_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlOutfall.SelectedIndexChanged
    '    Call BulkSave()
    '    Server.Transfer("frmDMREntry.aspx?Tkt=" & Request.QueryString("Tkt") & "&ReportID=" & Request.QueryString("ReportID") & "&UnitID=" & ddlOutfall.SelectedValue & "&DMRDate=" & ddlYrMonth.SelectedValue & "&RecDate=" & txtDateReceived.Text & "&PermitID=" & Session("PermitID"))
    'End Sub

    'Private Sub btnBulkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBulkSave.Click
    '    Page.Validate()

    '    If Page.IsValid() = True Then
    '        Call BulkSave()
    '    End If
    'End Sub

    Private Sub SetupPageButtons(ByVal SelectedPage As String)
        If SelectedPage = "1" Then
            btnPg1.ForeColor = System.Drawing.Color.Black
        Else
            btnPg1.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "2" Then
            btnPg2.ForeColor = System.Drawing.Color.Black
        Else
            btnPg2.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "3" Then
            btnPg3.ForeColor = System.Drawing.Color.Black
        Else
            btnPg3.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "4" Then
            btnPg4.ForeColor = System.Drawing.Color.Black
        Else
            btnPg4.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "5" Then
            btnPg5.ForeColor = System.Drawing.Color.Black
        Else
            btnPg5.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "6" Then
            btnPg6.ForeColor = System.Drawing.Color.Black
        Else
            btnPg6.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "7" Then
            btnPg7.ForeColor = System.Drawing.Color.Black
        Else
            btnPg7.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "8" Then
            btnPg8.ForeColor = System.Drawing.Color.Black
        Else
            btnPg8.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "9" Then
            btnPg9.ForeColor = System.Drawing.Color.Black
        Else
            btnPg9.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "10" Then
            btnPg10.ForeColor = System.Drawing.Color.Black
        Else
            btnPg10.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "11" Then
            btnPg11.ForeColor = System.Drawing.Color.Black
        Else
            btnPg11.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "12" Then
            btnPg12.ForeColor = System.Drawing.Color.Black
        Else
            btnPg12.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "13" Then
            btnPg13.ForeColor = System.Drawing.Color.Black
        Else
            btnPg13.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "14" Then
            btnPg14.ForeColor = System.Drawing.Color.Black
        Else
            btnPg14.ForeColor = System.Drawing.Color.Tan
        End If
        If SelectedPage = "99" Then
            btnPg99.ForeColor = System.Drawing.Color.Black
        Else
            btnPg99.ForeColor = System.Drawing.Color.Tan
        End If

    End Sub
    Private Sub btnPg1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg1.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("1")
            Session("DisplayPage") = "1"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg2.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("2")
            Session("DisplayPage") = "2"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg3.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("3")
            Session("DisplayPage") = "3"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg4.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("4")
            Session("DisplayPage") = "4"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg5.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("5")
            Session("DisplayPage") = "5"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg6.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("6")
            Session("DisplayPage") = "6"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg7.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("7")
            Session("DisplayPage") = "7"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg8.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("8")
            Session("DisplayPage") = "8"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg9.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("9")
            Session("DisplayPage") = "9"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg10.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("10")
            Session("DisplayPage") = "10"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg11.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("11")
            Session("DisplayPage") = "11"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg12.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("12")
            Session("DisplayPage") = "12"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg13.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("13")
            Session("DisplayPage") = "13"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg14.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("14")
            Session("DisplayPage") = "14"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub btnPg99_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPg99.Click
        Page.Validate()

        If Page.IsValid() = True Then
            Call BulkSave()
            Call SetupPageButtons("99")
            Session("DisplayPage") = "99"
            GetData(Session("DisplayPage"))
        End If

    End Sub
    Private Sub HideShowPgButtons(ByVal UnitID As Int32, ByVal StartDate As String, ByVal PermitID As Int32)
        Dim myDMR As New clsDMR
        Dim rdr As SqlDataReader
        btnPg1.Visible = False
        btnPg2.Visible = False
        btnPg3.Visible = False
        btnPg4.Visible = False
        btnPg5.Visible = False
        btnPg6.Visible = False
        btnPg7.Visible = False
        btnPg8.Visible = False
        btnPg9.Visible = False
        btnPg10.Visible = False
        btnPg11.Visible = False
        btnPg12.Visible = False
        btnPg13.Visible = False
        btnPg14.Visible = False
        btnPg99.Visible = False

        rdr = myDMR.GetDMRPages(UnitID, StartDate, PermitID)
        While rdr.Read = True
            Select Case rdr("DisplayPage")
                Case "1"
                    btnPg1.Visible = True
                Case "2"
                    btnPg2.Visible = True
                Case "3"
                    btnPg3.Visible = True
                Case "4"
                    btnPg4.Visible = True
                Case "5"
                    btnPg5.Visible = True
                Case "6"
                    btnPg6.Visible = True
                Case "7"
                    btnPg7.Visible = True
                Case "8"
                    btnPg8.Visible = True
                Case "9"
                    btnPg9.Visible = True
                Case "10"
                    btnPg10.Visible = True
                Case "11"
                    btnPg11.Visible = True
                Case "12"
                    btnPg12.Visible = True
                Case "13"
                    btnPg13.Visible = True
                Case "14"
                    btnPg14.Visible = True
                Case "99"
                    btnPg99.Visible = True
            End Select
        End While
    End Sub

    Private Sub ddlDMRNoDischargeReason_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDMRNoDischargeReason.SelectedIndexChanged
        Dim dgi As DataGridItem
        For Each dgi In dgDMR.Items
            If dgi.Visible = True And CType(dgi.FindControl("ddlNDI"), DropDownList).Enabled = True Then
                CType(dgi.FindControl("lblNDI"), Label).Text = ddlDMRNoDischargeReason.SelectedValue
                CType(dgi.FindControl("ddlNDI"), DropDownList).SelectedValue = ddlDMRNoDischargeReason.SelectedValue
            End If
        Next
        ddlDMRNoDischargeReason.SelectedIndex = 0
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        'Session("Status") = "S"

        Page.Validate()

        If Page.IsValid() = True Then
            Session("DateReceived") = DatePart(DateInterval.Month, Now()).ToString + "/" + DatePart(DateInterval.Day, Now()).ToString + "/" + DatePart(DateInterval.Year, Now()).ToString
            txtDateReceived.Text = DatePart(DateInterval.Month, Now()).ToString + "/" + DatePart(DateInterval.Day, Now()).ToString + "/" + DatePart(DateInterval.Year, Now()).ToString

            'Session("SaveOverride") = "Y"
            'Call BulkSave()

            Dim myDMR As New clsDMR
            Dim DMRComplete As String = ""
            myDMR.CheckComplete(Request.QueryString("PermitID"), Request.QueryString("UnitID"), txtStartDate.Text, DMRComplete)

            If Not (DMRComplete = "Yes") Then
                'Session("SaveOverride") = "Y"
                'Session("Status") = "N"
                Call BulkSave()
                lblUpload.Text = "Cannot Submit DMR until data for all Outfalls has been entered."
                Return
            Else
                'Session("SaveOverride") = "Y"
                'Session("Status") = "S"
                Call BulkSave(True)
                lblUpload.Text = "DMR Valid for Submission."
            End If

            Dim service As New ReportExecutionServiceSoapClient

            'service.Url = ConfigurationManager.AppSettings("reportserverpath")

            service.ClientCredentials.Windows.ClientCredential = New NetworkCredential(ConfigurationManager.AppSettings("reportuser"), ConfigurationManager.AppSettings("reportpassword"), ConfigurationManager.AppSettings("reportdomain"))

            'service.ClientCredentials = New NetworkCredential(ConfigurationManager.AppSettings("reportuser"), ConfigurationManager.AppSettings("reportpassword"), ConfigurationManager.AppSettings("reportdomain"))


            Dim parameters(2) As ParameterValue
            For i As Integer = 0 To parameters.Length - 1
                parameters(i) = New ParameterValue()
            Next
            parameters(0).Name = "ActID"
            parameters(0).Value = Session("ActID")
            parameters(1).Name = "StartDate"
            parameters(1).Value = ddlYrMonth.SelectedValue.Substring(4) + "/1/" + ddlYrMonth.SelectedValue.Substring(0, 4)
            parameters(2).Name = "PermitID"
            parameters(2).Value = Request.QueryString("PermitID")


            ' Init Report to execute
            Dim serverInfoHeader As ServerInfoHeader = Nothing
            Dim executionInfo As ExecutionInfo = Nothing
            Dim executionHeader As ExecutionHeader

            executionHeader = service.LoadReport(Nothing, ConfigurationManager.AppSettings("SubmissionReport"), Nothing, serverInfoHeader, executionInfo)
            service.SetExecutionParameters(executionHeader, Nothing, parameters, Nothing, executionInfo)
            Dim bytes() As Byte = Nothing
            Dim fileextension As String = String.Empty
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty

            Dim warnings() As Warning = Nothing
            Dim streamIds() As String = Nothing
            service.Render(executionHeader, Nothing, "PDF", Nothing, bytes, fileextension, mimeType, encoding, warnings, streamIds)


            Dim CROMERRMgr As New CROMERRExchange
            AddHandler CROMERRMgr.TransferCompleted, AddressOf SentToCROMERRCompleted
            AddHandler CROMERRMgr.TransferFailed, AddressOf SentToCROMERRFailed



            Dim FileName As String = ddlYrMonth.SelectedValue.ToString + "-" + Session("ActID") + "-" + Request.QueryString("PermitID") + ".pdf"
            'Dim UploadPath As String = Server.MapPath(ConfigurationManager.AppSettings("UploadPath").ToString)
            'Dim FilePath As String = UploadPath + "\" + FileName

            'Dim fs As FileStream = New FileStream(FilePath, FileMode.Create)
            'fs.Write(bytes, 0, bytes.Length)
            'fs.Close()

            'If FileTransferUpload1.IsBusy Then
            '    Thread.Sleep(1000)
            'End If

            ''set the filepath and start the uploader
            'FileTransferUpload1.LocalFilePath = FilePath

            'FileTransferUpload1.LoginToken = Request.QueryString("Tkt") 'HttpUtility.UrlDecode(Request.QueryString("Tkt"))
            'FileTransferUpload1.PiID = Request.QueryString("PiID")
            'FileTransferUpload1.ReportID = ConfigurationManager.AppSettings("ReportType").ToString
            'FileTransferUpload1.Comments = "Discharge Monitoring Report for " + lblSiteOutfall.Text + ", " + MonthName(Mid(ddlYrMonth.SelectedValue.ToString, 5, 2)) + ", " + Left(ddlYrMonth.SelectedValue.ToString, 4)
            'FileTransferUpload1.SequenceRef = Request.QueryString("UnitID") + ddlYrMonth.SelectedValue.ToString
            'FileTransferUpload1.IPAddressUsed = Request.UserHostAddress.ToString

            'FileTransferUpload1.RunWorkerAsync() 'start the background worker
            Dim comments As String = "Discharge Monitoring Report for " + lblSiteOutfall.Text + ", " + MonthName(Mid(ddlYrMonth.SelectedValue.ToString, 5, 2)) + ", " + Left(ddlYrMonth.SelectedValue.ToString, 4)
            Dim sequenceRef As String = Request.QueryString("UnitID") + ddlYrMonth.SelectedValue.ToString
            Dim token As String = Request.QueryString("Tkt")

            CROMERRMgr.SendReportToCROMERR(Me, token, bytes, FileName, ConfigurationManager.AppSettings("ReportType"), Request.QueryString("PiID"), comments, sequenceRef)
        End If

    End Sub

    Private Sub SentToCROMERRFailed(sender As Object, e As CROMERREventArgs)
        Dim myDMR As New clsDMR

        lblUpload.Text = "There was a problem submitting your Report electronically." & Err.ToString

        myDMR.DMRStatusUpdate(Session("ActID"),
                  Session("WasteWaterDEUser"),
                  "N",
                  lblUpload.Text,
                  txtStartDate.Text)
    End Sub
    Private Sub SentToCROMERRCompleted(sender As Object, e As CROMERREventArgs)
        Dim myDMR As New clsDMR

        myDMR.DMRStatusCORPath(Session("ActID"),
              e.documentURL,
              txtStartDate.Text)

        Response.Redirect("frmRedirect.aspx" & "?Tkt=" & Request.QueryString("Tkt"))

    End Sub


    'Private Sub FileTransferUpload1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FileTransferUpload1.RunWorkerCompleted
    '    Dim myDMR As New clsDMR
    '    Try
    '        File.Delete(FileTransferUpload1.LocalFilePath)
    '    Catch ex As Exception
    '    End Try



    '    If FileTransferUpload1.ValidToken = 1 AndAlso e.Error Is Nothing AndAlso Not e.Cancelled AndAlso FileTransferUpload1.RedirectPath <> String.Empty Then

    '        Session("LocalHash") = e.Result.ToString
    '        Session("RedirectPath") = FileTransferUpload1.RedirectPath

    '        myDMR.DMRStatusCORPath(Session("ActID"),
    '              Session("RedirectPath"),
    '              txtStartDate.Text)

    '        Response.Redirect("frmRedirect.aspx" & "?Tkt=" & Request.QueryString("Tkt"))
    '        FileTransferBase1.Dispose()
    '    Else
    '        Dim err As Exception = Nothing
    '        If e.Error IsNot Nothing Then
    '            err = e.Error
    '        ElseIf e.Cancelled Then
    '            err = New Exception("Transaction Cancelled.")
    '        ElseIf FileTransferUpload1.RedirectPath = String.Empty Then
    '            err = New Exception("Failed to get a return path to the file.")
    '        Else
    '            err = New Exception("Could not validate user.")
    '        End If
    '        lblUpload.Text = "There was a problem submitting your Report electronically." & err.ToString
    '        FileTransferBase1.Dispose()
    '        Session("Status") = "N"
    '        myDMR.DMRStatusUpdate(Session("ActID"),
    '              Session("WasteWaterDEUser"),
    '              Session("Status"),
    '              txtComments.Text,
    '              txtStartDate.Text)
    '        Return
    '    End If



    'End Sub



    Protected Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Dim Token As String
        Token = HttpUtility.UrlEncode(Request.QueryString("Tkt"))
        Server.Transfer("frmMainPage.aspx?Tkt=" & Token & "&ReportID=" & Request.QueryString("ReportID") & "&PiID=" & Request.QueryString("PiID"))
    End Sub

    Protected Sub txtAvgQ_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dgi As DataGridItem

        For Each dgi In dgDMR.Items
            If dgi.Visible = True Then

                If CType(dgi.FindControl("txtAvgQ"), TextBox).Text > CType(dgi.FindControl("lblAvgQuantLimit"), Label).Text Then
                    CType(dgi.FindControl("txtAvgQ"), TextBox).BackColor = Drawing.Color.Red
                Else
                    CType(dgi.FindControl("txtAvgQ"), TextBox).BackColor = Drawing.Color.White
                End If
            End If
        Next
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Session("ActID") Is Nothing Then
            Return
        End If

        Response.Redirect("ViewReport.ashx?ActID=" & Session("ActID") & "&monperiod=" & ddlYrMonth.SelectedValue.Substring(4) + "-1-" + ddlYrMonth.SelectedValue.Substring(0, 4) & "&PermitID=" & Request.QueryString("PermitID") & "")

        'Dim service As New ReportExecutionService()
        'service.Url = ConfigurationManager.AppSettings("reportserverpath")
        'service.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("reportuser"), ConfigurationManager.AppSettings("reportpassword"), ConfigurationManager.AppSettings("reportdomain"))
        'Dim parameters(2) As ParameterValue
        'For i As Integer = 0 To parameters.Length - 1
        '    parameters(i) = New ParameterValue()
        'Next
        'parameters(0).Name = "ActID"
        'parameters(0).Value = Session("ActID")
        'parameters(1).Name = "StartDate"
        'parameters(1).Value = ddlYrMonth.SelectedValue.Substring(4) + "/1/" + ddlYrMonth.SelectedValue.Substring(0, 4)
        'parameters(2).Name = "PermitID"
        'parameters(2).Value = Request.QueryString("PermitID")
        'service.LoadReport(ConfigurationManager.AppSettings("DraftReport"), Nothing)
        'service.SetExecutionParameters(parameters, Nothing)
        'Dim bytes() As Byte = _
        'service.Render("PDF", Nothing, "pdf", Nothing, Nothing, Nothing, Nothing)
        'Response.AddHeader("content-disposition", String.Format("inline; filename=""{0}""", "Report.pdf"))
        'Response.BinaryWrite(bytes)
        'Response.End()
    End Sub

    Protected Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click

        Dim myDMR As New clsDMR
        myDMR.DMRStatusReverse(Session("ActID"), Session("WasteWaterDEUser"), txtStartDate.Text)
        GetData(Session("DisplayPage"))
    End Sub
End Class
