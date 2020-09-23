<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" Inherits="DMROnline.frmDMREntry" Async="true" MaintainScrollPositionOnPostback="true" Codebehind="frmDMREntry.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div title="Metadata"
        align="left">
        <table id="Table1" bordercolor="Black" cellspacing="0" cellpadding="0"
            width="100%" border="0">
            <tr valign="top" height="30">
                <td>
                    <asp:Label ID="lblEnv" runat="server" ForeColor="Red" Font-Italic="True"
                        Font-Bold="True" Font-Size="14pt"
                        Style="font-size: 8pt; font-family: Arial, Helvetica, sans-serif"></asp:Label></td>
                <td></td>
                <td align="right" colspan="6">&nbsp; &nbsp;&nbsp;&nbsp;
							&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr style="font-weight: bold; font-size: 8pt" bgcolor="#339999">
                <td colspan="8" bgcolor="Black">&nbsp;</td>
            </tr>
            <tr style="font-weight: bold; font-size: large; color: #ffffcc" bgcolor="#339999">
                <td colspan="3" bgcolor="Black"
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #FFFFFF;"
                    bordercolor="Black">Enter&nbsp;Discharge Monitoring Data</td>
                <td bgcolor="Black">
                    <asp:Label
                        ID="Label10" runat="server"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">Go To Outfall:</asp:Label>
                </td>
                <td align="right" bgcolor="Black" style="text-align: center">
                    <asp:Button ID="btnStore" TabIndex="-1" runat="server" Font-Bold="True"
                        Width="75px" Text="Save"
                        ToolTip="Save current report data"
                        OnClientClick="return confirm('You are about to save any current changes you have made to the monitoring data for later completion.');"
                        ForeColor="White" BackColor="#000099"></asp:Button>
                </td>
                <td bgcolor="Black" style="text-align: center">
                    <asp:Button ID="btnSubmit" TabIndex="-1" runat="server"
                        Font-Bold="True"  Text="Submit"
                        ToolTip="Submit Report for Digitally Signing"
                        OnClientClick="return confirm('Are you sure you want to Submit this Discharge Monitoring Report? You will not be able to further edit this data.');"
                        ForeColor="White" BackColor="#000099"></asp:Button>
                    <asp:Button ID="btnReverse" TabIndex="-1" runat="server"
                        Font-Bold="True" Text="Reverse"
                        ToolTip="Reverse Submitted Report"
                        OnClientClick="return confirm('Are you sure you want to Reverse this Discharge Monitoring Report Submission? Once done, you will have to resubmit the data.');"
                        ForeColor="White" BackColor="#000099" Visible="False"></asp:Button>
                </td>
                <td bgcolor="Black" style="text-align: center">
                    <asp:Button ID="btnReturn" TabIndex="-1" runat="server"
                        Text="Return"
                        ToolTip="Returns to the previous page without saving any changes"
                        OnClientClick="return confirm('Are you sure you want to abandom this page without saving any changes you may have made?');"
                        Font-Bold="True" ForeColor="White" BackColor="#000099" Width="75px"></asp:Button>
                </td>
                <td bgcolor="Black" style="text-align: center">
                    <asp:Button ID="btnPrint"
                        TabIndex="-1" runat="server" Text="Print"
                        ToolTip="Create a Draft Copy of the DMR to PDF"
                        ForeColor="White" BackColor="#000099" Style="font-weight: 700"
                        Width="75px"></asp:Button>
                </td>
            </tr>
            <tr style="font-weight: bold; font-size: medium; color: #ffffcc" bgcolor="#339999">
                <td bgcolor="Black" colspan="3" bordercolor="Black" style="color: #FFFFFF">
                    <asp:Label ID="lblSiteOutfall" runat="server"
                        Style="font-size: medium; font-family: Arial, Helvetica, sans-serif"></asp:Label></td>
                <td bgcolor="Black">
                    <asp:DropDownList ID="ddlOutfall" runat="server" AutoPostBack="True"
                        DataTextField="UnitName" DataValueField="UnitID"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; color:black">
                    </asp:DropDownList>
                </td>
                <td align="right" bgcolor="Black" style="text-align: center">
                    <asp:Label ID="Label15" runat="server"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; text-align: center"
                        Text="Save For Later Editing"></asp:Label>
                </td>
                <td bgcolor="Black" style="text-align: center">
                    <asp:Label ID="Label16" runat="server"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                        Text="Submit To DNREC"></asp:Label>
                    <asp:Label ID="lblReverse" runat="server"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                        Text="Reverse Submission" Visible="False"></asp:Label>
                </td>
                <td bgcolor="Black" style="text-align: center">
                    <asp:Label ID="Label17" runat="server"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                        Text="Return To Monitored Outfalls"></asp:Label>
                </td>
                <td bgcolor="Black" style="text-align: center">
                    <asp:Label ID="Label18" runat="server"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                        Text="Print Draft"></asp:Label>
                </td>
            </tr>
            <tr style="font-weight: bold; font-size: 8pt" bgcolor="#339999">
                <td colspan="8" bgcolor="Black"></td>
            </tr>
            <tr style="font-weight: bold">
                <td colspan="8">
                    <table width="100%">
                        <tr>
                            <td style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 180px;">DMR Reporting Period:</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtStartDate" Width="0px" runat="server"
                                    Visible="false"></asp:TextBox><asp:TextBox ID="txtEndDate" Width="1px" runat="server" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlYrMonth" TabIndex="-1" runat="server" Width="88px"
                                    AutoPostBack="True" DataValueField="YrMon"
                                    DataTextField="MonthYear" Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                </asp:DropDownList>
                                <td style="font-weight: bold">
                                    <asp:CheckBox ID="chkShowMetadata" TabIndex="-1" runat="server" Text="Metadata" ToolTip="Display months required, sample type and sample frequency"
                                        AutoPostBack="True" TextAlign="Left" Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                                        Visible="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;<asp:CheckBox
                                            ID="chkShowLimits" TabIndex="-1" runat="server" Text="Limits" ToolTip="Display discharge limit under related textbox"
                                            AutoPostBack="True" TextAlign="Left" Checked="True"
                                            Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                                            Visible="False"></asp:CheckBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 180px;">Date Submitted to DNREC:</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDateReceived" runat="server" Width="88px" Enabled="False"
                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                            <td style="font-weight: bold">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 180px;">
                                <acronym title='<%="NDI (No Data Indicator) Reasons:" & VbCrlf & 
