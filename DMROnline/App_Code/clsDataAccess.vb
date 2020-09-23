Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Data

Public Class clsDataAccess
    Inherits System.Web.UI.Page
    Private connection As String = Session("ConnStr")

    Public Function GetWasteWaterSites(ByVal ActiveSitesOnly As String, ByVal OrderBySiteName As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblProgInterest_GetNPDES_Sites", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ActiveSitesOnly", SqlDbType.Char, 1)
        cmd.Parameters("@ActiveSitesOnly").Value = ActiveSitesOnly
        cmd.Parameters.Add("@OrderBySiteName", SqlDbType.Char, 1)
        cmd.Parameters("@OrderBySiteName").Value = OrderBySiteName
        Try
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return dr
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error getting list of Waste Water sites.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderPA1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPA1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Permit Facility Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderEA1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetEA1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Enforcement Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderPS1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPS1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Pipe Schedule Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderMV1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetMV1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 500
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Measurement violation Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderPL1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPL1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Limits Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderPE1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPE1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Permit Tracking Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderIN1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetIN1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Inspection Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderPF1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetPF1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Reissuance Data.  " & e.Message)
        End Try
    End Function

    Public Function GetDataReaderSV1(ByVal NPIDList As String, ByVal EPAFileName As String, ByVal MarkAsSubmitted As String) As SqlDataReader
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.spNPDES_GetSV1RSForEPA", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As New SqlParameter("@NPIDList", SqlDbType.VarChar, 7000)
        param.Value = NPIDList
        cmd.Parameters.Add(param)
        Dim param2 As New SqlParameter("@EPAFileName", SqlDbType.VarChar, 50)
        param2.Value = EPAFileName
        cmd.Parameters.Add(param2)
        Dim param3 As New SqlParameter("@MarkAsSubmitted", SqlDbType.Char, 1)
        param3.Value = MarkAsSubmitted
        cmd.Parameters.Add(param3)

        Try
            cn.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch e As Exception
            Throw New Exception("Error retrieving Violation Data for the following sites: " + NPIDList + ".  " & e.Message)
        End Try
    End Function

    Public Sub SaveArchiveFile(ByVal FileName As String, ByVal BatchNo As String, ByVal ReportType As String,
            ByVal EditType As String, ByVal EnvType As String,
            ByVal UserID As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("NPDESArchiveConnString"))
        Dim cmd As SqlCommand = New SqlCommand("dbo.spXML_Import", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim paramFileName As New SqlParameter("@FileName", SqlDbType.VarChar, 25)
        paramFileName.Value = Right(FileName, FileName.Length - FileName.LastIndexOf("\") - 1)
        cmd.Parameters.Add(paramFileName)
        Dim paramBatch As New SqlParameter("@BatchNo", SqlDbType.Char, 7)
        paramBatch.Value = BatchNo
        cmd.Parameters.Add(paramBatch)
        Dim paramReportType As New SqlParameter("@ReportType", SqlDbType.Char, 3)
        paramReportType.Value = ReportType
        cmd.Parameters.Add(paramReportType)
        Dim paramEditType As New SqlParameter("@EditType", SqlDbType.Char, 1)
        paramEditType.Value = EditType
        cmd.Parameters.Add(paramEditType)
        Dim paramEnvType As New SqlParameter("@EnvType", SqlDbType.Char, 1)
        paramEnvType.Value = EnvType
        cmd.Parameters.Add(paramEnvType)
        Dim paramUserID As New SqlParameter("@UserID", SqlDbType.VarChar, 64)
        paramUserID.Value = UserID
        cmd.Parameters.Add(paramUserID)
        'Get File to import into the database
        Dim paramFile As New SqlParameter("@File", SqlDbType.Image)
        Dim fs As New FileStream(FileName, FileMode.Open)
        Dim buffer(fs.Length) As Byte
        fs.Read(buffer, 0, buffer.Length)
        fs.Close()
        paramFile.Value = buffer
        cmd.Parameters.Add(paramFile)

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch e As Exception
            cn.Close()
            Throw New ApplicationException("SQL Server error saving XML file " + FileName + " to the DMZ NPDESArchive database: " + e.Message)
        End Try
    End Sub

    'Public Function GetArchiveFiles(ByVal IsProcessed As String) As SqlDataReader
    '    myConn.ConnectionString = ConfigurationSettings.AppSettings("NPDESArchiveConnString")
    '    Dim cmd As New SqlCommand("dbo.spXML_GetFiles")
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Connection = myConn
    '    Dim param As New SqlParameter("@IsProcessed", SqlDbType.Char, 1)
    '    param.Value = IsProcessed

    '    cmd.Parameters.Add(param)

    '    Try
    '        myConn.Open()
    '        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '    Catch e As Exception
    '        Throw New Exception("Error retrieving File Data from NPDESArchive database in the DMZ.  " & e.Message)
    '    End Try

    'End Function

    Public Sub SetBatchData(ByVal strBatch As String, ByVal strFile As String, ByVal strReport As String,
                            ByVal RecordCount As Int32, ByVal strUser As String)
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblNPDESBatch_Add", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim ParamBatch As New SqlClient.SqlParameter("@BatchNumber", SqlDbType.Char, 7)
        ParamBatch.Value = strBatch
        cmd.Parameters.Add(ParamBatch)
        Dim ParamFile As New SqlClient.SqlParameter("@FileName", SqlDbType.VarChar, 20)
        ParamFile.Value = strFile
        cmd.Parameters.Add(ParamFile)
        Dim ParamReport As New SqlClient.SqlParameter("@ReportType", SqlDbType.Char, 3)
        ParamReport.Value = strReport
        cmd.Parameters.Add(ParamReport)
        Dim ParamRecordCount As New SqlClient.SqlParameter("@RecordCount", SqlDbType.Int)
        ParamRecordCount.Value = RecordCount
        cmd.Parameters.Add(ParamRecordCount)
        Dim ParamUser As New SqlClient.SqlParameter("@UserID", SqlDbType.VarChar, 20)
        ParamUser.Value = strUser
        cmd.Parameters.Add(ParamUser)

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
        Catch e As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            Throw New Exception("Error Storing data about " + strReport + " Batch Number:" + strBatch + " in DEN.  " & e.Message)
        End Try

    End Sub

    Public Function GetNextBatchNumber(Optional ByRef ptrErrMsg As String = "") As String
        Dim cn As SqlConnection
        cn = New SqlConnection(connection)
        Dim cmd As SqlCommand = New SqlCommand("dbo.sptblNPDESBatch_GetNextBatchNumber", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@BatchNumber", SqlDbType.Char, 7)
        cmd.Parameters("@BatchNumber").Direction = ParameterDirection.Output

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            Return cmd.Parameters("@BatchNumber").Value
        Catch e As Exception
            Throw New Exception("Error retrieving Batch Number.  " & e.Message)
        End Try
    End Function
End Class



