<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="ExEvent.Master" CodeBehind="AgentView.aspx.cs" EnableEventValidation="false" Inherits="ExceptionDashboard.AgentView" %>

<asp:Content ID ="Content1" ContentPlaceHolderID="main" runat ="server">
    <script>
        (function ($) {
            "use strict";
            $(function () {
                $(window).konami({
                    cheat: function () {
                        window.location = "https://youtu.be/yu_HxA3KO_k";
                    } // end cheat
                });
            });
        }(jQuery));
    </script>
    <script type = "text/javascript">
        var popUpObj;
        var startDate = window.opener.document.getElementById('txtStartDate').value;
        var endDate = window.opener.document.getElementById('txtEndDate');
        var topLevel = window.opener.document.getElementById('listTopLevel');
        var department = window.opener.document.getElementById('listDepartment');
        var employee = window.opener.document.getElementById('listRepresentative');
        var status = window.opener.document.getElementById('listStatus');
        function showModalPopUp() {
            startDate = document.getElementById('<%= txtStartDate.ClientID %>').value;
            endDate = document.getElementById('<%= txtEndDate.ClientID %>').value;
            topLevel = document.getElementById('<%= listTopLevel.ClientID %>').value;
            department = document.getElementById('<%= listDepartment.ClientID %>').value;
            employee = document.getElementById('<%= listRepresentative.ClientID %>').value;
            status = document.getElementById('<%= listStatus.ClientID %>').value;
            updateURL = "EditSubmission.aspx" 
                + "?startDate=" + startDate
                + "&endDate=" + endDate
                + "&topLevel=" + topLevel
                + "&department=" + department
                + "&employee=" + employee
                + "&status=" + status;
            popUpObj = window.open(updateURL,
            "ModalPopUp",
            "toolbar=no," +
            "scrollbars=no," +
            "location=no," +
            "statusbar=no," +
            "titlebar=no," +
            "menubar=no," +
            "resizable=0," +
            "width=430," +
            "left=20," +
            "height=550,"
            );
            popUpObj.focus();
            LoadModalDiv();
        }
    </script>
    <script type = "text/javascript">
        function LoadModalDiv() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "block";
            
        }
    </script>
    <script type = "text/javascript">
        function HideModalDiv() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "none";
            
            document.getElementById('<%=btnViewReport %>').dispatchEvent(onclick);
        }
    </script>
    <div id = "divBackground" style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; left:0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8;display:none"></div>
    <asp:Panel runat="server" BackColor="#ffffff">
        <div id="searchFields">
        <asp:Table ID="searchTable" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Start Date:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="End Date:"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="txtStartDate" CssClass="txtDate" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtEndDate" CssClass="txtDate" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2">
                    <asp:Button ID="btnBPToDate1" runat="server" Text="All" OnClick="btnBPToDate_Click1" />

                    <asp:Button ID="btnBPToDate" runat="server" Text="< 72 Hours" OnClick="btnBPToDate_Click"/>

                    <asp:Button ID="btnPriorBP" runat="server" Text="> 72 Hours" OnClick="btnPriorBP_Click"/>

                    <asp:Button ID="btnMonthToDate" runat="server" Text="Month to Date" OnClick="btnMonthToDate_Click"/>

                    <asp:Button ID="btnLastMonth" runat="server" Text="Last Month" OnClick="btnLastMonth_Click"/>
                    <br />
                    <br />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Top Level"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Department"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" id="lblTopLevel" Text="Employee"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" id="lblStatus" Text="Status"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:DropDownList ID="listTopLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listTopLevel_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="listDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listDepartment_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="listRepresentative" runat="server" EnableViewState="true" OnSelectedIndexChanged="listRepresentative_SelectedIndexChanged"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="listStatus" runat="server" EnableViewState="true"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnViewReport" runat="server" Text="View Report" OnClick="btnViewReport_Click" OnClientClick="btnViewReport_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br /> 
        </div>
        <div id="mainContent">
        <asp:GridView ID="gvExEvent" runat="server" CssClass="grid" EmptyDataText="There are no data records to display." DataKeyNames="eventID" AllowSorting="false" OnSorting="gvExEvent_Sorting" AutoGenerateColumns = "false" OnRowCreated="gvExEvent_RowCreated" OnRowDataBound="gvExEvent_RowDataBound" OnRowCommand="gvExEvent_RowCommand" OnSelectedIndexChanged="gvExEvent_SelectedIndexChanged" EnableViewState="true">
            <AlternatingRowStyle BackColor="#e4efc7" />
            <HeaderStyle BackColor="#8cb529" ForeColor="white" Height="40px"/>
            <Columns>
                <asp:BoundField DataField="agentName" HeaderText="Agent" SortExpression="agentName" />
                <asp:BoundField DataField="eventDate" DataFormatString="{0:d}" HeaderText="Event Date" SortExpression="eventDate" />
                
                <asp:TemplateField HeaderText="Activity" ShowHeader="true">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlActivity" runat="server" AutoPostBack="true" HeaderText="Activity" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblfActivity" Text='<%# Bind("activityName") %>' HeaderText="Activity Name" SortExpression="activityName" />
                    </ItemTemplate>
                </asp:TemplateField>

                

                <asp:BoundField DataField="startTime" HeaderText="Start Time" SortExpression="startTime" />
                <asp:BoundField DataField="endTime" HeaderText="End Time" SortExpression="endTime" />
                <asp:BoundField DataField="duration" HeaderText="Duration" SortExpression="duration" />
                <asp:BoundField DataField="statusName" HeaderText="Status" SortExpression="statusName" />
                <asp:TemplateField ShowHeader="true">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CSSClass="btn" HeaderText="Edit" CommandName="EditEvent" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbComplete"  runat="server"  CausesValidation="false" CSSClass="btn" HeaderText="Mark Completed" CommandName="CompleteEvent" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="Page_Load">Mark Completed</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblNoData" runat="server" ForeColor="Red" Text="No records to display." />
        </div>
    </asp:Panel>
</asp:Content>
