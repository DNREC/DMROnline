﻿<%@ WebHandler Language="VB" Class="Handler" %>

Imports DMZReports
Imports System.Net
Imports System.Net.Mail
Imports System.Web
Imports System.Web.Services

Public Class Handler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        ProcessReportingServerReport(context)
    End Sub
    
    Sub ProcessReportingServerReport(ByVal context As HttpContext)
        Dim reportFormat As String = context.Request.QueryString("reportFormat")
        Dim request = context.Request
        Dim response = context.Response
        context.Response.ContentType = "application/pdf"

        Dim service As New ReportExecutionService()
        service.Url = ConfigurationManager.AppSettings("reportserverpath")
        service.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("reportuser"), ConfigurationManager.AppSettings("reportpassword"), ConfigurationManager.AppSettings("reportdomain"))

        Dim parameters(2) As ParameterValue
        For i As Integer = 0 To parameters.Length - 1
            parameters(i) = New ParameterValue()
        Next
        parameters(0).Name = "ActID"
        parameters(0).Value = request.QueryString("ActID")
        parameters(1).Name = "StartDate"
        parameters(1).Value = request.QueryString("monperiod")
        parameters(2).Name = "PermitID"
        parameters(2).Value = request.QueryString("PermitID")

        service.LoadReport(ConfigurationManager.AppSettings("DraftReport"), Nothing)
        service.SetExecutionParameters(parameters, Nothing)

        Dim bytes() As Byte = _
        service.Render("PDF", Nothing, "pdf", Nothing, Nothing, Nothing, Nothing)
        response.AddHeader("content-disposition", String.Format("inline; filename=""{0}""", "Report.pdf"))

        response.BinaryWrite(bytes)
        response.End()

    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class