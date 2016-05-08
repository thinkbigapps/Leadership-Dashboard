<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentSubmit.aspx.cs" Inherits="ExceptionDashboard.AgentSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Request New Exception</title>
    <link rel="stylesheet" href="style.css" />
    <link rel="stylesheet" href="jquery-ui.css" />
    <script src="jquery-1.10.2.js"></script>
    <script src="jquery-ui.js"></script>

    <script>
        $(function () {
            $(".txtDate").datepicker({
                buttonText: "Select date",
                showAnim: "slideDown"
            });
        });
    </script>
</head>
<body style="background-color:#e4efc7;">
    <div id="mainpanel">
    <script type = "text/javascript">
        function OnClose()
        {
        if(window.opener != null && !window.opener.closed) 
        {
            opener.location.href = './AgentView.aspx?event="newRequest"'
            window.opener.HideModalDiv();
        }
        }
        window.onunload = OnClose;
    </script>
    <asp:Panel runat="server">
        <div id="header">
            <asp:Label runat="server" CssClass="headertext" Text="Request New Exception"></asp:Label>
        </div>
        <form id="submitNew" runat="server">
        <div id="submitmain">
        <asp:Table runat="server" id="edittable">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Event Date:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" CssClass="txtDate" ID="txtEventDate"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Activity:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="listActivity"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Start:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="startHour">
                        <asp:ListItem value="01" text="01" />
                        <asp:ListItem value="02" text="02" />
                        <asp:ListItem value="03" text="03" />
                        <asp:ListItem value="04" text="04" />
                        <asp:ListItem value="05" text="05" />
                        <asp:ListItem value="06" text="06" />
                        <asp:ListItem value="07" text="07" />
                        <asp:ListItem value="08" text="08" />
                        <asp:ListItem value="09" text="09" />
                        <asp:ListItem value="10" text="10" />
                        <asp:ListItem value="11" text="11" />
                        <asp:ListItem value="12" text="12" />
                    </asp:DropDownList>

                    <asp:DropDownList runat="server" ID="startMinute">
                        <asp:ListItem value="00" text="00" />
                        <asp:ListItem value="01" text="01" />
                        <asp:ListItem value="02" text="02" />
                        <asp:ListItem value="03" text="03" />
                        <asp:ListItem value="04" text="04" />
                        <asp:ListItem value="05" text="05" />
                        <asp:ListItem value="06" text="06" />
                        <asp:ListItem value="07" text="07" />
                        <asp:ListItem value="08" text="08" />
                        <asp:ListItem value="09" text="09" />
                        <asp:ListItem value="10" text="10" />
                        <asp:ListItem value="11" text="11" />
                        <asp:ListItem value="12" text="12" />
                        <asp:ListItem value="13" text="13" />
                        <asp:ListItem value="14" text="14" />
                        <asp:ListItem value="15" text="15" />
                        <asp:ListItem value="16" text="16" />
                        <asp:ListItem value="17" text="17" />
                        <asp:ListItem value="18" text="18" />
                        <asp:ListItem value="19" text="19" />
                        <asp:ListItem value="20" text="20" />
                        <asp:ListItem value="21" text="21" />   
                        <asp:ListItem value="22" text="22" />
                        <asp:ListItem value="23" text="23" />
                        <asp:ListItem value="24" text="24" />
                        <asp:ListItem value="25" text="25" />
                        <asp:ListItem value="26" text="26" />
                        <asp:ListItem value="27" text="27" />
                        <asp:ListItem value="28" text="28" />
                        <asp:ListItem value="29" text="29" />
                        <asp:ListItem value="30" text="30" />
                        <asp:ListItem value="33" text="31" />  
                        <asp:ListItem value="32" text="32" />
                        <asp:ListItem value="33" text="33" />
                        <asp:ListItem value="34" text="34" />
                        <asp:ListItem value="35" text="35" />
                        <asp:ListItem value="36" text="36" />
                        <asp:ListItem value="37" text="37" />
                        <asp:ListItem value="38" text="38" />
                        <asp:ListItem value="39" text="39" />
                        <asp:ListItem value="40" text="40" />
                        <asp:ListItem value="41" text="41" /> 
                        <asp:ListItem value="42" text="42" />
                        <asp:ListItem value="43" text="43" />
                        <asp:ListItem value="44" text="44" />
                        <asp:ListItem value="45" text="45" />
                        <asp:ListItem value="46" text="46" />
                        <asp:ListItem value="47" text="47" />
                        <asp:ListItem value="48" text="48" />
                        <asp:ListItem value="49" text="49" />
                        <asp:ListItem value="50" text="50" />
                        <asp:ListItem value="51" text="51" />
                        <asp:ListItem value="52" text="52" />
                        <asp:ListItem value="53" text="53" />
                        <asp:ListItem value="54" text="54" />
                        <asp:ListItem value="55" text="55" />
                        <asp:ListItem value="56" text="56" />
                        <asp:ListItem value="57" text="57" />
                        <asp:ListItem value="58" text="58" />
                        <asp:ListItem value="59" text="59" />                              
                    </asp:DropDownList>

                    <asp:DropDownList runat="server" ID="startAMPM">
                        <asp:ListItem value="AM" text="AM" />
                        <asp:ListItem value="PM" text="PM" />
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="End:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="endHour">            
                        <asp:ListItem value="01" text="01" />
                        <asp:ListItem value="02" text="02" />
                        <asp:ListItem value="03" text="03" />
                        <asp:ListItem value="04" text="04" />
                        <asp:ListItem value="05" text="05" />
                        <asp:ListItem value="06" text="06" />
                        <asp:ListItem value="07" text="07" />
                        <asp:ListItem value="08" text="08" />
                        <asp:ListItem value="09" text="09" />
                        <asp:ListItem value="10" text="10" />
                        <asp:ListItem value="11" text="11" />
                        <asp:ListItem value="12" text="12" />
                    </asp:DropDownList>

                    <asp:DropDownList runat="server" ID="endMinute">
                        <asp:ListItem value="00" text="00" />
                        <asp:ListItem value="01" text="01" />
                        <asp:ListItem value="02" text="02" />
                        <asp:ListItem value="03" text="03" />
                        <asp:ListItem value="04" text="04" />
                        <asp:ListItem value="05" text="05" />
                        <asp:ListItem value="06" text="06" />
                        <asp:ListItem value="07" text="07" />
                        <asp:ListItem value="08" text="08" />
                        <asp:ListItem value="09" text="09" />
                        <asp:ListItem value="10" text="10" />
                        <asp:ListItem value="11" text="11" />
                        <asp:ListItem value="12" text="12" />
                        <asp:ListItem value="13" text="13" />
                        <asp:ListItem value="14" text="14" />
                        <asp:ListItem value="15" text="15" />
                        <asp:ListItem value="16" text="16" />
                        <asp:ListItem value="17" text="17" />
                        <asp:ListItem value="18" text="18" />
                        <asp:ListItem value="19" text="19" />
                        <asp:ListItem value="20" text="20" />
                        <asp:ListItem value="21" text="21" />   
                        <asp:ListItem value="22" text="22" />
                        <asp:ListItem value="23" text="23" />
                        <asp:ListItem value="24" text="24" />
                        <asp:ListItem value="25" text="25" />
                        <asp:ListItem value="26" text="26" />
                        <asp:ListItem value="27" text="27" />
                        <asp:ListItem value="28" text="28" />
                        <asp:ListItem value="29" text="29" />
                        <asp:ListItem value="30" text="30" />
                        <asp:ListItem value="33" text="31" />  
                        <asp:ListItem value="32" text="32" />
                        <asp:ListItem value="33" text="33" />
                        <asp:ListItem value="34" text="34" />
                        <asp:ListItem value="35" text="35" />
                        <asp:ListItem value="36" text="36" />
                        <asp:ListItem value="37" text="37" />
                        <asp:ListItem value="38" text="38" />
                        <asp:ListItem value="39" text="39" />
                        <asp:ListItem value="40" text="40" />
                        <asp:ListItem value="41" text="41" /> 
                        <asp:ListItem value="42" text="42" />
                        <asp:ListItem value="43" text="43" />
                        <asp:ListItem value="44" text="44" />
                        <asp:ListItem value="45" text="45" />
                        <asp:ListItem value="46" text="46" />
                        <asp:ListItem value="47" text="47" />
                        <asp:ListItem value="48" text="48" />
                        <asp:ListItem value="49" text="49" />
                        <asp:ListItem value="50" text="50" />
                        <asp:ListItem value="51" text="51" />
                        <asp:ListItem value="52" text="52" />
                        <asp:ListItem value="53" text="53" />
                        <asp:ListItem value="54" text="54" />
                        <asp:ListItem value="55" text="55" />
                        <asp:ListItem value="56" text="56" />
                        <asp:ListItem value="57" text="57" />
                        <asp:ListItem value="58" text="58" />
                        <asp:ListItem value="59" text="59" /> 
                    </asp:DropDownList>

                    <asp:DropDownList runat="server" ID="endAMPM">
                        <asp:ListItem value="AM" text="AM" />
                        <asp:ListItem value="PM" text="PM" />
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Activity Note:"></asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtActivityNote" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="closebutton">
                    <asp:Button runat="server" Text="Submit" ID="btnSubmit" OnClick
                        ="btnSubmit_Click"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </div>
        </form>
    </asp:Panel>
    </div>
</body>
</html>
