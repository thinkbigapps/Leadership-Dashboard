<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="ManagerView.aspx.cs" Inherits="ExceptionDashboard.ManagerView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <script type = "text/javascript">
        var popUpObj;
        var department = window.opener.document.getElementById('listDepartment');
        function showModalPopUp() {
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
                    <asp:Label runat="server" Text="Department"></asp:Label>
                </asp:TableCell>

            </asp:TableRow>

            <asp:TableRow>

                <asp:TableCell>
                    <asp:DropDownList ID="listDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listDepartment_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>
                </asp:TableCell>

            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnViewReport" runat="server" Text="View Report" OnClick="btnViewReport_Click"  OnClientClick="btnViewReport_Click"/>
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
                <asp:BoundField DataField="supName" HeaderText="Supervisor" SortExpression="supName" />
                <asp:BoundField DataField="agentName" HeaderText="Agent" SortExpression="agentName" />
                <asp:BoundField DataField="eventDate" DataFormatString="{0:d}" HeaderText="Event Date" SortExpression="eventDate" />
                
                <asp:TemplateField HeaderText="Activity" ShowHeader="true">

                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblfActivity" Text='<%# Bind("activityName") %>' HeaderText="Activity Name" SortExpression="activityName" />
                    </ItemTemplate>
                </asp:TemplateField>

                

                <asp:BoundField DataField="startTime" HeaderText="Start Time" SortExpression="startTime" />
                <asp:BoundField DataField="endTime" HeaderText="End Time" SortExpression="endTime" />
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
