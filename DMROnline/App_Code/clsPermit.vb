Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data

Public Class clsPermit
    Inherits System.Web.UI.Page
    Private connection As String = Session("ConnStr")


    Public Function GetPermitsForSite(ByVal ProgID As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPermitsForProgID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ProgID", SqlDbType.VarChar, 9)
        cmd.Parameters("@ProgID").Value = ProgID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of permits for NPDES ID " + ProgID + ".  " & e.Message)
        End Try
    End Function

    Public Function GetPiIDPiNameProgIDForPermitID(ByVal PermitID As Integer, ByRef PiName As String, ByRef ProgID As String) As Int32
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPiIDPiNameForPermitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@PiID", SqlDbType.Int)
        cmd.Parameters("@PiID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PiName", SqlDbType.VarChar, 80)
        cmd.Parameters("@PiName").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ProgID", SqlDbType.VarChar, 10)
        cmd.Parameters("@ProgID").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

            PiName = cmd.Parameters("@PiName").Value()
            ProgID = cmd.Parameters("@ProgID").Value()
            Return cmd.Parameters("@PiID").Value()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting site name and ID for PermitID=" + PermitID.ToString + ".  " & e.Message)
        End Try
    End Function

    Public Sub GetPermitStartExpiresForPermitID(ByVal PermitID As Integer, ByRef PermitStartDate As String, ByRef PermitExpires As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblPermit_GetStartExpiresForPermitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@PermitStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@PermitStartDate").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PermitExpires", SqlDbType.VarChar, 12)
        cmd.Parameters("@PermitExpires").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

            PermitStartDate = cmd.Parameters("@PermitStartDate").Value()
            PermitExpires = cmd.Parameters("@PermitExpires").Value()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting permit issued and expiration dates for PermitID=" + PermitID.ToString + ".  " & e.Message)
        End Try
    End Sub

    Public Function GetPermitIDForUnitDate(ByVal UnitID As String, ByRef MonitorPeriodEnd As String) As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPermitIDForUnitIDDate", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.VarChar, 12)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@MonitorPeriodEnd", SqlDbType.VarChar, 25)
        cmd.Parameters("@MonitorPeriodEnd").Value = MonitorPeriodEnd
        cmd.Parameters.Add("@PermitID", SqlDbType.VarChar, 12)
        cmd.Parameters("@PermitID").Direction = ParameterDirection.Output

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@PermitID").Value()

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting PermitID for unit and monitoring period.   " & e.Message)
        End Try
    End Function

    Public Function AddEditPermit(ByVal ParentPermitID As String, _
    ByVal PermitID As String, _
    ByVal PermitNumber As String, ByVal PermitStartDate As String, _
    ByVal PermitExpires As String, _
    ByVal InitialLimitDefaultStart As String, ByVal InitialLimitDefaultEnd As String, _
    ByVal IntermediateLimitDefaultStart As String, ByVal IntermediateLimitDefaultEnd As String, _
    ByVal FinalLimitDefaultStart As String, ByVal FinalLimitDefaultEnd As String, _
    ByVal PermitStatusCode As String, _
    ByVal PermitDesc As String, ByVal PermitComment As String, _
    ByVal PermitteeNameAsIssued As String, _
    ByVal LegalNoticeRequired As String, _
    ByVal UserID As String) As String

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_AddEditPermit", cn)
        cmd.CommandType = CommandType.StoredProcedure
        If ParentPermitID Is Nothing Then
            ParentPermitID = 0
        End If
        If PermitID Is Nothing Then
            PermitID = 0
        End If
        If PermitNumber Is Nothing Then
            PermitNumber = ""
        End If

        cmd.Parameters.Add("@ParentPermitID", SqlDbType.Int)
        cmd.Parameters("@ParentPermitID").Value = ParentPermitID
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@PermitNumber", SqlDbType.VarChar, 60)
        cmd.Parameters("@PermitNumber").Value = PermitNumber
        cmd.Parameters.Add("@PermitStartDate", SqlDbType.VarChar, 10)
        cmd.Parameters("@PermitStartDate").Value = PermitStartDate
        cmd.Parameters.Add("@PermitExpires", SqlDbType.VarChar, 10)
        cmd.Parameters("@PermitExpires").Value = PermitExpires

        cmd.Parameters.Add("@InitialLimitDefaultStart", SqlDbType.VarChar, 10)
        cmd.Parameters("@InitialLimitDefaultStart").Value = InitialLimitDefaultStart
        cmd.Parameters.Add("@InitialLimitDefaultEnd", SqlDbType.VarChar, 10)
        cmd.Parameters("@InitialLimitDefaultEnd").Value = InitialLimitDefaultEnd
        cmd.Parameters.Add("@IntermediateLimitDefaultStart", SqlDbType.VarChar, 10)
        cmd.Parameters("@IntermediateLimitDefaultStart").Value = IntermediateLimitDefaultStart
        cmd.Parameters.Add("@IntermediateLimitDefaultEnd", SqlDbType.VarChar, 10)
        cmd.Parameters("@IntermediateLimitDefaultEnd").Value = IntermediateLimitDefaultEnd
        cmd.Parameters.Add("@FinalLimitDefaultStart", SqlDbType.VarChar, 10)
        cmd.Parameters("@FinalLimitDefaultStart").Value = FinalLimitDefaultStart
        cmd.Parameters.Add("@FinalLimitDefaultEnd", SqlDbType.VarChar, 10)
        cmd.Parameters("@FinalLimitDefaultEnd").Value = FinalLimitDefaultEnd

        cmd.Parameters.Add("@PermitStatusCode", SqlDbType.Char, 1)
        cmd.Parameters("@PermitStatusCode").Value = PermitStatusCode
        cmd.Parameters.Add("@PermitDesc", SqlDbType.VarChar, 100)
        cmd.Parameters("@PermitDesc").Value = PermitDesc
        cmd.Parameters.Add("@PermitComment", SqlDbType.VarChar, 1000)
        cmd.Parameters("@PermitComment").Value = PermitComment
        cmd.Parameters.Add("@PermitteeNameAsIssued", SqlDbType.VarChar, 100)
        cmd.Parameters("@PermitteeNameAsIssued").Value = PermitteeNameAsIssued
        cmd.Parameters.Add("@LegalNoticeRequired", SqlDbType.Char, 1)
        cmd.Parameters("@LegalNoticeRequired").Value = LegalNoticeRequired
        cmd.Parameters.Add("@User", SqlDbType.VarChar, 20)
        cmd.Parameters("@User").Value = UserID

        cmd.Parameters.Add("@NewPermitID", SqlDbType.Int)
        cmd.Parameters("@NewPermitID").Direction = ParameterDirection.Output

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@NewPermitID").Value

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error adding/editing Permit.  " & e.ToString)
        End Try
    End Function

    Public Function DeletePermit(ByVal PermitID As String) As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_DeletePermit", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@HasRelatedLimits", SqlDbType.Char, 1)
        cmd.Parameters("@HasRelatedLimits").Direction = ParameterDirection.Output

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@HasRelatedLimits").Value

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error deleting Permit.  " & e.ToString)
        End Try
    End Function

    Public Sub GetPermitDataForPermitID(ByVal PermitID As Integer, _
    ByRef PermitStartDate As String, ByRef PermitExpires As String, _
    ByRef InitialLimitDefaultStart As String, ByRef InitialLimitDefaultEnd As String, _
    ByRef IntermediateLimitDefaultStart As String, ByRef IntermediateLimitDefaultEnd As String, _
    ByRef FinalLimitDefaultStart As String, ByRef FinalLimitDefaultEnd As String, _
    ByRef PermitStatusCode As String, ByRef PermitDesc As String, ByRef PermitComment As String, _
    ByRef PermitteeNameAsIssued As String, ByRef LegalNoticeRequired As String, ByRef PermitMetaData As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPermitDataForPermitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@PermitStartDate", SqlDbType.VarChar, 10)
        cmd.Parameters("@PermitStartDate").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PermitExpires", SqlDbType.VarChar, 10)
        cmd.Parameters("@PermitExpires").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@InitialLimitDefaultStart", SqlDbType.VarChar, 10)
        cmd.Parameters("@InitialLimitDefaultStart").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@InitialLimitDefaultEnd", SqlDbType.VarChar, 10)
        cmd.Parameters("@InitialLimitDefaultEnd").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@IntermediateLimitDefaultStart", SqlDbType.VarChar, 10)
        cmd.Parameters("@IntermediateLimitDefaultStart").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@IntermediateLimitDefaultEnd", SqlDbType.VarChar, 10)
        cmd.Parameters("@IntermediateLimitDefaultEnd").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@FinalLimitDefaultStart", SqlDbType.VarChar, 10)
        cmd.Parameters("@FinalLimitDefaultStart").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@FinalLimitDefaultEnd", SqlDbType.VarChar, 10)
        cmd.Parameters("@FinalLimitDefaultEnd").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PermitStatusCode", SqlDbType.Char, 1)
        cmd.Parameters("@PermitStatusCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PermitDesc", SqlDbType.VarChar, 100)
        cmd.Parameters("@PermitDesc").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PermitComment", SqlDbType.VarChar, 1000)
        cmd.Parameters("@PermitComment").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PermitteeNameAsIssued", SqlDbType.VarChar, 100)
        cmd.Parameters("@PermitteeNameAsIssued").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@LegalNoticeRequired", SqlDbType.Char, 1)
        cmd.Parameters("@LegalNoticeRequired").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PermitMetadata", SqlDbType.VarChar, 200)
        cmd.Parameters("@PermitMetadata").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

            PermitStartDate = cmd.Parameters("@PermitStartDate").Value()
            PermitExpires = cmd.Parameters("@PermitExpires").Value()
            InitialLimitDefaultStart = cmd.Parameters("@InitialLimitDefaultStart").Value
            InitialLimitDefaultEnd = cmd.Parameters("@InitialLimitDefaultEnd").Value
            IntermediateLimitDefaultStart = cmd.Parameters("@IntermediateLimitDefaultStart").Value
            IntermediateLimitDefaultEnd = cmd.Parameters("@IntermediateLimitDefaultEnd").Value
            FinalLimitDefaultStart = cmd.Parameters("@FinalLimitDefaultStart").Value
            FinalLimitDefaultEnd = cmd.Parameters("@FinalLimitDefaultEnd").Value
            PermitStatusCode = cmd.Parameters("@PermitStatusCode").Value()
            PermitDesc = cmd.Parameters("@PermitDesc").Value()
            PermitComment = cmd.Parameters("@PermitComment").Value()
            PermitteeNameAsIssued = cmd.Parameters("@PermitteeNameAsIssued").Value()
            LegalNoticeRequired = cmd.Parameters("@LegalNoticeRequired").Value()
            PermitMetaData = cmd.Parameters("@PermitMetadata").Value()

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting permit data for PermitID=" + PermitID.ToString + ".  " & e.Message)
        End Try
    End Sub

    Public Function GetPermitStatusForCombo() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblPermitStatus_GetRSCombo", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list permit statuses. " & e.Message)
        End Try
    End Function

    Public Function GetPermitsForPermitNumber(ByVal PermitNumber As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPermitsForPermitNumber", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PermitNumber", SqlDbType.VarChar, 9)
        cmd.Parameters("@PermitNumber").Value = PermitNumber
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of permit date ranges for NPDES ID: " + PermitNumber + ".  " & e.Message)
        End Try
    End Function

    'Public Function tblPermit_GetRSComboForPiType() As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermitType_GetRSComboForPiType", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
    '    cmd.Parameters("@PiTypeID").Value = 3

    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting PiType list for combo.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetContactsForSite(ByVal PiID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPersonOrg_GetPermitContactsForPiID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiID", SqlDbType.Int)
    '    cmd.Parameters("@PiID").Value = PiID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Contact list for site.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetProjOfficersForPermitID(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblPermits_GetProjOfficers", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Project Officers for Permit.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetContactsForPermitID(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermits_GetContactsForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting contacts for Permit.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetCurrentOfficialForPermitID(ByVal PermitID As Integer) As Int32
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermits_GetCurrentOfficialForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@PersonOrgID", SqlDbType.Int)
    '    cmd.Parameters("@PersonOrgID").Direction = ParameterDirection.Output
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return cmd.Parameters("@PersonOrgID").Value
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting current official for Permit.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetPermitForPermitID(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("AP_spGetAirPermitForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Permit Info.  " & e.Message)
    '    End Try
    'End Function

    'Public Function UpdatePermitStatus(ByVal PermitID As Integer) As String
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermits_UpdateAirPermitStatusForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@PermitStatus", SqlDbType.VarChar, 70)
    '    cmd.Parameters("@PermitStatus").Direction = ParameterDirection.Output
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return cmd.Parameters("@PermitStatus").Value
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error updating permit status.  " & e.ToString)
    '    End Try
    'End Function

    'Public Function GetProjectOfficers() As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblDNRECEmpAfflTypePiType_GetRSComboForPiTypeID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
    '    cmd.Parameters("@PiTypeID").Value = 3
    '    cmd.Parameters.Add("@AfflTypeCode", SqlDbType.Int)
    '    cmd.Parameters("@AfflTypeCode").Value = 31 '' For Project Officers
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Project Officers.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetTrackingPath() As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermitTrackingPath_GetRSCombo", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting TrackingPath list for combo.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetTrackingPathForPermitID(ByVal PermitID As String) As Int16
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermits_GetTrackingPathIDForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@TrackingPathID", SqlDbType.Int)
    '    cmd.Parameters("@TrackingPathID").Direction = ParameterDirection.Output
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return cmd.Parameters("@TrackingPathID").Value
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting TrackingPathID for Air PermitID=" + PermitID.ToString + ".  " & e.ToString)
    '    End Try
    'End Function

    'Public Function GetTrackingPathForPermitType(ByVal PermitTypeCode As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermitTrackingPath_GetRSComboForPermitType", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitTypeCode", SqlDbType.Int)
    '    cmd.Parameters("@PermitTypeCode").Value = PermitTypeCode
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting TrackingPathInfo for PermitType.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetNextPermitNumber(ByVal PermitTypeCode As Integer, ByVal PiID As Int32) As String
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblPermits_GetNextNewAirPermitNumber", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitTypeCode", SqlDbType.Int)
    '    cmd.Parameters("@PermitTypeCode").Value = PermitTypeCode
    '    cmd.Parameters.Add("@PiID", SqlDbType.Int)
    '    cmd.Parameters("@PiID").Value = PiID
    '    cmd.Parameters.Add("@NextNewPermitNumber", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@NextNewPermitNumber").Direction = ParameterDirection.Output
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return cmd.Parameters("@NextNewPermitNumber").Value
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting next Permit Number for PermitTypeCode=" + PermitTypeCode.ToString + " and PiID=" + PiID.ToString + "." & e.Message)
    '    End Try
    'End Function


    'Public Function GetPermitees(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermits_GetRSPermiteesForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Permittees.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetTempPermitees(ByVal UserName As String) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblAPTempPermiteeInfo_GetRS", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100)
    '    cmd.Parameters("@UserName").Value = UserName
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting temp Permittees.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetLatestTrackingInfo(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPEvents_GetLast4PlusAppRecForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting latest tracking info.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetRelatedPermits(ByVal PermitID As Integer, ByRef RowCount As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermitParent_GetRSForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@RowCount", SqlDbType.Int)
    '    cmd.Parameters("@RowCount").Direction = ParameterDirection.ReturnValue
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        RowCount = cmd.Parameters("@RowCount").Value
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting related permits.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetPermitListForPiID(ByVal PiID As Integer, ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermits_GetPermitListForPiID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiID", SqlDbType.Int)
    '    cmd.Parameters("@PiID").Value = PiID
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error Permit List.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetOtherPermitAtSite(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblPermits_GetOtherAirPermitsAtSiteForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("SQL Server error getting list of other permits at a site.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetPermitListForPermitTypeIdTrackingPathId(ByVal PermitTypeCode As Integer, ByVal TrackingPathId As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("AP_AirPermitsListForPermitTypeTrackingId", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitTypeCode", SqlDbType.Int)
    '    cmd.Parameters("@PermitTypeCode").Value = PermitTypeCode
    '    cmd.Parameters.Add("@TrackingPathId", SqlDbType.Int)
    '    cmd.Parameters("@TrackingPathId").Value = TrackingPathId
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Permit List.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetDataReaderPermitList(ByVal PiID As Int32) As SqlDataReader
    '    Dim strConnSQL As String = Session("ConnStr")
    '    Dim Conn As New SqlConnection(strConnSQL)
    '    Dim cmd As New SqlCommand("dbo.sptblPermits_APGetPermitListForPiID")
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Connection = Conn
    '    Dim param As New SqlParameter("@PiID", SqlDbType.Int)
    '    param.Value = PiID
    '    cmd.Parameters.Add(param)

    '    Try
    '        Conn.Open()
    '        Return cmd.ExecuteReader
    '    Catch e As Exception
    '        Throw New Exception("Error retrieving Permit List data.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetPermitListForPiIDPermitTypeIdTrackingPathId(ByVal PiID As Integer, ByVal PermitTypeCode As Integer, ByVal TrackingPathId As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("AP_PermitsForPiIdPermitTypeIdTrackingPathId", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiID", SqlDbType.Int)
    '    cmd.Parameters("@PiID").Value = PiID
    '    cmd.Parameters.Add("@PermitTypeCode", SqlDbType.Int)
    '    cmd.Parameters("@PermitTypeCode").Value = PermitTypeCode
    '    cmd.Parameters.Add("@TrackingPathId", SqlDbType.Int)
    '    cmd.Parameters("@TrackingPathId").Value = TrackingPathId
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Permit List.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetAfflForPiIDAfflTypeID(ByVal PiID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblAffliations_GetRSForPiIDAfflType", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiID", SqlDbType.Int)
    '    cmd.Parameters("@PiID").Value = PiID
    '    cmd.Parameters.Add("@AfflType", SqlDbType.Int)
    '    cmd.Parameters("@AfflType").Value = 1
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting affliations for PiID.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetRelations() As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermitRelationship_GetRSCombo", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting relations.  " & e.Message)
    '    End Try
    'End Function

    'Public Function tblPermit_GetPiNameForPiID(ByVal PiID As String) As String
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblProgInterest_GetGenDataForPiID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiID", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@PiID").Value = PiID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        If dr.Read() Then
    '            Return dr(2) & " - " & dr(0)
    '        Else
    '            Return "Not available"
    '        End If
    '        dr.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting PiName.  " & e.Message)
    '    End Try
    'End Function

    'Public Function tblPermit_GetPiName(ByVal ProgID As String) As String
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblProgInterest_GetPiNameForProgID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@ProgID", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@ProgID").Value = ProgID
    '    cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
    '    cmd.Parameters("@PiTypeID").Value = 3
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        If dr.Read() Then
    '            Return dr(0)
    '        Else
    '            Return "Not available"
    '        End If
    '        dr.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting PiName.  " & e.Message)
    '    End Try
    'End Function

    'Public Function tblPermit_GetRSComboPermitStatus() As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermitStatus_GetRSCombo", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Permit Status list for combo.  " & e.Message)
    '    End Try
    'End Function

    'Public Function tblPermit_GetRSComboOtherPermits(ByVal PiID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermits_GetPermitsForPiIDPiTypeID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiID", SqlDbType.Int)
    '    cmd.Parameters("@PiID").Value = PiID
    '    cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
    '    cmd.Parameters("@PiTypeID").Value = 3
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Other Permits list for combo.  " & e.Message)
    '    End Try
    'End Function

    'Public Function AddEditPermit(ByVal PermitID As String, ByVal PermitName As String, _
    'ByVal PermitNumber As String, ByVal AltPermitNumber As String, _
    'ByVal AltPermitNumberSource As String, ByVal PermitTypeCode As Integer, _
    'ByVal PermitStatusCode As String, ByVal PermitStartDate As String, _
    'ByVal PermitForCode As Integer, ByVal PermitDesc As String, _
    'ByVal PermitExpires As String, ByVal PermitteeNameAsIssued As String, _
    'ByVal PermitParentID As Integer, ByVal LegalNoticeRequired As String, _
    'ByVal AccountingFund As Int16, ByVal AmendmentNumber As Int16, _
    'ByVal PermitFeeRequired As String, ByVal PermitOneTimeUse As String, _
    'ByVal PermitCoversCode As Integer, ByVal PermitComment As String, _
    'ByVal RegsComment As String, _
    'ByVal PiList As String, ByVal PiTypeID As Integer, ByVal POList As String, _
    'ByVal UserID As String, ByVal ExtendedTo As String) As String

    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    If PermitID = "0" Then
    '        cmd = New SqlCommand("sptblPermits_AddNew", cn)
    '        cmd.CommandType = CommandType.StoredProcedure
    '    Else
    '        cmd = New SqlCommand("sptblPermits_UpdateNew", cn)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '        cmd.Parameters("@PermitID").Value = Convert.ToInt32(PermitID)
    '    End If
    '    cmd.Parameters.Add("@PermitName", SqlDbType.VarChar, 60)
    '    cmd.Parameters("@PermitName").Value = PermitName
    '    cmd.Parameters.Add("@PermitNumber", SqlDbType.VarChar, 60)
    '    cmd.Parameters("@PermitNumber").Value = PermitNumber
    '    cmd.Parameters.Add("@AltPermitNumber", SqlDbType.VarChar, 60)
    '    cmd.Parameters("@AltPermitNumber").Value = AltPermitNumber
    '    cmd.Parameters.Add("@AltPermitNumberSource", SqlDbType.VarChar, 60)
    '    cmd.Parameters("@AltPermitNumberSource").Value = AltPermitNumberSource
    '    cmd.Parameters.Add("@PermitTypeCode", SqlDbType.SmallInt)
    '    cmd.Parameters("@PermitTypeCode").Value = PermitTypeCode
    '    cmd.Parameters.Add("@PermitStatusCode", SqlDbType.Char, 1)
    '    cmd.Parameters("@PermitStatusCode").Value = PermitStatusCode
    '    cmd.Parameters.Add("@PermitStartDate", SqlDbType.DateTime)
    '    If PermitStartDate = "" Then
    '        cmd.Parameters("@PermitStartDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitStartDate").Value = PermitStartDate
    '    End If
    '    cmd.Parameters.Add("@PermitForCode", SqlDbType.TinyInt)
    '    cmd.Parameters("@PermitForCode").Value = PermitForCode
    '    cmd.Parameters.Add("@PermitDesc", SqlDbType.VarChar, 100)
    '    cmd.Parameters("@PermitDesc").Value = PermitDesc
    '    cmd.Parameters.Add("@PermitExpires", SqlDbType.DateTime)
    '    If PermitExpires = "" Then
    '        cmd.Parameters("@PermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitExpires").Value = PermitExpires
    '    End If
    '    cmd.Parameters.Add("@PermitteeNameAsIssued", SqlDbType.VarChar, 100)
    '    cmd.Parameters("@PermitteeNameAsIssued").Value = PermitteeNameAsIssued
    '    cmd.Parameters.Add("@PermitParentID", SqlDbType.Int)
    '    cmd.Parameters("@PermitParentID").Value = System.DBNull.Value

    '    cmd.Parameters.Add("@LegalNoticeRequired", SqlDbType.Char, 1)
    '    cmd.Parameters("@LegalNoticeRequired").Value = LegalNoticeRequired

    '    cmd.Parameters.Add("@AccountingFund", SqlDbType.SmallInt)
    '    cmd.Parameters("@AccountingFund").Value = AccountingFund

    '    cmd.Parameters.Add("@AmendmentNumber", SqlDbType.TinyInt)
    '    cmd.Parameters("@AmendmentNumber").Value = AmendmentNumber

    '    cmd.Parameters.Add("@PermitFeeRequired", SqlDbType.Char, 1)
    '    cmd.Parameters("@PermitFeeRequired").Value = PermitFeeRequired

    '    cmd.Parameters.Add("@PermitOneTimeUse", SqlDbType.Char, 1)
    '    cmd.Parameters("@PermitOneTimeUse").Value = PermitOneTimeUse
    '    cmd.Parameters.Add("@PermitCoversCode", SqlDbType.SmallInt)
    '    cmd.Parameters("@PermitCoversCode").Value = PermitCoversCode
    '    cmd.Parameters.Add("@PermitComment", SqlDbType.VarChar, 1000)
    '    cmd.Parameters("@PermitComment").Value = PermitComment
    '    cmd.Parameters.Add("@RegsComment", SqlDbType.VarChar, 1000)
    '    cmd.Parameters("@RegsComment").Value = RegsComment
    '    cmd.Parameters.Add("@PiList", SqlDbType.VarChar, 1000)
    '    cmd.Parameters("@PiList").Value = PiList
    '    cmd.Parameters.Add("@PiTypeID", SqlDbType.SmallInt)
    '    cmd.Parameters("@PiTypeID").Value = PiTypeID
    '    cmd.Parameters.Add("@POList", SqlDbType.VarChar, 800)
    '    cmd.Parameters("@POList").Value = POList
    '    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@UserID").Value = UserID
    '    cmd.Parameters.Add("@ExtendedTo", SqlDbType.DateTime)
    '    If ExtendedTo = "" Then
    '        cmd.Parameters("@ExtendedTo").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@ExtendedTo").Value = ExtendedTo
    '    End If
    '    If PermitID = "0" Then
    '        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '        cmd.Parameters("@PermitID").Direction = ParameterDirection.Output
    '    End If
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return cmd.Parameters("@PermitID").Value

    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error adding/editing Permit.  " & e.Message)
    '    End Try
    'End Function

    'Public Sub delPermit(ByVal PermitID As Integer)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    cmd = New SqlCommand("sptblPermits_Delete", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error deleting permit.  " & e.Message)
    '    End Try
    'End Sub

    'Public Sub updatePermitTrackingPath(ByVal PermitID As Integer, ByVal TrackingPathId As Integer)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    cmd = New SqlCommand("sptblPermits_UpdatePermitTrackingPath", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@TrackingPathId ", SqlDbType.SmallInt)
    '    cmd.Parameters("@TrackingPathId ").Value = TrackingPathId
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error updating Tracking Path.  " & e.Message)
    '    End Try
    'End Sub

    'Public Sub updateTemporaryPermit(ByVal PermitID As Integer, ByVal TemporaryPermit As String)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    cmd = New SqlCommand("sptblPermits_UpdateTemporaryPermit", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@TemporaryPermit ", SqlDbType.Char, 1)
    '    cmd.Parameters("@TemporaryPermit ").Value = IIf(TemporaryPermit <> "Y", "N", "Y")
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error updating temporary permit.  " & e.Message)
    '    End Try
    'End Sub

    'Public Sub delRegulations(ByVal PermitID As Integer)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    cmd = New SqlCommand("sptblPermitStatute_DeleteForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error deleting Regulation.  " & e.Message)
    '    End Try
    'End Sub

    'Public Sub addRegulations(ByVal PermitID As Integer, ByVal StatuteID As Integer)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    cmd = New SqlCommand("sptblPermitStatute_Add", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@StatuteID", SqlDbType.Int)
    '    cmd.Parameters("@StatuteID").Value = StatuteID
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error adding Regulation.  " & e.Message)
    '    End Try
    'End Sub

    'Public Sub addPermitRelations(ByVal PermitID As Integer, ByVal RelatedPermitID As Integer, ByVal RelationshipID As Int16)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    cmd = New SqlCommand("sptblPermitParent_Add", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@RelatedPermitID", SqlDbType.Int)
    '    cmd.Parameters("@RelatedPermitID").Value = RelatedPermitID
    '    cmd.Parameters.Add("@RelationshipID", SqlDbType.TinyInt)
    '    cmd.Parameters("@RelationshipID").Value = RelationshipID
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error adding Permit Relation.  " & e.Message)
    '    End Try
    'End Sub

    'Public Sub delPermitRelations(ByVal PermitID As Integer)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand
    '    cmd = New SqlCommand("sptblPermitParent_Delete", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error deleting Permit Relation.  " & e.Message)
    '    End Try
    'End Sub

    'Public Function GetProjectOfficersAfflID(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblAffliations_GetAfflForPermitIDAfflTypeID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@AfflTypeCode", SqlDbType.Int)
    '    cmd.Parameters("@AfflTypeCode").Value = 31
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting project officers.  " & e.Message)
    '    End Try
    'End Function

    'Public Function tblPermits_GetPermitInfo(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblPermits_GetAirRSForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Permit Info.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetRegulations() As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblCitationStatute_GetRSDeAirRegs", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Regulations.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetSelectedRegulationsForPermitID(ByVal PermitID As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPermitStatute_GetRSForPiTypeIDDeRegs_ForPermitID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiTypeID", SqlDbType.TinyInt)
    '    cmd.Parameters("@PiTypeID").Value = 3
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Selected Regs for Permit.  " & e.Message)
    '    End Try
    'End Function

    'Public Function GetPermitStatus(ByVal PermitID As Integer) As String
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.AP_sptblPermits_PermitStatus_Get", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        dr.Read()
    '        GetPermitStatus = dr("PermitStatusCode").ToString()
    '        dr.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting Selected Regs for Permit.  " & e.Message)
    '    End Try
    'End Function

    'Public Function UpdatePermitStatus(ByVal PermitID As Integer, ByVal PermitStatusType As String) As Integer
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.AP_sptblPermits_UpdatePermitStatus", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.Parameters.Add("@PermitStatusCode", SqlDbType.Char, 1)
    '    cmd.Parameters("@PermitStatusCode").Value = PermitStatusType
    '    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@UserID").Value = Session.Item("AirDeUser")
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error setting permit status.  " & e.Message)
    '    End Try
    'End Function

    'Public Function runSpForPermitsUpdate(ByVal PermitID As Integer, ByVal PermitStartDate As String, ByVal InEffectFromDate As String, ByVal PermitExpires As String) As Integer
    '    Dim cn As New System.Data.SqlClient.SqlConnection(Session("ConnStr"))
    '    Dim cmd As New System.Data.SqlClient.SqlCommand("AP_sptblPermits_Update", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID

    '    cmd.Parameters.Add("@PermitStartDate", SqlDbType.DateTime)
    '    If PermitStartDate Is Nothing Or PermitStartDate = "" Then
    '        cmd.Parameters("@PermitStartDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitStartDate").Value = PermitStartDate
    '    End If

    '    cmd.Parameters.Add("@InEffectFromDate", SqlDbType.DateTime)
    '    If InEffectFromDate Is Nothing Or InEffectFromDate = "" Then
    '        cmd.Parameters("@InEffectFromDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@InEffectFromDate").Value = InEffectFromDate
    '    End If
    '    cmd.Parameters.Add("@PermitExpires", SqlDbType.DateTime)
    '    If PermitExpires Is Nothing Or PermitExpires = "" Then
    '        cmd.Parameters("@PermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitExpires").Value = PermitExpires
    '    End If
    '    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@UserID").Value = Session.Item("AirDeUser")

    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return 1

    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error updating Permit.  " & e.Message)
    '    End Try
    'End Function  'runSpForPermitsUpdate

    'Public Function runSpForPermitsUpdateWithConst(ByVal PermitID As Integer, _
    'ByVal PermitStartDate As String, ByVal InEffectFromDate As String, ByVal PermitExpires As String, _
    'ByVal ConstPermitStartDate As String, ByVal ConstPermitEffectiveDate As String, ByVal ConstPermitExpires As String, _
    'ByVal OrigPermitExpires As String, ByVal OrigConstPermitExpires As String, ByVal PermitStatusCode As String) As Integer
    '    Dim cn As New System.Data.SqlClient.SqlConnection(Session("ConnStr"))

    '    Dim cmd As New System.Data.SqlClient.SqlCommand("AP_sptblPermits_UpdateWithConst", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID

    '    cmd.Parameters.Add("@PermitStartDate", SqlDbType.DateTime)
    '    If PermitStartDate Is Nothing Or PermitStartDate = "" Then
    '        cmd.Parameters("@PermitStartDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitStartDate").Value = PermitStartDate
    '    End If

    '    cmd.Parameters.Add("@InEffectFromDate", SqlDbType.DateTime)
    '    If InEffectFromDate Is Nothing Or InEffectFromDate = "" Then
    '        cmd.Parameters("@InEffectFromDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@InEffectFromDate").Value = InEffectFromDate
    '    End If

    '    cmd.Parameters.Add("@PermitExpires", SqlDbType.DateTime)
    '    If PermitExpires Is Nothing Or PermitExpires = "" Then
    '        cmd.Parameters("@PermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitExpires").Value = PermitExpires
    '    End If

    '    cmd.Parameters.Add("@ConstPermitStartDate", SqlDbType.DateTime)
    '    If ConstPermitStartDate Is Nothing Or ConstPermitStartDate = "" Then
    '        cmd.Parameters("@ConstPermitStartDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@ConstPermitStartDate").Value = ConstPermitStartDate
    '    End If

    '    cmd.Parameters.Add("@ConstPermitEffectiveDate", SqlDbType.DateTime)
    '    If ConstPermitEffectiveDate Is Nothing Or ConstPermitEffectiveDate = "" Then
    '        cmd.Parameters("@ConstPermitEffectiveDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@ConstPermitEffectiveDate").Value = ConstPermitEffectiveDate
    '    End If

    '    cmd.Parameters.Add("@ConstPermitExpires", SqlDbType.DateTime)
    '    If ConstPermitExpires Is Nothing Or ConstPermitExpires = "" Then
    '        cmd.Parameters("@ConstPermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@ConstPermitExpires").Value = ConstPermitExpires
    '    End If

    '    cmd.Parameters.Add("@OrigPermitExpires", SqlDbType.DateTime)
    '    If OrigPermitExpires Is Nothing Or OrigPermitExpires = "" Then
    '        cmd.Parameters("@OrigPermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@OrigPermitExpires").Value = OrigPermitExpires
    '    End If

    '    cmd.Parameters.Add("@OrigConstPermitExpires", SqlDbType.DateTime)
    '    If OrigConstPermitExpires Is Nothing Or OrigConstPermitExpires = "" Then
    '        cmd.Parameters("@OrigConstPermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@OrigConstPermitExpires").Value = OrigConstPermitExpires
    '    End If

    '    cmd.Parameters.Add("@PermitStatusCode", SqlDbType.Char, 1)
    '    cmd.Parameters("@PermitStatusCode").Value = PermitStatusCode

    '    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@UserID").Value = Session.Item("AirDeUser")

    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return 1

    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error updating Permit.  " & e.Message)
    '    End Try
    'End Function  'runSpForPermitsUpdateWithConst


    'Public Function runSpForPermitsUpdateWithoutConst(ByVal PermitID As Integer, _
    '   ByVal PermitStartDate As String, ByVal InEffectFromDate As String, ByVal PermitExpires As String, _
    '   ByVal OrigPermitExpires As String, ByVal PermitStatusCode As String) As Integer
    '    Dim cn As New System.Data.SqlClient.SqlConnection(Session("ConnStr"))

    '    Dim cmd As New System.Data.SqlClient.SqlCommand("AP_sptblPermits_UpdateWithoutConst", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID

    '    cmd.Parameters.Add("@PermitStartDate", SqlDbType.DateTime)
    '    If PermitStartDate Is Nothing Or PermitStartDate = "" Then
    '        cmd.Parameters("@PermitStartDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitStartDate").Value = PermitStartDate
    '    End If

    '    cmd.Parameters.Add("@InEffectFromDate", SqlDbType.DateTime)
    '    If InEffectFromDate Is Nothing Or InEffectFromDate = "" Then
    '        cmd.Parameters("@InEffectFromDate").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@InEffectFromDate").Value = InEffectFromDate
    '    End If

    '    cmd.Parameters.Add("@PermitExpires", SqlDbType.DateTime)
    '    If PermitExpires Is Nothing Or PermitExpires = "" Then
    '        cmd.Parameters("@PermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@PermitExpires").Value = PermitExpires
    '    End If

    '    cmd.Parameters.Add("@OrigPermitExpires", SqlDbType.DateTime)
    '    If OrigPermitExpires Is Nothing Or OrigPermitExpires = "" Then
    '        cmd.Parameters("@OrigPermitExpires").Value = System.DBNull.Value
    '    Else
    '        cmd.Parameters("@OrigPermitExpires").Value = OrigPermitExpires
    '    End If

    '    cmd.Parameters.Add("@PermitStatusCode", SqlDbType.Char, 1)
    '    cmd.Parameters("@PermitStatusCode").Value = PermitStatusCode

    '    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@UserID").Value = Session.Item("AirDeUser")

    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '        Return 1
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error updating Permit.  " & e.Message)
    '    End Try
    'End Function  'runSpForPermitsUpdateWithConst

    'Public Sub DeletePermitID(ByVal PermitID As Integer)
    '    Dim cn As New System.Data.SqlClient.SqlConnection(Session("ConnStr"))

    '    Dim cmd As New System.Data.SqlClient.SqlCommand
    '    cmd.Connection = cn
    '    cmd.CommandType = CommandType.StoredProcedure

    '    ' Start: Delete records in tblPEvents
    '    cmd.CommandText = "AP_sptblPEvents_Del_PermitID"
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    cmd.ExecuteNonQuery()
    '    ' End: Delete records in tblPEvents

    '    ' Start: Delete records in tblPermitFee
    '    cmd.Parameters.Clear()
    '    cmd.CommandText = "AP_sptblPermitFee_Del_PermitID"
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID

    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()

    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error deleting Permit.  " & e.Message)
    '    End Try
    'End Sub

    'Public Function UpdatePIPermit(ByVal OldPiID As Int32, ByVal NewPiID As Int32, ByVal PermitID As Int32) As String
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("sptblPiPermit_Update", cn)
    '    cmd.CommandType = CommandType.StoredProcedure

    '    cmd.Parameters.Add("@OldPiID", SqlDbType.Int)
    '    cmd.Parameters("@OldPiID").Value = OldPiID

    '    cmd.Parameters.Add("@NewPiID", SqlDbType.Int)
    '    cmd.Parameters("@NewPiID").Value = NewPiID

    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID

    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error updating Permit.  " & e.ToString)
    '    End Try
    'End Function

    'Public Function GetPermitNosForProgIDPermitTypeID(ByVal PiID As Integer, ByVal PermitTypeCode As Integer) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblPermits_GetPermitsForPiIDPiTypeIDPermitTypeCode", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@PiID", SqlDbType.Int)
    '    cmd.Parameters("@PiID").Value = PiID
    '    cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
    '    cmd.Parameters("@PiTypeID").Value = 3
    '    cmd.Parameters.Add("@PermitTypeCode", SqlDbType.Int)
    '    cmd.Parameters("@PermitTypeCode").Value = PermitTypeCode
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error retrieving permit numbers.  " & e.Message)
    '    End Try
    'End Function
End Class

