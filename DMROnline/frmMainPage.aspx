<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" Inherits = "DMROnline.frmMainPage" EnableSessionState="True" enableViewState="False" Codebehind="frmMainPage.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
            <div style="">
				<TABLE id="Table1" borderColor="Black" cellSpacing="0" cellPadding="0" 
                    width="100%" border="1">
					<tr>
						<td align="right" colspan="2">&nbsp;&nbsp;<asp:label id="lblAWM" runat="server" 
                                CssClass="Label" Font-Size="10pt" 
                                style="font-family: Arial, Helvetica, sans-serif">Logout</asp:label>&nbsp;
							<IMG style="CURSOR: hand; width: 19px;" 
                                onclick='javascript:window.open("frmWelcome.aspx","_self")' height="10"
								src="images/go.gif" width="19"></td>
						<tr>
						<td align="right" colspan="2"><asp:label id="lblEnv" runat="server" ForeColor="Red" Font-Italic="False" 
                                Font-Bold="True" Font-Size="10pt" 
                                style="font-family: Arial, Helvetica, sans-serif"></asp:label></td>
						<TR>
						<TD bgColor="Black" colSpan="2" 
                            style="font-family: Arial, Helvetica, sans-serif"><STRONG><FONT color="White" 
                                size="3">WasteWater 
									Discharge&nbsp;Sites</FONT></STRONG></TD>
					</TR>
					<tr>
						<td style="width: 14px">
							<STRONG style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Site</STRONG></td>
						<td>
							<asp:listbox id="lstWasteWaterSites" runat="server" 
                                Height="20px" AutoPostBack="True"
								DataValueField="PiID" DataTextField="Site" Font-Names="Arial" Font-Size="8pt" 
                                Font-Bold="True" ForeColor="#CCFF99" BackColor="#003300" 
                                style="margin-left: 0px"></asp:listbox></td>
					</tr>
					<TR>
						<TD colspan="2" style="width: 100%">
                            <STRONG style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Monitored Outfalls with Active Permit</STRONG></TD>
					</TR>
					<tr>
						<td width="95%" 
                            
                            style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; font-style: italic" 
                            colspan="2">(Select a Monitored Outfall to begin editing DMR)</td>
					</tr>
					<TR>
						<TD width="95%" vAlign="top" align="right" colspan="2"><asp:datagrid id="dgPermits" runat="server" Width="450px" 
                                BorderColor="black" BorderWidth="3px"
								AutoGenerateColumns="False" CellPadding="2" ItemStyle-CssClass="dgItemStyle" Font-Names="Arial" 
                                Font-Size="8pt">
								<HeaderStyle CssClass="dgHeaderStyle"></HeaderStyle>
								<AlternatingItemStyle CssClass="dgAlternatingItemStyle" />

<ItemStyle CssClass="dgItemStyle"></ItemStyle>
								<Columns>
									<asp:BoundColumn Visible="false" DataField="PermitID" ReadOnly="True" />
									<asp:TemplateColumn HeaderText="">
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
										<ItemTemplate>
											&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="true" DataField="PermitDateRange" 
                                        ItemStyle-Width="50px" HeaderText="Issued-Expired"
										ReadOnly="True" >
<ItemStyle Width="50px"></ItemStyle>
                                    </asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Status">
										<ItemTemplate>
											<asp:Label ID="lblPermitStatus" Visible=true runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PermitStatus") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outfalls" ItemStyle-Width="150px">
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:datagrid id="dgOutfalls" runat="server" Width="100%" 
                                                ItemStyle-CssClass="dgItemStyle" CellPadding="2"
												AutoGenerateColumns="False" BorderWidth="1px" BorderColor="Black" ShowHeader="False">
												<AlternatingItemStyle CssClass="dgAlternatingItemStyle" />
												<ItemStyle CssClass="dgItemStyle" />
												<Columns>
													<asp:BoundColumn Visible="false" DataField="UnitID" ReadOnly="True" />
													<asp:TemplateColumn>
														<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
														<ItemTemplate>
														    &nbsp;<input ID="Button2" 
                                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; color: #000000; background-color: #FFFF00; border: thin solid #808000" 
                                                                type="button" value="SELECT" onclick='location.href="frmDMREntry.aspx?Tkt=<%# httputility.urlencode(Request.QueryString("Tkt")) %>&ReportID=<%# Request.QueryString("ReportID") %>&PiID=<%# Request.QueryString("PiID") %>&UnitID=<%# DataBinder.Eval(Container.DataItem,"UnitID") %>&PermitID=<%# DataBinder.Eval(Container.DataItem,"PermitID") %>"'/>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn Visible="true" DataField="UnitName" ItemStyle-Font-Bold="True" 
                                                        ReadOnly="True" >
												        <ItemStyle Font-Bold="True" />
                                                    </asp:BoundColumn>
												</Columns>
											</asp:datagrid>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</div>
</asp:Content>
