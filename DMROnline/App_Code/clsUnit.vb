
Imports System.Data.SqlClient
Imports System.Data

Public Class clsUnit
    Inherits System.Web.UI.Page
    Private connection As String = Session("ConnStr")

    Public Function GetOutfalls(ByVal PermitID As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetOutfallsForPermitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of outfalls for PermitID= " + PermitID + ".  " & e.Message)
        End Try
    End Function

    Public Sub ReplaceOutfalls(ByVal PermitID As String, ByVal UnitIDList As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_ReplaceOutfallsForPermitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@UnitIDList", SqlDbType.VarChar, 500)
        cmd.Parameters("@UnitIDList").Value = UnitIDList
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error replacing outfalls for PermitID= " + PermitID + ".  " & e.Message)
        End Try
    End Sub

    Public Function GetUnitsForPermitNumber(ByVal PermitNumber As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetUnitsForPermitNo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PermitNumber", SqlDbType.VarChar, 20)
        cmd.Parameters("@PermitNumber").Value = PermitNumber
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Outfalls for Permit for Site.  " & e.Message)
        End Try
    End Function

    Public Function GetOutfallsForNPDESID(ByVal NPDESID As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetOutfallsForNPDESID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@NPDESID", SqlDbType.Char, 9)
        cmd.Parameters("@NPDESID").Value = NPDESID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Outfalls for Site.  " & e.Message)
        End Try
    End Function

    Public Function GetOtherOutfalls(ByVal UnitID As Integer) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetOtherOutfallForUnitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Outfalls for  Site.  " & e.Message)
        End Try
    End Function

    Public Function GetOutfallsForPiID(ByVal PiID As Integer) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetOutfallsForPiID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PiID", SqlDbType.Int)
        cmd.Parameters("@PiID").Value = PiID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Outfalls for Site.  " & e.Message)
        End Try
    End Function

    Public Function GetWater() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("sptblSurfaceWater_GetRsCombo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of receiving waters.  " & e.Message)
        End Try
    End Function

    Public Function GetOutfallTypes() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetOutfallTypes", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Outfall Type list.  " & e.Message)
        End Try
    End Function

    Public Function GetWasteTypes() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblWasteType_RSComboForPiTypeID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@PiTypeID", SqlDbType.Int)
        cmd.Parameters("@PiTypeID").Value = 4  'NPDES
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Outfall Waste Type list.  " & e.Message)
        End Try
    End Function

    Public Function GetPipeData(ByVal UnitID As Int32, ByVal PermitID As Int32) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPipeData", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Outfall data.  " & e.Message)
        End Try
    End Function

    Public Sub GetLocationInfoForOutfall(ByVal UnitID As String, ByRef UnitTypeID As String, ByRef LocID As String, ByRef USGSHydrologicUnitCode As String, _
ByRef HorizontalAccuracyM As String, ByRef EPAHorizontalDatumCode As String, ByRef HorizontalDatum As String, _
ByRef ReferencePtCode As String, ByRef x As String, ByRef Y As String, ByRef Latitude As String, ByRef Longitude As String, _
ByRef EPAHorizontalMethodCode As String, ByRef SourceMapScale As String, ByRef EPAHorizontalCollectionMethodText As String, ByRef ReferencePt As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblUnit_GetUnitTypeIDLocInfoForUnitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@UnitTypeID", SqlDbType.VarChar, 10)
        cmd.Parameters("@UnitTypeID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@LocID", SqlDbType.VarChar, 10)
        cmd.Parameters("@LocID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@USGSHydrologicUnitCode", SqlDbType.VarChar, 20)
        cmd.Parameters("@USGSHydrologicUnitCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@HorizontalAccuracyM", SqlDbType.VarChar, 10)
        cmd.Parameters("@HorizontalAccuracyM").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@EPAHorizontalDatumCode", SqlDbType.VarChar, 10)
        cmd.Parameters("@EPAHorizontalDatumCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@HorizontalDatum", SqlDbType.VarChar, 10)
        cmd.Parameters("@HorizontalDatum").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ReferencePtCode", SqlDbType.VarChar, 10)
        cmd.Parameters("@ReferencePtCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@X", SqlDbType.VarChar, 10)
        cmd.Parameters("@X").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Y", SqlDbType.VarChar, 10)
        cmd.Parameters("@Y").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Latitude", SqlDbType.VarChar, 12)
        cmd.Parameters("@Latitude").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Longitude", SqlDbType.VarChar, 12)
        cmd.Parameters("@Longitude").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@EPAHorizontalMethodCode", SqlDbType.VarChar, 10)
        cmd.Parameters("@EPAHorizontalMethodCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@SourceMapScale", SqlDbType.VarChar, 10)
        cmd.Parameters("@SourceMapScale").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@EPAHorizontalCollectionMethodText", SqlDbType.VarChar, 60)
        cmd.Parameters("@EPAHorizontalCollectionMethodText").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ReferencePt", SqlDbType.VarChar, 60)
        cmd.Parameters("@ReferencePt").Direction = ParameterDirection.Output

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            UnitTypeID = cmd.Parameters("@UnitTypeID").Value
            LocID = cmd.Parameters("@LocID").Value
            USGSHydrologicUnitCode = cmd.Parameters("@USGSHydrologicUnitCode").Value
            HorizontalAccuracyM = cmd.Parameters("@HorizontalAccuracyM").Value
            EPAHorizontalDatumCode = cmd.Parameters("@EPAHorizontalDatumCode").Value
            HorizontalDatum = cmd.Parameters("@HorizontalDatum").Value
            ReferencePtCode = cmd.Parameters("@ReferencePtCode").Value
            x = cmd.Parameters("@X").Value
            Y = cmd.Parameters("@Y").Value
            Latitude = cmd.Parameters("@Latitude").Value
            Longitude = cmd.Parameters("@Longitude").Value
            EPAHorizontalMethodCode = cmd.Parameters("@EPAHorizontalMethodCode").Value
            SourceMapScale = cmd.Parameters("@SourceMapScale").Value
            EPAHorizontalCollectionMethodText = cmd.Parameters("@EPAHorizontalCollectionMethodText").Value
            ReferencePt = cmd.Parameters("@ReferencePt").Value

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting location info for an outfall.  " & e.Message)
        End Try
    End Sub

    Public Sub GetUnitData(ByVal UnitID As String, ByRef UnitTypeID As String, ByRef LocID As String, ByRef UnitName As String, _
    ByRef UnitDesc As String, ByRef WasteTypeID As String, ByRef SurfaceWaterID As String, ByRef X As String, _
    ByRef Y As String, ByRef ReferencePt As String, ByRef ReferencePtCode As String, ByRef HorizontalMethod As String, ByRef HorizontalMethodCode As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetUnitData", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@UnitTypeID", SqlDbType.VarChar, 10)
        cmd.Parameters("@UnitTypeID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@LocID", SqlDbType.VarChar, 10)
        cmd.Parameters("@LocID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@UnitName", SqlDbType.VarChar, 3)
        cmd.Parameters("@UnitName").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@UnitDesc", SqlDbType.VarChar, 30)
        cmd.Parameters("@UnitDesc").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@X", SqlDbType.VarChar, 10)
        cmd.Parameters("@X").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Y", SqlDbType.VarChar, 10)
        cmd.Parameters("@Y").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@HorizontalMethod", SqlDbType.VarChar, 80)
        cmd.Parameters("@HorizontalMethod").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ReferencePt", SqlDbType.VarChar, 50)
        cmd.Parameters("@ReferencePt").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@HorizontalMethodCode", SqlDbType.VarChar, 8)
        cmd.Parameters("@HorizontalMethodCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ReferencePtCode", SqlDbType.VarChar, 50)
        cmd.Parameters("@ReferencePtCode").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@WasteTypeID", SqlDbType.VarChar, 8)
        cmd.Parameters("@WasteTypeID").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@SurfaceWaterID", SqlDbType.VarChar, 8)
        cmd.Parameters("@SurfaceWaterID").Direction = ParameterDirection.Output

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            UnitTypeID = cmd.Parameters("@UnitTypeID").Value
            LocID = cmd.Parameters("@LocID").Value
            UnitName = cmd.Parameters("@UnitName").Value
            UnitDesc = cmd.Parameters("@UnitDesc").Value
            ReferencePt = cmd.Parameters("@ReferencePt").Value
            ReferencePtCode = cmd.Parameters("@ReferencePtCode").Value
            X = cmd.Parameters("@X").Value
            Y = cmd.Parameters("@Y").Value
            HorizontalMethod = cmd.Parameters("@HorizontalMethod").Value
            HorizontalMethodCode = cmd.Parameters("@HorizontalMethodCode").Value
            WasteTypeID = cmd.Parameters("@WasteTypeID").Value
            SurfaceWaterID = cmd.Parameters("@SurfaceWaterID").Value

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting data for an outfall.  " & e.Message)
        End Try
    End Sub

    Public Sub SaveUnitData(ByVal UnitID As Int32, ByRef PermitID As Int32, ByRef LimitGroup As String, _
    ByRef WasteTypeID As String, ByRef MonitoringStartDate As String, _
    ByRef TotalNumberOfReports As String, ByVal NumberOfMonthsInReportingPeriod As String, _
    ByVal InitialStateSubmissionDate As String, ByVal NumberOfUnitsInSubmissionPeriodState As String, _
    ByVal PipeInactiveDate As String, ByVal ALLP As String, _
    ByVal FinalLimitsStartDate As String, ByVal FinalLimitsEndDate As String, _
    ByVal InterimLimitsStartDate As String, ByVal InterimLimitsEndDate As String, _
    ByVal InitialLimitsStartDate As String, ByVal InitialLimitsEndDate As String, _
    ByVal UserID As String)
        If TotalNumberOfReports.Length < 3 And TotalNumberOfReports.Length > 0 Then
            TotalNumberOfReports = Right("00" + TotalNumberOfReports, 3)
        End If
        If NumberOfMonthsInReportingPeriod.Length < 3 And NumberOfMonthsInReportingPeriod.Length > 0 Then
            NumberOfMonthsInReportingPeriod = Right("00" + NumberOfMonthsInReportingPeriod, 3)
        End If

        If NumberOfUnitsInSubmissionPeriodState.Length = 1 Then
            NumberOfUnitsInSubmissionPeriodState = "0" + NumberOfUnitsInSubmissionPeriodState
        ElseIf NumberOfUnitsInSubmissionPeriodState.Length = 3 Then
            NumberOfUnitsInSubmissionPeriodState = Right(NumberOfUnitsInSubmissionPeriodState, 2)
        End If
        ALLP = ALLP.ToUpper

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_SavePipeData", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@LimitGroup", SqlDbType.Char, 1)
        cmd.Parameters("@LimitGroup").Value = LimitGroup
        cmd.Parameters.Add("@WasteTypeID", SqlDbType.SmallInt)
        cmd.Parameters("@WasteTypeID").Value = WasteTypeID
        cmd.Parameters.Add("@MonitoringStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@MonitoringStartDate").Value = MonitoringStartDate
        cmd.Parameters.Add("@TotalNumberOfReports", SqlDbType.VarChar, 3)
        cmd.Parameters("@TotalNumberOfReports").Value = TotalNumberOfReports
        cmd.Parameters.Add("@NumberOfMonthsInReportingPeriod", SqlDbType.VarChar, 3)
        cmd.Parameters("@NumberOfMonthsInReportingPeriod").Value = NumberOfMonthsInReportingPeriod
        cmd.Parameters.Add("@InitialStateSubmissionDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@InitialStateSubmissionDate").Value = InitialStateSubmissionDate
        cmd.Parameters.Add("@NumberOfUnitsInSubmissionPeriodState", SqlDbType.VarChar, 2)
        cmd.Parameters("@NumberOfUnitsInSubmissionPeriodState").Value = NumberOfUnitsInSubmissionPeriodState
        cmd.Parameters.Add("@PipeInactiveDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@PipeInactiveDate").Value = PipeInactiveDate
        cmd.Parameters.Add("@ALLP", SqlDbType.VarChar, 12)
        cmd.Parameters("@ALLP").Value = ALLP
        cmd.Parameters.Add("@FinalLimitsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@FinalLimitsStartDate").Value = FinalLimitsStartDate
        cmd.Parameters.Add("@FinalLimitsEndDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@FinalLimitsEndDate").Value = FinalLimitsEndDate
        cmd.Parameters.Add("@InterimLimitsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@InterimLimitsStartDate").Value = InterimLimitsStartDate
        cmd.Parameters.Add("@InterimLimitsEndDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@InterimLimitsEndDate").Value = InterimLimitsEndDate
        cmd.Parameters.Add("@InitialLimitsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@InitialLimitsStartDate").Value = InitialLimitsStartDate
        cmd.Parameters.Add("@InitialLimitsEndDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@InitialLimitsEndDate").Value = InitialLimitsEndDate
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 64)
        cmd.Parameters("@UserID").Value = UserID

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error saving data for an Outfall-Report Designator.  " & e.Message)
        End Try
    End Sub

    Public Sub SaveOutfallData(ByVal UnitID As Int32, ByRef LocID As Int32, _
    ByVal SurfaceWaterID As String, _
    ByVal UnitName As String, ByRef X As String, ByVal Y As String, _
    ByVal ReferencePtCode As String, ByVal HorizontalMethodCode As String, _
    ByVal OutfallDesc As String, ByVal UnitTypeID As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_AddUpdateOutfall", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@LocID", SqlDbType.Int)
        cmd.Parameters("@LocID").Value = LocID
        cmd.Parameters.Add("@UnitName", SqlDbType.VarChar, 3)
        cmd.Parameters("@UnitName").Value = UnitName
        cmd.Parameters.Add("@OutfallDesc", SqlDbType.VarChar, 30)
        cmd.Parameters("@OutfallDesc").Value = OutfallDesc
        cmd.Parameters.Add("@UnitTypeID", SqlDbType.SmallInt)
        cmd.Parameters("@UnitTypeID").Value = UnitTypeID
        cmd.Parameters.Add("@SurfaceWaterID", SqlDbType.SmallInt)
        cmd.Parameters("@SurfaceWaterID").Value = SurfaceWaterID
        cmd.Parameters.Add("@X", SqlDbType.VarChar, 20)
        cmd.Parameters("@X").Value = X
        cmd.Parameters.Add("@Y", SqlDbType.VarChar, 20)
        cmd.Parameters("@Y").Value = Y
        cmd.Parameters.Add("@ReferencePtCode", SqlDbType.VarChar, 3)
        cmd.Parameters("@ReferencePtCode").Value = ReferencePtCode
        cmd.Parameters.Add("@HorizontalMethodCode", SqlDbType.VarChar, 3)
        cmd.Parameters("@HorizontalMethodCode").Value = HorizontalMethodCode
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 64)
        cmd.Parameters("@UserID").Value = Session("WasteWaterDEUser")

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error saving outfall data.  " & e.Message)
        End Try
    End Sub
End Class
