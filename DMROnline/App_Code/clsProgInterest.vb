
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProgInterest
    Inherits System.Web.UI.Page
    Private connection As String = Session("ConnStr")

    Public Function GetWasteWaterSites(ByVal ActiveSitesOnly As String, ByVal OrderBySiteName As String, ByVal PiID As Int32) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblProgInterest_GetNPDES_Sites", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ActiveSitesOnly", SqlDbType.Char, 1)
        cmd.Parameters("@ActiveSitesOnly").Value = ActiveSitesOnly
        cmd.Parameters.Add("@OrderBySiteName", SqlDbType.Char, 1)
        cmd.Parameters("@OrderBySiteName").Value = OrderBySiteName
        cmd.Parameters.Add("@PiID", SqlDbType.Int)
        cmd.Parameters("@PiID").Value = PiID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of Waste Water sites.  " & e.Message)
        End Try
    End Function

    Public Function GetPiNameForProgID(ByVal ProgID As String) As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)

        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblProgInterest_GetSiteNameForProgID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ProgID", SqlDbType.VarChar, 20)
        cmd.Parameters("@ProgID").Value = ProgID
        cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
        cmd.Parameters("@PiTypeID").Value = 4  'Wasterwater sites
        cmd.Parameters.Add("@PiName", SqlDbType.VarChar, 80)
        cmd.Parameters("@PiName").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@PiName").Value
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Site name for site ID.  " & e.Message)
        End Try
    End Function

    Public Sub GetPiNameProgIDForPiID(ByVal PiID As Int32, ByRef PiName As String, ByRef ProgID As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)

        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblProgInterest_GetPiNameProgIDForPiID ", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PiID", SqlDbType.Int)
        cmd.Parameters("@PiID").Value = PiID
        cmd.Parameters.Add("@PiName", SqlDbType.VarChar, 80)
        cmd.Parameters("@PiName").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ProgID", SqlDbType.VarChar, 30)
        cmd.Parameters("@ProgID").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            PiName = cmd.Parameters("@PiName").Value
            ProgID = cmd.Parameters("@ProgID").Value
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Site name  and ProgID for PiID.  " & e.Message)
        End Try
    End Sub

    Public Function GetPiIDForProgID(ByVal ProgID As String) As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)

        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblProgInterest_GetPiIDForProgID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ProgID", SqlDbType.VarChar, 20)
        cmd.Parameters("@ProgID").Value = ProgID
        cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
        cmd.Parameters("@PiTypeID").Value = 4  'Wasterwater sites
        cmd.Parameters.Add("@PiID", SqlDbType.Int)
        cmd.Parameters("@PiID").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@PiID").Value
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting PiID for site ID.  " & e.Message)
        End Try
    End Function

    Public Sub ProgInterestGetData(
    ByVal PiID As Int32,
    ByRef PiTypeID As Int16,
    ByRef PiType As String,
    ByRef PiName As String,
    ByRef FacName As String,
    ByRef ProgID As String,
    ByRef PiFederalID As String,
    ByRef LandTypeCode As String,
    ByRef SiteTypeCode As String,
    ByRef FacID As Int32,
    ByRef PiDUNS As String,
    ByRef PiUltimateDUNS As String,
    ByRef PiStartDate As String,
    ByRef PiEndDate As String,
    ByRef PiDesc As String,
    ByRef PiLocation As String,
    ByRef PiComplianceStatusID As Int16,
    ByRef PiOperatingStatusID As Int16,
    ByRef PiContEmMonitorCode As String,
    ByRef LocID As Int32,
    ByRef DeCountyCode As String,
    ByRef SiteArea As String,
    ByRef Employees As String,
    ByRef Produces As String,
    ByRef PiX As String,
    ByRef PiY As String,
    ByRef PiComment As String,
    ByRef CreatedBy As String,
    ByRef CreatedDate As String,
    ByRef LastChgBy As String,
    ByRef LastChgDate As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("sptblProgInterest_GetFieldsForPiID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PiID", SqlDbType.Int)
        cmd.Parameters("@PiID").Value = PiID
        cmd.Parameters.Add("@PiTypeID", SqlDbType.SmallInt)
        cmd.Parameters("@PiTypeID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiType", SqlDbType.VarChar, 50)
        cmd.Parameters("@PiType").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiName", SqlDbType.VarChar, 80)
        cmd.Parameters("@PiName").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@FacName", SqlDbType.VarChar, 60)
        cmd.Parameters("@FacName").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ProgID", SqlDbType.VarChar, 20)
        cmd.Parameters("@ProgID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiFederalID", SqlDbType.VarChar, 12)
        cmd.Parameters("@PiFederalID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@LandTypeCode", SqlDbType.VarChar, 1)
        cmd.Parameters("@LandTypeCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@SiteTypeCode", SqlDbType.VarChar, 2)
        cmd.Parameters("@SiteTypeCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@FacID", SqlDbType.Int)
        cmd.Parameters("@FacID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiDUNS", SqlDbType.VarChar, 9)
        cmd.Parameters("@PiDUNS").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiUltimateDUNS", SqlDbType.VarChar, 9)
        cmd.Parameters("@PiUltimateDUNS").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiStartDate", SqlDbType.VarChar, 20)
        cmd.Parameters("@PiStartDate").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiEndDate", SqlDbType.VarChar, 20)
        cmd.Parameters("@PiEndDate").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiDesc", SqlDbType.VarChar, 100)
        cmd.Parameters("@PiDesc").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiLocation", SqlDbType.VarChar, 100)
        cmd.Parameters("@PiLocation").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiComplianceStatusID", SqlDbType.TinyInt)
        cmd.Parameters("@PiComplianceStatusID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiOperatingStatusID", SqlDbType.TinyInt)
        cmd.Parameters("@PiOperatingStatusID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiContEmMonitorCode", SqlDbType.VarChar, 1)
        cmd.Parameters("@PiContEmMonitorCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@LocID", SqlDbType.Int)
        cmd.Parameters("@LocID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@DeCountyCode", SqlDbType.VarChar, 3)
        cmd.Parameters("@DeCountyCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@SiteArea", SqlDbType.VarChar, 10)
        cmd.Parameters("@SiteArea").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Employees", SqlDbType.VarChar, 10)
        cmd.Parameters("@Employees").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Produces", SqlDbType.VarChar, 500)
        cmd.Parameters("@Produces").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiX", SqlDbType.VarChar, 15)
        cmd.Parameters("@PiX").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiY", SqlDbType.VarChar, 15)
        cmd.Parameters("@PiY").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiComment", SqlDbType.VarChar, 1000)
        cmd.Parameters("@PiComment").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 20)
        cmd.Parameters("@CreatedBy").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@CreatedDate", SqlDbType.VarChar, 20)
        cmd.Parameters("@CreatedDate").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@LastChgBy", SqlDbType.VarChar, 20)
        cmd.Parameters("@LastChgBy").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@LastChgDate", SqlDbType.VarChar, 20)
        cmd.Parameters("@LastChgDate").Direction = ParameterDirection.Output

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            PiTypeID = cmd.Parameters("@PiTypeID").Value()
            PiType = cmd.Parameters("@PiType").Value()
            PiName = cmd.Parameters("@PiName").Value()
            FacName = cmd.Parameters("@FacName").Value()
            ProgID = cmd.Parameters("@ProgID").Value()
            PiFederalID = cmd.Parameters("@PiFederalID").Value()
            LandTypeCode = cmd.Parameters("@LandTypeCode").Value()
            SiteTypeCode = cmd.Parameters("@SiteTypeCode").Value()
            FacID = cmd.Parameters("@FacID").Value()
            PiDUNS = cmd.Parameters("@PiDUNS").Value()
            PiDUNS = PiDUNS.Trim
            PiUltimateDUNS = cmd.Parameters("@PiUltimateDUNS").Value()
            PiUltimateDUNS = PiUltimateDUNS.Trim
            PiStartDate = cmd.Parameters("@PiStartDate").Value()
            PiEndDate = cmd.Parameters("@PiEndDate").Value()
            PiDesc = cmd.Parameters("@PiDesc").Value()
            PiLocation = cmd.Parameters("@PiLocation").Value()
            PiComplianceStatusID = cmd.Parameters("@PiComplianceStatusID").Value()
            PiOperatingStatusID = cmd.Parameters("@PiOperatingStatusID").Value()
            PiContEmMonitorCode = cmd.Parameters("@PiContEmMonitorCode").Value()
            LocID = cmd.Parameters("@LocID").Value()
            DeCountyCode = cmd.Parameters("@DeCountyCode").Value()
            SiteArea = cmd.Parameters("@SiteArea").Value()
            Employees = cmd.Parameters("@Employees").Value()
            Produces = cmd.Parameters("@Produces").Value()
            PiX = cmd.Parameters("@PiX").Value()
            PiY = cmd.Parameters("@PiY").Value()
            PiComment = cmd.Parameters("@PiComment").Value()
            CreatedBy = cmd.Parameters("@CreatedBy").Value()
            CreatedDate = cmd.Parameters("@CreatedDate").Value()
            LastChgBy = cmd.Parameters("@LastChgBy").Value()
            LastChgDate = cmd.Parameters("@LastChgDate").Value()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error retrieving Program Interest data.  " & ex.Message)
        End Try
    End Sub

End Class

