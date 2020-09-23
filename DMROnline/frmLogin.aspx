<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" Inherits = "DMROnline.frmLogin" EnableSessionState="True" enableViewState="False" Codebehind="frmLogin.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
            <div style="BORDER-RIGHT: #FFFFFF 2px solid; BORDER-TOP: #FFFFFF 2px solid; BORDER-LEFT: #FFFFFF 2px solid; BORDER-BOTTOM: #FFFFFF 2px solid; border-color: #000000;">
				<P align="center" style="width: 100%"><STRONG>
                    <span style="font-size: 76px; font-weight: bold;">e-DMR</span></STRONG></P>
				<P align="center" 
                    style="width: 100%; color: #800000; font-family: Arial, Helvetica, sans-serif; font-size: 12pt; font-weight: bold;">
                    ONLINE ELECTRONIC DISCHARGE MONITORING REPORT SUBMISSION</P>
                <P align="center" style="width: 100%">
                    <img 
                        alt="Water Logo" src="images/vectordnreclogotransparent.gif" 
                        style="width: 150px; height: 90px" align="middle" /></P>
				<P align="center" style="width: 100%; font-size: 24pt; color: #0066FF;">
                    Welcome
                    <P align="center" style="width: 100%">
                    <asp:Label ID="Label3" runat="server" ForeColor="#CC0000" 
                        Text="Your On-line Session has expired or is invalid. Please sign in again below."></asp:Label>
                     <P align="center" style="width: 100%">
                        <asp:Button ID="btnLogin" runat="server" BackColor="Red" BorderColor="#CC0000" 
                            Font-Bold="True" Font-Size="10pt" Text="Existing User Login" Width="200px" />
                      <P align="center" style="width: 100%">
                    <asp:Label 
                        ID="Label5" runat="server" 
                        Text="Use the above link to digitally sign any existing submissions" 
                        style="font-style: italic" Font-Size="10pt"></asp:Label>
                       <P align="center" style="width: 100%">
                          <asp:Button ID="btnNewUser" runat="server" BackColor="#006600" 
                              BorderColor="#336600" Font-Bold="False" Font-Size="10pt" Text="Create New User" 
                              Width="200px" ForeColor="#CCFF99" />
                </P>
				<P align="center" style="width: 100%">
                    <asp:Label 
                        ID="Label4" runat="server" 
                        Text="This site is designed to be viewed optimally at a screen resolution of 1024x768" 
                        style="font-style: italic" Font-Size="10pt"></asp:Label>
                </P>
				<P align="center" style="width: 100%">
                    &nbsp;</P>
				<P align="center" style="width: 100%">
                    &nbsp;</P>
				<P align="center" style="width: 100%"><FONT size="6"></FONT>
						<asp:RadioButtonList ID="rblProductionTest" runat="server" 
                        RepeatDirection="Horizontal" Visible="False">
		                <asp:ListItem Enabled="true" Text="Test" Value="2"></asp:ListItem>
		                <asp:ListItem Enabled="true" Text="Production" Value="1" Selected="true"></asp:ListItem>
		            </asp:RadioButtonList></P>

				<P align="center" style="width: 100%">&nbsp;</P>
			</div>
</asp:Content>