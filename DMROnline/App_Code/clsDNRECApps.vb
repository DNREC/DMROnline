Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data

Public Class clsDNRECApps
    'Private connection As String = ConfigurationSettings.AppSettings.Item("DNRECAppsConnStr")

    'Public Function GetAppMessages(ByVal AppID As Int32) As SqlDataReader
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblMsg_GetCurrentMsgForAppID", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@AppID", SqlDbType.Int)
    '    cmd.Parameters("@AppID").Value = AppID
    '    Try
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    '        Return dr
    '    Catch e As Exception
    '        If cn.State = ConnectionState.Open Then cn.Close()
    '        Throw New Exception("Error getting App Status meassages.  " & e.ToString)
    '    End Try
    'End Function

    'Public Sub LogSessionEnd(ByVal AppID As Int32, ByVal SessionID As String)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblUsageLog_UpdateSessionEnd", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@AppID", SqlDbType.Int)
    '    cmd.Parameters("@AppID").Value = AppID
    '    cmd.Parameters.Add("@SessionID", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@SessionID").Value = SessionID
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State = ConnectionState.Open Then cn.Close()
    '        Throw New Exception("Error logging session end.  " & e.ToString)
    '    End Try
    'End Sub

    'Public Sub LogSessionStart(ByVal AppID As Int32, ByVal SessionID As String, ByVal AppUser As String)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblUsageLog_AddSessionStart", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@AppID", SqlDbType.Int)
    '    cmd.Parameters("@AppID").Value = AppID
    '    cmd.Parameters.Add("@SessionID", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@SessionID").Value = SessionID
    '    cmd.Parameters.Add("@AppUser", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@AppUser").Value = AppUser
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State = ConnectionState.Open Then cn.Close()
    '        Throw New Exception("Error logging session start.  " & e.ToString)
    '    End Try
    'End Sub

    'Public Sub LogAppEnd(ByVal AppID As Int32)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblAppLog_UpdateAppEnd", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@AppID", SqlDbType.Int)
    '    cmd.Parameters("@AppID").Value = AppID

    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State = ConnectionState.Open Then cn.Close()
    '        Throw New Exception("Error logging Application end.  " & e.ToString)
    '    End Try
    'End Sub

    'Public Sub LogAppStart(ByVal AppID As Int32)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblAppLog_AddAppStart", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@AppID", SqlDbType.Int)
    '    cmd.Parameters("@AppID").Value = AppID
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State = ConnectionState.Open Then cn.Close()
    '        Throw New Exception("Error logging Application start.  " & e.ToString)
    '    End Try
    'End Sub

    'Public Sub LogError(ByVal AppID As Int32, ByVal SessionID As String, _
    'ByVal AppUser As String, ByVal MachineName As String, _
    'ByVal CurrentExecutionFilePath As String, ByVal PhysicalPath As String, _
    'ByVal Path As String, ByVal PageName As String, ByVal BrowserType As String, _
    'ByVal Version As String, ByVal IsAOL As String, ByVal Exceptiontype As String, _
    'ByVal Source As String, ByVal FullName As String, ByVal Method As String, _
    'ByVal StackTrace As String, ByVal SqlProc As String, ByVal ErrorNumber As String, ByVal AbsoluteURI As String)
    '    Dim cn As SqlConnection
    '    cn = New SqlConnection(connection)
    '    Dim cmd As SqlCommand = New SqlCommand("dbo.sptblErrorLog_Add ", cn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@AppID", SqlDbType.SmallInt)
    '    cmd.Parameters("@AppID").Value = AppID
    '    cmd.Parameters.Add("@SessionID", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@SessionID").Value = SessionID
    '    cmd.Parameters.Add("@AppUser", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@AppUser").Value = AppUser
    '    cmd.Parameters.Add("@MachineName", SqlDbType.VarChar, 100)
    '    cmd.Parameters("@MachineName").Value = MachineName
    '    cmd.Parameters.Add("@CurrentExecutionFilePath", SqlDbType.VarChar, 500)
    '    cmd.Parameters("@CurrentExecutionFilePath").Value = CurrentExecutionFilePath
    '    cmd.Parameters.Add("@PhysicalPath", SqlDbType.VarChar, 500)
    '    cmd.Parameters("@PhysicalPath").Value = PhysicalPath
    '    cmd.Parameters.Add("@Path", SqlDbType.VarChar, 500)
    '    cmd.Parameters("@Path").Value = Path
    '    cmd.Parameters.Add("@PageName", SqlDbType.VarChar, 100)
    '    cmd.Parameters("@PageName").Value = PageName
    '    cmd.Parameters.Add("@BrowserType", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@BrowserType").Value = BrowserType
    '    cmd.Parameters.Add("@Version", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@Version").Value = Version
    '    cmd.Parameters.Add("@IsAOL", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@IsAOL").Value = IsAOL
    '    cmd.Parameters.Add("@ExceptionType", SqlDbType.VarChar, 200)
    '    cmd.Parameters("@ExceptionType").Value = Exceptiontype
    '    cmd.Parameters.Add("@Source", SqlDbType.VarChar, 200)
    '    cmd.Parameters("@Source").Value = Source
    '    cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 500)
    '    cmd.Parameters("@FullName").Value = FullName
    '    cmd.Parameters.Add("@Method", SqlDbType.VarChar, 100)
    '    cmd.Parameters("@Method").Value = Method
    '    cmd.Parameters.Add("@StackTrace", SqlDbType.VarChar, 6000)
    '    cmd.Parameters("@StackTrace").Value = StackTrace
    '    cmd.Parameters.Add("@SqlProc", SqlDbType.VarChar, 100)
    '    cmd.Parameters("@SqlProc").Value = SqlProc
    '    cmd.Parameters.Add("@ErrorNumber", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@ErrorNumber").Value = ErrorNumber
    '    cmd.Parameters.Add("@AbsoluteURI", SqlDbType.VarChar, 20)
    '    cmd.Parameters("@AbsoluteURI").Value = AbsoluteURI
    '    Try
    '        cn.Open()
    '        cmd.ExecuteNonQuery()
    '        cn.Close()
    '    Catch e As Exception
    '        If cn.State = ConnectionState.Open Then cn.Close()
    '        Throw New Exception("Error logging error message.  " & e.ToString)
    '    End Try
    'End Sub
End Class

