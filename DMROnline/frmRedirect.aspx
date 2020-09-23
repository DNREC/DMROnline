<%@ Page Language="VB" MasterPageFile="~/CLFNested.master" AutoEventWireup="false" Inherits="DMROnline.frmRedirect" title="Redirect to CROMERR Page" Codebehind="frmRedirect.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
<p class="FormMessage" 
        style="font-size:20pt; font-weight:bold; color:Red; text-align:center; width:100%; font-family: Arial;">
Your submission has been received.
<br />
<br />
    <span style="font-size:16;">You need to go to DNREC Online Reporting System to sign your submission.</span>
</p>
<br />
<br />
<p style="width:100%; color:Gray; text-align:center; font-family: Arial;">
The page will automatically redirect to DNREC Online Reporting System in 5 seconds.
<br />
<br />
To go to DNREC Online Reporting System now, please click <asp:LinkButton ID="btnCromErr" runat="server" Text="Go" CssClass="ButtonClass" />
</p>
<script type="text/javascript" language="javascript">
    function RedirectPage()
    {
        window.location = '<%=Session("NewPath") %>';
    }
    
    setTimeout('RedirectPage()', 5000)
</script>
</asp:Content>
