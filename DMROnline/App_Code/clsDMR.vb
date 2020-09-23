Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data

Public Class clsDMR
    Inherits System.Web.UI.Page
    Private connection As String = Session("ConnStr")

    'Public Function GetDMRLimitsForUnitDate(ByVal UnitID As String, _
    'ByVal StartDate As String, ByVal PermitID As String) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetRSDMRForUnitDate", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@UnitID", SqlDbType.Int)
    '    cmd.Parameters("@UnitID").Value = UnitID
    '    cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 12)
    '    cmd.Parameters("@StartDate").Value = StartDate
    '    cmd.Parameters.Add("@PermitID", SqlDbType.Int)
    '    cmd.Parameters("@PermitID").Value = PermitID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State.Open = True Then cn.Close()
    '        Throw New Exception("Error getting DMR data for UnitID=" _
    '        + UnitID + e.Message)
    '    End Try
    'End Function

    Public Function GetDMRLimitsForUnitDateT(ByVal UnitID As String, _
   ByVal StartDate As String, ByVal PermitID As String, ByVal DisplayPage As String, ByRef Status As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetRSDMRForUnitDateT", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@StartDate").Value = StartDate
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@DisplayPage", SqlDbType.VarChar, 2)
        cmd.Parameters("@DisplayPage").Value = DisplayPage
        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 1)
        cmd.Parameters("@Status").Direction = ParameterDirection.Output
        cmd.Parameters("@Status").Value = "N"
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dr.Close()
            Status = Convert.ToString(cmd.Parameters("@Status").Value)
            'Return dr
            cn.Close()

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting DMR data for UnitID=" _
            + UnitID + e.Message)
        End Try

        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting DMR data for UnitID=" _
            + UnitID + e.Message)
        End Try
    End Function


    Public Function GetDMRPages(ByVal UnitID As String, _
    ByVal StartDate As String, ByVal PermitID As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetDisplayPageList", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@StartDate").Value = StartDate
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting DMR Page List for UnitID=" _
            + UnitID + e.Message)
        End Try
    End Function

    Public Function GetNextDMRDate(ByVal UnitID As String) As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetNextDMRDate", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@YearMonth", SqlDbType.VarChar, 6)
        cmd.Parameters("@YearMonth").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@YearMonth").Value
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting DMR data for UnitID=" _
            + UnitID + e.Message)
        End Try
    End Function

    Public Sub RollbackDMR(ByVal UnitID As String, ByVal StartDate As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_RollbackDMR", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@StartDate").Value = StartDate
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error rolling back EPA DMR submittal.  Details follow: " _
           + e.Message)
        End Try
    End Sub


    Public Function GetDMRReceivedDate(ByVal UnitID As String, ByVal StartDate As String) As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetDMRReceivedDate", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 10)
        cmd.Parameters("@StartDate").Value = StartDate
        cmd.Parameters.Add("@DMRDateReceived", SqlDbType.VarChar, 10)
        cmd.Parameters("@DMRDateReceived").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@DMRDateReceived").Value
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting DMR received date for UnitID=" _
            + UnitID + e.Message)
        End Try
    End Function

    Public Sub GetDMRComments(ByVal UnitID As String, ByVal PermitID As String, ByVal StartDate As String, ByRef DMRComments As String, ByRef DMRRejection As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetDMRComments", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@PermitID", SqlDbType.Int)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 10)
        cmd.Parameters("@StartDate").Value = StartDate
        cmd.Parameters.Add("@DMRComments", SqlDbType.VarChar, 1000)
        cmd.Parameters("@DMRComments").Direction = ParameterDirection.Output
        cmd.Parameters.Add("@DMRRejection", SqlDbType.VarChar, 2000)
        cmd.Parameters("@DMRRejection").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            DMRComments = cmd.Parameters("@DMRComments").Value
            DMRRejection = cmd.Parameters("@DMRRejection").Value
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting Comments for UnitID=" _
            + UnitID + e.Message)
        End Try
    End Sub


    Public Function GetSiteOutfall(ByVal UnitID As String) As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetSiteOutFallForUnitID", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@SiteOutfall", SqlDbType.VarChar, 200)
        cmd.Parameters("@SiteOutfall").Direction = ParameterDirection.Output
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            Return cmd.Parameters("@SiteOutfall").Value
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting site and outfall name. " + e.Message)
        End Try
    End Function

    Public Sub AddEditDMRRow(ByVal ActResultsSetID As String, _
       ByVal UnitID As String, _
       ByVal ActID As String, _
       ByVal ActReceiptDate As String, _
       ByVal ActMonitorLocCode As String, _
       ByVal ActSampleTypecode As String, _
       ByVal LimitGroup As String, _
       ByVal FrequencyID As String, _
       ByVal ActResultsStartDate As String, _
       ByVal ActResultsEndDate As String, _
       ByVal ParamID As String, _
       ByVal MinCActResultsID As String, _
       ByVal MinC As String, _
       ByVal MinCVMC As String, _
       ByVal MinCLimitID As String, _
       ByVal MinCNDI As String, _
       ByVal AvgCActResultsID As String, _
       ByVal AvgC As String, _
       ByVal AvgCVMC As String, _
       ByVal AvgCLimitID As String, _
       ByVal AvgCNDI As String, _
       ByVal MaxCActResultsID As String, _
       ByVal MaxC As String, _
       ByVal MaxCVMC As String, _
       ByVal MaxCLimitID As String, _
       ByVal MaxCNDI As String, _
       ByVal ConcMUnitsID As String, _
       ByVal AvgQActResultsID As String, _
       ByVal AvgQ As String, _
       ByVal AvgQVMC As String, _
       ByVal AvgQLimitID As String, _
       ByVal AvgQNDI As String, _
       ByVal MaxQActResultsID As String, _
       ByVal MaxQ As String, _
       ByVal MaxQVMC As String, _
       ByVal MaxQLimitID As String, _
       ByVal MaxQNDI As String, _
       ByVal QuantMUnitsID As String, _
       ByVal NumberOfExcursions As String, _
       ByVal UserID As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_AddEditDMRRow", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ActResultsSetID", SqlDbType.VarChar, 15)
        cmd.Parameters("@ActResultsSetID").Value = ActResultsSetID
        cmd.Parameters.Add("@UnitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@ActID", SqlDbType.VarChar, 15)
        cmd.Parameters("@ActID").Value = ActID
        cmd.Parameters.Add("@ActReceiptDate", SqlDbType.VarChar, 50)
        cmd.Parameters("@ActReceiptDate").Value = ActReceiptDate

        cmd.Parameters.Add("@ActMonitorLocCode", SqlDbType.VarChar, 1)
        cmd.Parameters("@ActMonitorLocCode").Value = ActMonitorLocCode
        cmd.Parameters.Add("@ActSampleTypeCode", SqlDbType.VarChar, 2)
        cmd.Parameters("@ActSampleTypeCode").Value = ActSampleTypecode

        cmd.Parameters.Add("@LimitGroup", SqlDbType.VarChar, 1)
        cmd.Parameters("@LimitGroup").Value = LimitGroup
        cmd.Parameters.Add("@FrequencyID", SqlDbType.VarChar, 15)
        cmd.Parameters("@FrequencyID").Value = FrequencyID

        cmd.Parameters.Add("@ActResultsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsStartDate").Value = ActResultsStartDate
        cmd.Parameters.Add("@ActResultsEndDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsEndDate").Value = ActResultsEndDate
        cmd.Parameters.Add("@ParamID", SqlDbType.VarChar, 15)
        cmd.Parameters("@ParamID").Value = ParamID
        'MinC
        cmd.Parameters.Add("@MinCActResultsID", SqlDbType.VarChar, 15)
        cmd.Parameters("@MinCActResultsID").Value = MinCActResultsID
        cmd.Parameters.Add("@MinC", SqlDbType.VarChar, 50)
        cmd.Parameters("@MinC").Value = MinC
        cmd.Parameters.Add("@MinCVMC", SqlDbType.VarChar, 1)
        cmd.Parameters("@MinCVMC").Value = MinCVMC
        cmd.Parameters.Add("@MinCLimitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@MinCLimitID").Value = MinCLimitID
        cmd.Parameters.Add("@MinCNDI", SqlDbType.Char, 1)
        cmd.Parameters("@MinCNDI").Value = MinCNDI
        'AvgC
        cmd.Parameters.Add("@AvgCActResultsID", SqlDbType.VarChar, 15)
        cmd.Parameters("@AvgCActResultsID").Value = AvgCActResultsID
        cmd.Parameters.Add("@AvgC", SqlDbType.VarChar, 50)
        cmd.Parameters("@AvgC").Value = AvgC
        cmd.Parameters.Add("@AvgCVMC", SqlDbType.VarChar, 1)
        cmd.Parameters("@AvgCVMC").Value = AvgCVMC
        cmd.Parameters.Add("@AvgCLimitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@AvgCLimitID").Value = AvgCLimitID
        cmd.Parameters.Add("@AvgCNDI", SqlDbType.Char, 1)
        cmd.Parameters("@AvgCNDI").Value = AvgCNDI
        'MaxC
        cmd.Parameters.Add("@MaxCActResultsID", SqlDbType.VarChar, 15)
        cmd.Parameters("@MaxCActResultsID").Value = MaxCActResultsID
        cmd.Parameters.Add("@MaxC", SqlDbType.VarChar, 50)
        cmd.Parameters("@MaxC").Value = MaxC
        cmd.Parameters.Add("@MaxCVMC", SqlDbType.VarChar, 1)
        cmd.Parameters("@MaxCVMC").Value = MaxCVMC
        cmd.Parameters.Add("@MaxCLimitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@MaxCLimitID").Value = MaxCLimitID
        cmd.Parameters.Add("@MaxCNDI", SqlDbType.Char, 1)
        cmd.Parameters("@MaxCNDI").Value = MaxCNDI

        cmd.Parameters.Add("@ConcMUnitsID", SqlDbType.VarChar, 15)
        cmd.Parameters("@ConcMUnitsID").Value = ConcMUnitsID

        'AvgQ
        cmd.Parameters.Add("@AvgQActResultsID", SqlDbType.VarChar, 15)
        cmd.Parameters("@AvgQActResultsID").Value = AvgQActResultsID
        cmd.Parameters.Add("@AvgQ", SqlDbType.VarChar, 50)
        cmd.Parameters("@AvgQ").Value = AvgQ
        cmd.Parameters.Add("@AvgQVMC", SqlDbType.VarChar, 1)
        cmd.Parameters("@AvgQVMC").Value = AvgQVMC
        cmd.Parameters.Add("@AvgQLimitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@AvgQLimitID").Value = AvgQLimitID
        cmd.Parameters.Add("@AvgQNDI", SqlDbType.Char, 1)
        cmd.Parameters("@AvgQNDI").Value = AvgQNDI
        'MaxQ
        cmd.Parameters.Add("@MaxQActResultsID", SqlDbType.VarChar, 15)
        cmd.Parameters("@MaxQActResultsID").Value = MaxQActResultsID
        cmd.Parameters.Add("@MaxQ", SqlDbType.VarChar, 50)
        cmd.Parameters("@MaxQ").Value = MaxQ
        cmd.Parameters.Add("@MaxQVMC", SqlDbType.VarChar, 1)
        cmd.Parameters("@MaxQVMC").Value = MaxQVMC
        cmd.Parameters.Add("@MaxQLimitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@MaxQLimitID").Value = MaxQLimitID
        cmd.Parameters.Add("@MaxQNDI", SqlDbType.Char, 1)
        cmd.Parameters("@MaxQNDI").Value = MaxQNDI

        cmd.Parameters.Add("@QuantMUnitsID", SqlDbType.VarChar, 15)
        cmd.Parameters("@QuantMUnitsID").Value = QuantMUnitsID


        cmd.Parameters.Add("@NumberOfExcursions", SqlDbType.VarChar, 15)
        cmd.Parameters("@NumberOfExcursions").Value = NumberOfExcursions
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 64)
        cmd.Parameters("@UserID").Value = UserID
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error saving DMR data: " + e.Message)
        End Try
    End Sub

    Public Sub DMRBulkUpdate( _
      ByVal UnitID As String, _
      ByVal ActID As String, _
      ByVal ActReceiptDate As String, _
      ByVal ActResultsStartDate As String, _
      ByVal ActResultsEndDate As String, _
      ByVal UserID As String, _
      ByVal BulkData1 As String, _
      ByVal BulkData2 As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_BulkDMRSave", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@UnitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@ActID", SqlDbType.VarChar, 15)
        cmd.Parameters("@ActID").Value = ActID
        cmd.Parameters.Add("@ActReceiptDate", SqlDbType.VarChar, 50)
        cmd.Parameters("@ActReceiptDate").Value = ActReceiptDate

        cmd.Parameters.Add("@ActResultsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsStartDate").Value = ActResultsStartDate
        cmd.Parameters.Add("@ActResultsEndDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsEndDate").Value = ActResultsEndDate

        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 64)
        cmd.Parameters("@UserID").Value = UserID

        cmd.Parameters.Add("@BulkData1", SqlDbType.VarChar, 8000)
        cmd.Parameters("@BulkData1").Value = BulkData1
        cmd.Parameters.Add("@BulkData2", SqlDbType.VarChar, 8000)
        cmd.Parameters("@BulkData2").Value = BulkData2

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error saving DMR data: " + e.Message)
        End Try
    End Sub

    Public Sub DMRStatusUpdate(ByVal ActID As String, ByVal UserID As String, ByVal Status As String, ByVal Comments As String, ByVal ActResultsStartDate As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_DMRStatusUpdate", cn)
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@UnitID", SqlDbType.VarChar, 15)
        'cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@ActID", SqlDbType.VarChar, 15)
        cmd.Parameters("@ActID").Value = ActID
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 64)
        cmd.Parameters("@UserID").Value = UserID
        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 1)
        cmd.Parameters("@Status").Value = Status

        cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 1000)
        cmd.Parameters("@Comments").Value = Comments

        cmd.Parameters.Add("@ActResultsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsStartDate").Value = ActResultsStartDate

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error saving DMR data: " + e.Message)
        End Try
    End Sub

    Public Sub DMRStatusCORPath(ByVal ActID As String, ByVal RedirectPath As String, ByVal ActResultsStartDate As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_DMRStatusSaveCORPath", cn)
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@UnitID", SqlDbType.VarChar, 15)
        'cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@ActID", SqlDbType.VarChar, 15)
        cmd.Parameters("@ActID").Value = ActID
        cmd.Parameters.Add("@CORPath", SqlDbType.VarChar, 1000)
        cmd.Parameters("@CORPath").Value = RedirectPath
        cmd.Parameters.Add("@ActResultsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsStartDate").Value = ActResultsStartDate

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error saving DMR Copy of Record Location: " + e.Message)
        End Try
    End Sub

    Public Function GetValueModifierList() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblValueModifier_GetRSComboNPDES", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting value modifier list.  " & e.Message)
        End Try
    End Function

    Public Function GetNoDataIndicator() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblNoDataIndicator_GetRSCombo", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of No Data Indicators.  " & e.Message)
        End Try
    End Function

    Public Function GetNoDataIndicatorCodes() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblNoDataIndicator_GetRSComboCode", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of No Data Indicator Codess.  " & e.Message)
        End Try
    End Function

    Public Function GetValueModifierCodes() As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblValueModifier_GetNPDESCode", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting value modifier codes.  " & e.Message)
        End Try
    End Function

    Public Sub CheckComplete(ByVal PermitID As String, ByVal UnitID As Integer, ByVal ActResultsStartDate As String, ByRef StrComplete As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_CheckDMRComplete", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@PermitID", SqlDbType.VarChar, 15)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@CurrUnitID", SqlDbType.Int)
        cmd.Parameters("@CurrUnitID").Value = UnitID
        cmd.Parameters.Add("@ActResultsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsStartDate").Value = ActResultsStartDate
        cmd.Parameters.Add("@Complete", SqlDbType.VarChar, 100)
        cmd.Parameters("@Complete").Direction = ParameterDirection.Output
        cmd.Parameters("@Complete").Value = StrComplete

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            StrComplete = cmd.Parameters("@Complete").Value
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error checking whether DMR is ready for submission: " + e.Message)
        End Try
    End Sub

    Public Sub GetActID(ByVal PermitID As String, ByVal ActResultsStartDate As String, ByRef StrActID As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetActID", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@PermitID", SqlDbType.VarChar, 10)
        cmd.Parameters("@PermitID").Value = PermitID
        cmd.Parameters.Add("@ActResultsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsStartDate").Value = ActResultsStartDate
        cmd.Parameters.Add("@ActID", SqlDbType.VarChar, 10)
        cmd.Parameters("@ActID").Direction = ParameterDirection.Output
        cmd.Parameters("@ActID").Value = StrActID
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            StrActID = cmd.Parameters("@ActID").Value
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error finding internal Activity Reference: " + e.Message)
        End Try
    End Sub

    Public Sub DMRStatusReverse(ByVal ActID As String, ByVal UserID As String, ByVal ActResultsStartDate As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_DMRStatusReverse", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ActID", SqlDbType.VarChar, 10)
        cmd.Parameters("@ActID").Value = ActID
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 64)
        cmd.Parameters("@UserID").Value = UserID
        cmd.Parameters.Add("@ActResultsStartDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ActResultsStartDate").Value = ActResultsStartDate

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error Reversing DMR submission: " + e.Message)
        End Try
    End Sub

    Public Sub DMRStatusCheckSigned_Ext(ByVal ActID As String, ByRef Signed As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_DMRStatusCheckSigned_Ext", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@ActID", SqlDbType.VarChar, 10)
        cmd.Parameters("@ActID").Value = ActID
        cmd.Parameters.Add("@Signed", SqlDbType.VarChar, 1)
        cmd.Parameters("@Signed").Direction = ParameterDirection.Output
        cmd.Parameters("@Signed").Value = "N"

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            Signed = cmd.Parameters("@Signed").Value
            cn.Close()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error checking for Digitally Signed status of DMR data: " + e.Message)
        End Try
    End Sub

    Public Sub GetOutsideLimits(ByVal PageID As String, ByVal ResultsDate As String, ByVal PiID As String, ByVal UnitID As String, ByRef Results As String)

        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spDMR_OutsideLimits", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@PageID", SqlDbType.VarChar, 2)
        cmd.Parameters("@PageID").Value = PageID
        cmd.Parameters.Add("@PiID", SqlDbType.Int)
        cmd.Parameters("@PiID").Value = PiID
        cmd.Parameters.Add("@UnitID", SqlDbType.Int)
        cmd.Parameters("@UnitID").Value = UnitID
        cmd.Parameters.Add("@ResultsDate", SqlDbType.VarChar, 12)
        cmd.Parameters("@ResultsDate").Value = ResultsDate
        cmd.Parameters.Add("@Results", SqlDbType.VarChar, 5000)
        cmd.Parameters("@Results").Direction = ParameterDirection.Output
        cmd.Parameters("@Results").Value = Results

        Try
            cn.Open()
            cmd.ExecuteReader()
            Results = cmd.Parameters("@Results").Value
            cn.Close()

        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error reading Limits for Outfall=" + UnitID + e.Message)
        End Try
    End Sub
End Class
