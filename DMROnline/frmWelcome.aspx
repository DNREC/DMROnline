<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" Inherits = "DMROnline.frmWelcome" EnableSessionState="True" enableViewState="False" Codebehind="frmWelcome.aspx.vb" %>
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
                        <asp:Button ID="btnLogin" runat="server" BackColor="Red" BorderColor="#CC0000" 
                            Font-Bold="True" Font-Size="10pt" Text="Existing User Login" Width="200px" />
                     <P align="center" style="width: 100%">
                    <asp:Label 
                        ID="Label5" runat="server" 
                        Text="User the above link to digitally sign any existing submissions" 
                        style="font-style: italic" Font-Size="10pt"></asp:Label>
                      <P align="center" style="width: 100%">
                          <asp:Button ID="btnNewUser" runat="server" BackColor="#006600" 
                              BorderColor="#336600" Font-Bold="False" Font-Size="10pt" Text="Create New User" 
                              Width="200px" ForeColor="#CCFF99" />
                       <P align="center" style="width: 100%">
                    <asp:Label 
                        ID="Label4" runat="server" 
                        Text="This site is designed to be viewed optimally at a screen resolution of 1280x1024" 
                        style="font-style: italic" Font-Size="10pt"></asp:Label>
                </P>
				<P align="center" style="width: 100%">&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                        ImageUrl="~/images/PDF.png" Width="26px" />
                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Italic="True" 
                        Font-Size="10pt" ForeColor="#003366">Click here to view Instructions</asp:LinkButton>
                </P>
				<P align="center" style="width: 100%">
                    <asp:Label ID="Label1" runat="server" 
                    Text="Concerning the Online Discharge Monitoring Report Submission" 
                    Width="100%" Font-Bold="True" Font-Size="Large"></asp:Label></P>
				<P align="center" style="width: 100%">
                    <asp:Label ID="Label2" runat="server" 
                    Text="In September 2005 EPA promulgated new regulations for online reporting known as Cross Media Electronic Reporting Rules (CROMERR). In accordance with these regulations States receiving electronic reports must comply with certain minimum requirements. States had to receive EPA’s approval for systems receiving electronic reports. Delaware’s system to receive reports online has been approved. Accordingly all reports received online henceforth must comply with the new system requirements. Therefore you need to establish a new user ID, password, set up answers to five knowledge based questions, and print, sign and mail the new agreement. You need to set up the new credentials and submit the new agreement before you can submit any online notification or annual reports. DNREC will expedite the approval of your agreement so that there is minimal disruption to your use of the online system. To conform to the new requirements, once you have filled out the online data form, verified its accuracy and submitted it electronically (if you are the authorized person to sign the document) you will be transferred to a new web page. Here you will be afforded the opportunity to review the document before you click the Sign button. You will be prompted for your password and for answer to one of the five questions you had set up while registering to comply with CROMERR." 
                    Width="100%" style="font-style: italic" Font-Size="10pt"></asp:Label></P>
				<P align="center" style="width: 100%"><FONT size="6"></FONT>
						<asp:RadioButtonList ID="rblProductionTest" runat="server" 
                        RepeatDirection="Horizontal" Visible="False">
		                <asp:ListItem Enabled="true" Text="Test" Value="2" ></asp:ListItem>
		                <asp:ListItem Enabled="true" Text="Production" Value="1" Selected="true" ></asp:ListItem>
		            </asp:RadioButtonList></P>

				<P align="center" style="width: 100%">&nbsp;</P>
			</div>
</asp:Content>