<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" Inherits = "DMROnline.frmErrorPage" EnableSessionState="True" enableViewState="False" Codebehind="frmErrorPage.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
			<table width="100%" bgcolor="lemonchiffon">
				<tr>
					<td style="WIDTH: 2px"><asp:label id="lblEnv" runat="server" Font-Size="14pt" Font-Bold="True" Font-Italic="True"
							ForeColor="Red"></asp:label></td>
					<td align="right">&nbsp;Welcome Page&nbsp;&nbsp;<IMG style="CURSOR: hand" onclick='javascript:window.open("Welcome.aspx","_self")' height="10"
							src="images/go.gif" width="19"></td>
				</tr>
				<tr>
					<td colspan="2"><FONT size="6"><STRONG>&nbsp;<FONT size="5">Error Encountered in WasteWaterDE 
									on Page&nbsp;<%=request.params("Page")%></FONT></STRONG></FONT><b></b></td>
				</tr>
				<tr>
					<td colspan="2" style="FONT-STYLE: italic"><%=request.params("UserMessage")%></td>
				</tr>
				<tr>
					<td colspan="2"></td>
				</tr>
				<tr>
					<td colspan="2"><hr>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="FONT-WEIGHT: bold">The DEN support staff have been notified 
						of this error. If you would like to be notified about the resolution of this 
						error please contact Dennis Murphy.</td>
				</tr>
				<TR>
					<td colSpan="2" style="HEIGHT: 22px">Subject:
						<asp:TextBox id="txtSubject" runat="server" Width="432px"></asp:TextBox>
					</td>
				</TR>
				<TR>
					<TD style="FONT-WEIGHT: bold; HEIGHT: 13px" colSpan="2">
						<asp:TextBox id="txtBody" runat="server" Width="100%" TextMode="MultiLine" Rows="8"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-WEIGHT: bold" align="center" colSpan="2">
						<asp:Button id="btnSend" runat="server" Text="Send Mail"></asp:Button></TD>
				</TR>
			</table>
</asp:Content>