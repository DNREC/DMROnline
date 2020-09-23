Imports System.Data.SqlClient
Imports System.Text

Public Class clsBldAppMessages
    'Function BldAppMessages(ByVal AppID As Int16, ByRef AllowLogins As String) As String
    '    Dim myAppMsg As New clsDNRECApps
    '    Dim rdr As SqlDataReader = myAppMsg.GetAppMessages(AppID)
    '    AllowLogins = "Y"
    '    If rdr.HasRows Then
    '        Dim strbld As New StringBuilder(100)
    '        strbld.Append("<br/>")
    '        While rdr.Read()
    '            AllowLogins = rdr("AllowLogins")
    '            Select Case rdr("MsgClass")
    '                Case "Default"
    '                    strbld.Append("<FONT size='3' color='Black'><b>Status Normal: " + rdr("MsgHeader") + "</b><br/>" + rdr("Msg") + "</Font><br/><br/>")
    '                Case "Informational"
    '                    strbld.Append("<FONT size='3' color='Brown'><b>Informational Message: " + rdr("MsgHeader") + "</b><br/>" + rdr("Msg") + "</Font><br/><br/>")
    '                Case "Warning"
    '                    strbld.Append("<FONT size='4' color='Magenta'><b>Normal Functionality Of This Application Is Impared: " + rdr("MsgHeader") + "</b><br/>" + rdr("Msg") + "</Font><br/><br/>")
    '                Case "Critical"
    '                    strbld.Append("<FONT size='5' color='Orange'><b>This Application Will Be Going Down: " + rdr("MsgHeader") + "</b><br/>" + rdr("Msg") + "</Font><br/><br/>")
    '                Case "Fatal"
    '                    strbld.Append("<FONT size='6' color='Red'><b>This Application Is Unavailable: " + rdr("MsgHeader") + "</b><br/>" + rdr("Msg") + "</Font><br/><br/>")
    '                Case Else
    '                    strbld.Append("<FONT size='3' color='Brown'><b>" + rdr("MsgHeader") + "</b><br/>" + rdr("Msg") + "</Font><br/><br/>")
    '            End Select
    '        End While
    '        Return strbld.ToString
    '    Else
    '        Return "<br/>No messages.<br/>"
    '    End If
    'End Function
End Class