"1 - Wrong Flow" & vbCrLf &
"2 - Operation Shutdown" & vbCrLf &
"4 - Discharge to Lagoon" & vbCrLf &
"5 - Frozen Conditions" & vbCrLf &
"8 - No Sample (Other)" & vbCrLf &
"9 - Monitoring Not Required this Period" & vbCrLf &
"A - General Permit Exemption" & vbCrLf &
"B - Not Detected" & vbCrLf &
"C - No Discharge" & vbCrLf &
"D - Lost Sample" & vbCrLf &
"E - Analysis Not Conducted" & vbCrLf &
"F - Insufficient Flow for Sampling" & vbCrLf &
"G - Sampling Equipment Failure" & vbCrLf &
"H - Invalid Test" & vbCrLf &
"I - Land Applied" & vbCrLf &
"J - Recycled - Water-Closed System" & vbCrLf &
"K - Flood Disaster" & vbCrLf &
"Q - Not Quantifiable" & vbCrLf &
"V - Weather Related"%>'>
                                    <asp:Label ID="Label11" runat="server" Font-Italic="True"
                                        Text="No Data Reason (NDI): "
                                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Label></acronym>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDMRNoDischargeReason" runat="server"
                                    ForeColor="#cc0000" Font-Bold="True"
                                    AutoPostBack="True" DataValueField="NoDataIndicatorCode" DataTextField="NoDataIndicator" BackColor="White"
                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Gray" Text="Select to auto-fill all parameters on this page."></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 180px;">Comments 
                                        Regarding this DMR:</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtComments" runat="server" Width="95%"
                                    MaxLength="1000" TextMode="MultiLine"
                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 180px;">&nbsp;<asp:Label
                                ID="Label12" runat="server" Text="Reason Rejected:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtReasonRejected"
                                    runat="server" Width="95%"
                                    Enabled="False" MaxLength="1000" TextMode="MultiLine"
                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 8pt; width: 180px;">System Messages:</td>
                            <td colspan="3">
                                <asp:Label
                                    ID="lblUpload" runat="server" ForeColor="Red"
                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: small"></asp:Label>
                                &nbsp;<asp:Label
                                    ID="lblStatus" runat="server" ForeColor="#000066"
                                    Font-Bold="True" Style="font-family: Arial, Helvetica, sans-serif; font-size: small"></asp:Label>
                                &nbsp;<asp:Label ID="lblSystemMessages" runat="server" ForeColor="#003300"
                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                        Width="100%" BackColor="MistyRose" BorderStyle="Inset"
                        HeaderText="PLEASE CORRECT THE PROBLEMS LISTED BELOW AND RE-SAVE" BorderColor="Red"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:ValidationSummary>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8">&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td align="left" colspan="8">
                    <asp:Button ID="btnPg1" TabIndex="-1" runat="server"
                        ForeColor="Tan" Font-Bold="True" Font-Size="8pt"
                        Text="Pg 1" ToolTip="Display Page 1 of the DMR form." BorderStyle="Ridge" CausesValidation="False"
                        Style="width: 36px; font-family: Arial, Helvetica, sans-serif; font-size: 8pt;"></asp:Button>
                    <asp:Button ID="btnPg2" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 2" ToolTip="Display Page 2 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Button>
                    <asp:Button ID="btnPg3" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 3" ToolTip="Display Page 3 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Button>
                    <asp:Button ID="btnPg4" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 4" ToolTip="Display Page 4 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Button>
                    <asp:Button ID="btnPg5" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 5" ToolTip="Display Page 5 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Button>
                    <asp:Button ID="btnPg6" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 6" ToolTip="Display Page 6 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Button>
                    <asp:Button ID="btnPg7" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 7" ToolTip="Display Page 7 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:Button>
                    <asp:Button ID="btnPg8" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 8" ToolTip="Display Page 8 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif"></asp:Button>
                    <asp:Button ID="btnPg9" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 9" ToolTip="Display Page 9 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif"></asp:Button>
                    <asp:Button ID="btnPg10" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 10" ToolTip="Display Page 10 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif"></asp:Button>
                    <asp:Button ID="btnPg11" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 11" ToolTip="Display Page 11 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif"></asp:Button>
                    <asp:Button ID="btnPg12" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 12" ToolTip="Display Page 12 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif"></asp:Button>
                    <asp:Button ID="btnPg13" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 13" ToolTip="Display Page 13 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif"></asp:Button>
                    <asp:Button ID="btnPg14" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Pg 14" ToolTip="Display Page 14 of the DMR form." CausesValidation="False"
                        Style="font-family: Arial, Helvetica, sans-serif"></asp:Button>
                    <asp:Button ID="btnPg99" TabIndex="-1" runat="server" ForeColor="Tan"
                        Font-Bold="True" Font-Size="8pt"
                        Text="Unassigned" ToolTip="Display parameters that have been assigned to a DMR page."
                        CausesValidation="False" Style="font-family: Arial, Helvetica, sans-serif"></asp:Button></td>
            </tr>
            <tr>
                <td width="100%" colspan="8">
                    <asp:DataGrid ID="dgDMR" runat="server" Width="100%" ItemStyle-CssClass="dgItemStyle"
                        CellPadding="3" AutoGenerateColumns="False"
                        OnItemDataBound="dgDMR_ItemDataBound"
                        Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                        ForeColor="Black" GridLines="Vertical" BackColor="White"
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" DataKeyField="">
                        <FooterStyle BackColor="#CCCCCC" />
                        <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <AlternatingItemStyle CssClass="dgAlternatingItemStyle" BackColor="#CCCCCC"></AlternatingItemStyle>
                        <ItemStyle CssClass="dgItemStyle"></ItemStyle>
                        <HeaderStyle Font-Bold="True" Height="10px" BackColor="Black"
                            BorderColor="Black" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn Visible="False">
                                <HeaderStyle Width="5%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Pg
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblPage" Font-Size="8pt" Visible="true" runat="server" Font-Bold="True" Text='<%# DataBinder.Eval(Container, "DataItem.PageOrderDisplay") %>'>
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="10%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Parameter
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblParameter" Font-Size="8pt" Visible="true" runat="server" Font-Bold="True" Text='<%# DataBinder.Eval(Container, "DataItem.Parameter") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLimitGroup2" Font-Size="8pt" Visible="true" runat="server" ForeColor="Maroon" Font-Bold="True" Text='<%# DataBinder.Eval(Container, "DataItem.LimitGroup") %>'>
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMonLoc" Font-Size="8pt" Visible="true" runat="server" Font-Bold="True" Text='<%# DataBinder.Eval(Container, "DataItem.MonLoc") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLimitMonths" Font-Size="8pt" Visible="true" runat="server" Font-Bold="True" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMonths") %>'>
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    NDI
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblNDI" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoDataIndicator") %>'>
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlNDI" runat="server" ForeColor="#cc0000"
                                                    Font-Bold="True" DataTextField="NoDataIndicatorCode"
                                                    DataValueField="NoDataIndicatorCode"
                                                    ToolTip='<%#"NDI (No Data Indicator) Reasons:" & vbCrLf &
            "1 - Wrong Flow" & vbCrLf &
            "2 - Operation Shutdown" & vbCrLf &
            "4 - Discharge to Lagoon" & vbCrLf &
            "5 - Frozen Conditions" & vbCrLf &
            "8 - No Sample (Other)" & vbCrLf &
            "9 - Monitoring Not Required this Period" & vbCrLf &
            "A - General Permit Exemption" & vbCrLf &
            "B - Not Detected" & vbCrLf &
            "C - No Discharge" & vbCrLf &
            "D - Lost Sample" & vbCrLf &
            "E - Analysis Not Conducted" & vbCrLf &
            "F - Insufficient Flow for Sampling" & vbCrLf &
            "G - Sampling Equipment Failure" & vbCrLf &
            "H - Invalid Test" & vbCrLf &
            "I - Land Applied" & vbCrLf &
            "J - Recycled - Water-Closed System" & vbCrLf &
            "K - Flood Disaster" & vbCrLf &
            "Q - Not Quantifiable" & vbCrLf &
            "V - Weather Related"%>'
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="11%" Font-Bold="True" Font-Size="8pt" HorizontalAlign="Left"></HeaderStyle>
                                <HeaderTemplate>
                                    Average Quantity
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblAvgQuantActResultsID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvgQuantActResultsID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblAvgQuantLimitID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvgQuantLimitID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblAvgQVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvgQuantVMC") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLimitAvgQVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitAvgQuantVMC") %>'>
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlAvgQVMC" runat="server" Font-Size="8pt"
                                                    BackColor="White" TabIndex="-1"
                                                    Style="font-family: Arial, Helvetica, sans-serif">
                                                    <asp:ListItem Value=" "></asp:ListItem>
                                                    <asp:ListItem Value="&lt;">&lt;</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtAvgQ" runat="server" Width="70px" BackColor="White"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.AvgQuant") %>'
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"
                                                    OnTextChanged="txtAvgQ_TextChanged"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revAvgQ" runat="server" ErrorMessage="Average quantity must be either blank or a valid number."
                                                    ControlToValidate="txtAvgQ" ValidationExpression="[-]?[0-9]*\.?[0-9]*">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAvgQuantLimit" Font-Size="8pt" ForeColor="Navy" Font-Italic="True" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitAvgQuantDisplay") %>'>
                                                </asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="11%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Maximum Quantity
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblMaxQuantActResultsID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxQuantActResultsID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblMaxQuantLimitID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxQuantLimitID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblMaxQVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxQuantVMC") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLimitMaxQVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMaxQuantVMC") %>'>
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlMaxQVMC" runat="server" Font-Size="8pt"
                                                    BackColor="White" TabIndex="-1"
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                                    <asp:ListItem Value=" "></asp:ListItem>
                                                    <asp:ListItem Value="&lt;">&lt;</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtMaxQ" runat="server" Width="70px" BackColor="White"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.MaxQuant") %>'
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revMaxQ" runat="server" ErrorMessage="Maximum quantity must be either blank or a valid number."
                                                    ControlToValidate="txtMaxQ" ValidationExpression="[-]?[0-9]*\.?[0-9]*">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMaxQuantLimit" Font-Size="8pt" ForeColor="Navy" Font-Italic="True" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMaxQuantDisplay") %>'>
                                                </asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="11%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Minimum Concentration
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblMinConcActResultsID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MinConcActResultsID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblMinConcLimitID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MinConcLimitID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblMinCVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MinConcVMC") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLimitMinCVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMinConcVMC") %>'>
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlMinCVMC" runat="server"
                                                    BackColor="White" Font-Size="8pt"
                                                    TabIndex="-1" Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                                    <asp:ListItem Value=" "></asp:ListItem>
                                                    <asp:ListItem Value="&lt;">&lt;</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtMinC" runat="server" Width="70px"
                                                    BackColor="White"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.MinConc") %>'
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revMinC" runat="server" ErrorMessage="Minimum Concentration must be either blank or a valid number."
                                                    ControlToValidate="txtMinC" ValidationExpression="[-]?[0-9]*\.?[0-9]*">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMinConcLimit" Font-Size="8pt" ForeColor="Navy" Font-Italic="True" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMinConcDisplay") %>'>
                                                </asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="11%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Average Concentration
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblAvgConcActResultsID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvgConcActResultsID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblAvgConcLimitID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvgConcLimitID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblAvgCVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvgConcVMC") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLimitAvgCVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitAvgConcVMC") %>'>
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlAvgCVMC" runat="server" Font-Size="8pt"
                                                    BackColor="White" TabIndex="-1"
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                                    <asp:ListItem Value=" "></asp:ListItem>
                                                    <asp:ListItem Value="&lt;">&lt;</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtAvgC" runat="server" Width="70px" BackColor="White"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.AvgConc") %>'
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revAvgC" runat="server" ErrorMessage="Average Concentration must be either blank or a valid number."
                                                    ControlToValidate="txtAvgC" ValidationExpression="[-]?[0-9]*\.?[0-9]*">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAvgConcLimit" Font-Size="8pt" ForeColor="Navy" Font-Italic="True" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitAvgConcDisplay") %>'>
                                                </asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="11%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Maximum Concentration
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblMaxConcActResultsID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxConcActResultsID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblMaxConcLimitID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxConcLimitID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblMaxCVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxConcVMC") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLimitMaxCVMC" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMaxConcVMC") %>'>
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlMaxCVMC" runat="server" Font-Size="8pt"
                                                    BackColor="White" TabIndex="-1"
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                                    <asp:ListItem Value=" "></asp:ListItem>
                                                    <asp:ListItem Value="&lt;">&lt;</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtMaxC" runat="server" Width="70px" BackColor="White"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.MaxConc") %>'
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revMaxC" runat="server" ErrorMessage="Maximum Concentration must be either blank or a valid number."
                                                    ControlToValidate="txtMaxC" ValidationExpression="[-]?[0-9]*\.?[0-9]*">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMaxConcLimit" Font-Size="8pt" ForeColor="Navy" Font-Italic="True" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMaxConcDisplay") %>'>
                                                </asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Exc
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:TextBox ID="txtNumberOfExcursions" runat="server" Width="20px"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.NumberOfExcursions") %>'
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="11%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Frequency
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblSampleFreqID" runat="server"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.SampleFreqID") %>'
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblLimitSampleFreqID" Visible="False" runat="server"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.LimitSampleFreqID") %>'></asp:Label>
                                                <asp:DropDownList ID="ddlSampleFreq" runat="server" Font-Size="6pt"
                                                    BackColor="White" TabIndex="-1"
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 6pt"
                                                    SelectedValue='<%# DataBinder.Eval(Container, "DataItem.SampleFreqID") %>'>
                                                    <asp:ListItem Value=" "></asp:ListItem>
                                                    <asp:ListItem Value="1">N/A - Not Applicable</asp:ListItem>
                                                    <asp:ListItem Value="2">N/R - Not Reported</asp:ListItem>
                                                    <asp:ListItem Value="3">N/V - Not Valid</asp:ListItem>
                                                    <asp:ListItem Value="4">CL/OC - Chlorination/Occurs</asp:ListItem>
                                                    <asp:ListItem Value="5">DL/DS - Daily When Discharge</asp:ListItem>
                                                    <asp:ListItem Value="6">WH/DS - When Discharge</asp:ListItem>
                                                    <asp:ListItem Value="7">WH/MN - Measured When Monitored</asp:ListItem>
                                                    <asp:ListItem Value="8">01/BA - Once/Batch</asp:ListItem>
                                                    <asp:ListItem Value="9">01/DD - Once/Discharge Day</asp:ListItem>
                                                    <asp:ListItem Value="10">01/DM - Once/Discharge Month</asp:ListItem>
                                                    <asp:ListItem Value="11">01/DQ - Once/Discharge Quarter</asp:ListItem>
                                                    <asp:ListItem Value="12">01/DS - Once/Discharge</asp:ListItem>
                                                    <asp:ListItem Value="13">01/DW - Once/Discharge Week</asp:ListItem>
                                                    <asp:ListItem Value="14">01/RN - Once/Rain Event</asp:ListItem>
                                                    <asp:ListItem Value="15">01/SH - Once/Shift</asp:ListItem>
                                                    <asp:ListItem Value="16">01/YR - Annual</asp:ListItem>
                                                    <asp:ListItem Value="17">01/01 - Daily</asp:ListItem>
                                                    <asp:ListItem Value="18">01/02 - Once/2 Days</asp:ListItem>
                                                    <asp:ListItem Value="19">01/03 - Once/3 Days</asp:ListItem>
                                                    <asp:ListItem Value="20">01/04 - Once/4 Days</asp:ListItem>
                                                    <asp:ListItem Value="21">01/05 - Once/5 Days</asp:ListItem>
                                                    <asp:ListItem Value="22">01/06 - Once/6 Days</asp:ListItem>
                                                    <asp:ListItem Value="23">01/07 - Weekly</asp:ListItem>
                                                    <asp:ListItem Value="24">01/08 - Once/8 Days</asp:ListItem>
                                                    <asp:ListItem Value="25">01/09 - Once/9 Days</asp:ListItem>
                                                    <asp:ListItem Value="26">01/10 - Once/10 Days</asp:ListItem>
                                                    <asp:ListItem Value="27">01/11 - Once/11 Days</asp:ListItem>
                                                    <asp:ListItem Value="28">01/12 - Once/12 Days</asp:ListItem>
                                                    <asp:ListItem Value="29">01/13 - Once/13 Days</asp:ListItem>
                                                    <asp:ListItem Value="30">01/14 - Once/2 Weeks</asp:ListItem>
                                                    <asp:ListItem Value="31">01/21 - Once/3 Weeks</asp:ListItem>
                                                    <asp:ListItem Value="32">01/30 - Once/Month</asp:ListItem>
                                                    <asp:ListItem Value="33">01/28 - Once/4 Weeks</asp:ListItem>
                                                    <asp:ListItem Value="34">01/60 - Once/2 Months</asp:ListItem>
                                                    <asp:ListItem Value="35">01/90 - Quarterly</asp:ListItem>
                                                    <asp:ListItem Value="36">01/99 - Instant</asp:ListItem>
                                                    <asp:ListItem Value="37">02/BA - Twice/Batch</asp:ListItem>
                                                    <asp:ListItem Value="38">02/DS - Twice/Discharge</asp:ListItem>
                                                    <asp:ListItem Value="39">02/DW - Twice/Discharge Week</asp:ListItem>
                                                    <asp:ListItem Value="40">02/SH - Twice/Shift</asp:ListItem>
                                                    <asp:ListItem Value="41">02/YR - Semi-Annual</asp:ListItem>
                                                    <asp:ListItem Value="42">02/01 - Twice/Day</asp:ListItem>
                                                    <asp:ListItem Value="43">02/07 - Twice/Week</asp:ListItem>
                                                    <asp:ListItem Value="44">02/12 - Twice/12 Days</asp:ListItem>
                                                    <asp:ListItem Value="45">02/30 - Twice/Month</asp:ListItem>
                                                    <asp:ListItem Value="46">02/90 - Twice/Quarter</asp:ListItem>
                                                    <asp:ListItem Value="47">02/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="48">03/BA - 3 Times/Batch</asp:ListItem>
                                                    <asp:ListItem Value="49">03/DS - 3 Times/Discharge</asp:ListItem>
                                                    <asp:ListItem Value="50">03/YR - 3 Times/Year</asp:ListItem>
                                                    <asp:ListItem Value="51">03/01 - 3 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="52">03/05 - 3 Times/5 Days</asp:ListItem>
                                                    <asp:ListItem Value="53">03/07 - 3 Times/Week </asp:ListItem>
                                                    <asp:ListItem Value="54">03/08 - 3 Times/8 Days</asp:ListItem>
                                                    <asp:ListItem Value="55">03/30 - 3 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="56">03/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="57">04/BA - 4 Times/Batch</asp:ListItem>
                                                    <asp:ListItem Value="58">04/01 - 4 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="59">04/07 - 4 Times/Week</asp:ListItem>
                                                    <asp:ListItem Value="60">04/30 - 4 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="61">04/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="62">05/BA - 5 Times/Batch</asp:ListItem>
                                                    <asp:ListItem Value="63">05/WK - 5 Times/Week</asp:ListItem>
                                                    <asp:ListItem Value="64">05/01 - 5 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="65">05/07 - Week-Days</asp:ListItem>
                                                    <asp:ListItem Value="66">05/08 - 5 Times/8 Days</asp:ListItem>
                                                    <asp:ListItem Value="67">05/30 - 5 Times/ Month</asp:ListItem>
                                                    <asp:ListItem Value="68">05/90 - 5 Times/Quarterly</asp:ListItem>
                                                    <asp:ListItem Value="69">05/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="70">06/SH - 6 Times/Operating Shift</asp:ListItem>
                                                    <asp:ListItem Value="71">06/01 - 6 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="72">06/07 - 6 Times/Week</asp:ListItem>
                                                    <asp:ListItem Value="73">06/30 - 6 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="74">06/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="75">07/30 - 7 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="76">07/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="77">08/BA - 8 Times/Batch</asp:ListItem>
                                                    <asp:ListItem Value="78">08/01 - 8 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="79">08/30 - 8 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="80">08/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="81">09/01 - 9 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="82">09/30 - 9 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="83">09/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="84">10/30 - 10 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="85">10/99 - See Permit</asp:ListItem>
                                                    <asp:ListItem Value="86">12/01 - 12 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="87">12/30 - 12 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="88">15/30 - 15 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="89">16/01 - 16 Times/Day</asp:ListItem>
                                                    <asp:ListItem Value="90">18/30 - 18 Times/Month</asp:ListItem>
                                                    <asp:ListItem Value="91">24/01 - Hourly</asp:ListItem>
                                                    <asp:ListItem Value="92">48/01 - Every 1/2 Hour</asp:ListItem>
                                                    <asp:ListItem Value="93">66/66 - Wpc Plan</asp:ListItem>
                                                    <asp:ListItem Value="94">77/77 - Counting Gent</asp:ListItem>
                                                    <asp:ListItem Value="95">88/88 - Cleaning</asp:ListItem>
                                                    <asp:ListItem Value="96">99/99 - Continuous</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSampleFreqLimit" Font-Size="8pt" ForeColor="Navy"
                                                    Font-Italic="True" runat="server"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.LimitSampleFrequencyDisplay") %>'></asp:Label>
                                                <asp:Label ID="Label6" runat="server" Font-Size="8pt"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.SampleFrequencyDisplay") %>'
                                                    Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="11%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Sample Type
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblSampleTypeCode" runat="server"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.SampleTypeCode") %>'
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblLimitSampleTypeCode" Visible="False" runat="server"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.LimitSampleTypeCode") %>'></asp:Label>
                                                <asp:DropDownList ID="ddlSampleType" runat="server" Font-Size="6pt"
                                                    BackColor="White" TabIndex="-1"
                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: 6pt"
                                                    SelectedValue='<%# DataBinder.Eval(Container, "DataItem.SampleTypeCode") %>'>
                                                    <asp:ListItem Value=" "></asp:ListItem>
                                                    <asp:ListItem Value="TB">TB - Trip Blank</asp:ListItem>
                                                    <asp:ListItem Value="CA">CA - Calculated</asp:ListItem>
                                                    <asp:ListItem Value="CN">CN - Counting</asp:ListItem>
                                                    <asp:ListItem Value="CP">CP - Composition</asp:ListItem>
                                                    <asp:ListItem Value="CR">CR - Check Required</asp:ListItem>
                                                    <asp:ListItem Value="CS">CS - CORSAM</asp:ListItem>
                                                    <asp:ListItem Value="CU">CU - Curve</asp:ListItem>
                                                    <asp:ListItem Value="DA">DA - Daily Average</asp:ListItem>
                                                    <asp:ListItem Value="DS">DS - Discrete</asp:ListItem>
                                                    <asp:ListItem Value="ES">ES - Estimate</asp:ListItem>
                                                    <asp:ListItem Value="FI">FI - FLOIND</asp:ListItem>
                                                    <asp:ListItem Value="GH">GH - 5GR24H</asp:ListItem>
                                                    <asp:ListItem Value="GM">GM - Grab-10</asp:ListItem>
                                                    <asp:ListItem Value="GR">GR - Grab</asp:ListItem>
                                                    <asp:ListItem Value="G2">G2 - Grab-2</asp:ListItem>
                                                    <asp:ListItem Value="G3">G3 - Grab-3</asp:ListItem>
                                                    <asp:ListItem Value="G4">G4 - Grab-4</asp:ListItem>
                                                    <asp:ListItem Value="G5">G5 - Grab-5</asp:ListItem>
                                                    <asp:ListItem Value="G6">G6 - Grab-6</asp:ListItem>
                                                    <asp:ListItem Value="G7">G7 - Grab-7</asp:ListItem>
                                                    <asp:ListItem Value="G8">G8 - Grab-8</asp:ListItem>
                                                    <asp:ListItem Value="G9">G9 - Grab-9</asp:ListItem>
                                                    <asp:ListItem Value="IM">IM - Imersion</asp:ListItem>
                                                    <asp:ListItem Value="IN">IN - Instantaneous</asp:ListItem>
                                                    <asp:ListItem Value="IS">IS - Insitu</asp:ListItem>
                                                    <asp:ListItem Value="IT">IT - IMRSTB</asp:ListItem>
                                                    <asp:ListItem Value="MC">MC - MathCL</asp:ListItem>
                                                    <asp:ListItem Value="MP">MP - MathCP</asp:ListItem>
                                                    <asp:ListItem Value="MS">MS - Measured</asp:ListItem>
                                                    <asp:ListItem Value="NA">NA - Not Applicable</asp:ListItem>
                                                    <asp:ListItem Value="NR">NR - NotRpt</asp:ListItem>
                                                    <asp:ListItem Value="OC">OC - Occurs</asp:ListItem>
                                                    <asp:ListItem Value="PC">PC - Pmpcrv</asp:ListItem>
                                                    <asp:ListItem Value="PL">PL - PmpLog</asp:ListItem>
                                                    <asp:ListItem Value="RC">RC - Recorder</asp:ListItem>
                                                    <asp:ListItem Value="RD">RD - Rng Da</asp:ListItem>
                                                    <asp:ListItem Value="RF">RF - RCDFLO</asp:ListItem>
                                                    <asp:ListItem Value="RG">RG - Range C</asp:ListItem>
                                                    <asp:ListItem Value="RP">RP - REPRES</asp:ListItem>
                                                    <asp:ListItem Value="RT">RT - RCOTOT</asp:ListItem>
                                                    <asp:ListItem Value="R4">R4 - RNG 4A</asp:ListItem>
                                                    <asp:ListItem Value="SR">SR - Single Reading</asp:ListItem>
                                                    <asp:ListItem Value="TI">TI - TimeMT</asp:ListItem>
                                                    <asp:ListItem Value="TM">TM - Total Z</asp:ListItem>
                                                    <asp:ListItem Value="VI">VI - Visual</asp:ListItem>
                                                    <asp:ListItem Value="01">01 - Composite 1</asp:ListItem>
                                                    <asp:ListItem Value="02">02 - Composite 2</asp:ListItem>
                                                    <asp:ListItem Value="03">03 - Composite 3</asp:ListItem>
                                                    <asp:ListItem Value="04">04 - Composite 4</asp:ListItem>
                                                    <asp:ListItem Value="05">05 - Composite 5</asp:ListItem>
                                                    <asp:ListItem Value="06">06 - Composite 6</asp:ListItem>
                                                    <asp:ListItem Value="08">08 - Comp 8</asp:ListItem>
                                                    <asp:ListItem Value="1H">1H - Average 1H</asp:ListItem>
                                                    <asp:ListItem Value="10">10 - Composite 10</asp:ListItem>
                                                    <asp:ListItem Value="12">12 - Composite 12</asp:ListItem>
                                                    <asp:ListItem Value="16">16 - Composite 16</asp:ListItem>
                                                    <asp:ListItem Value="2H">2H - Average 2H</asp:ListItem>
                                                    <asp:ListItem Value="20">20 - Composite 20</asp:ListItem>
                                                    <asp:ListItem Value="22">22 - Batch</asp:ListItem>
                                                    <asp:ListItem Value="24">24 - Composite 24</asp:ListItem>
                                                    <asp:ListItem Value="28">28 - Composite 28</asp:ListItem>
                                                    <asp:ListItem Value="3G">3G - 3 Gr / Hr</asp:ListItem>
                                                    <asp:ListItem Value="4H">4H - Average 4H</asp:ListItem>
                                                    <asp:ListItem Value="5G">5G - 5 Gr / 45 M</asp:ListItem>
                                                    <asp:ListItem Value="72">72 - Composite 72</asp:ListItem>
                                                    <asp:ListItem Value="96">96 - Composite 96</asp:ListItem>
                                                    <asp:ListItem Value="??">?? - Unknown</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSampleTypeLimit" Font-Size="8pt" ForeColor="Navy"
                                                    Font-Italic="True" runat="server"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.LimitSampleTypeDisplay") %>'></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Font-Size="8pt"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.SampleTypeDisplay") %>'
                                                    Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False">
                                <HeaderStyle Width="1%" Font-Bold="True" Font-Size="8pt"></HeaderStyle>
                                <HeaderTemplate>
                                    Sample Metadata
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblLoc" Font-Size="8pt" Visible="true" runat="server" Font-Bold="True"
                                                    Text='Location:'></asp:Label>
                                                <asp:Label ID="lblMonitorLoc" Font-Size="8pt" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMonitorLoc") %>'>
                                                </asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="Label5" Font-Size="8pt" Visible="true" runat="server" Font-Bold="True"
                                                    Text='Months Required:'></asp:Label>
                                                <asp:Label ID="lblLimitMonthsRequired" Font-Size="8pt" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMonthsRequired") %>'>
                                                </asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="Label1" Font-Size="8pt" Visible="true" runat="server" Font-Bold="True"
                                                    Text='Quant Units:'></asp:Label>
                                                <asp:Label ID="Label7" Font-Size="8pt" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QuantMUnits") %>'>
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblRptD" Font-Size="8pt" ForeColor="black" Font-Bold="True" Visible="true"
                                                    runat="server" Text='Rpt Dsg: '></asp:Label>
                                                <asp:Label ID="lblRptDsg" Font-Size="8pt" ForeColor="black" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitGroup") %>'>
                                                </asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="8pt"
                                                    Text="Conc Units:" Visible="true"></asp:Label>
                                                <asp:Label ID="Label9" runat="server" Font-Size="8pt"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.ConcMUnits") %>' Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td nowrap>&nbsp;</td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitID") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblActID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActID") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblCurrentStatus" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CurrentStatus") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblLimitMonitorLocCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitMonitorLocCode") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblLimitGroup" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LimitGroup") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblActResultsSetID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActResultsSetID") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblParamID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ParamID") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblConcMUnitsID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ConcMUnitsID") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblQuantMUnitsID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QuantMUnitsID") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="1%" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
            </tr>
        </table>
    </div>
</asp:Content>
