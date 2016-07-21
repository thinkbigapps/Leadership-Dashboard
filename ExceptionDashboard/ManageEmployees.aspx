<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="ManageEmployees.aspx.cs" Inherits="ExceptionDashboard.ManageEmployees" %>
<asp:Content ID ="Content1" ContentPlaceHolderID="main" runat ="server">
    <script type = "text/javascript">
        var popUpObj;
        
        function showModalPopUp() {
            popUpObj = window.open("EditEmployee.aspx",
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
            "height=315,"
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
                    <asp:Label runat="server" Text="Top Level"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Department"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" id="lblTopLevel" Text="Employee"></asp:Label>
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
        <asp:GridView ID="gvExEvent" runat="server" CssClass="grid" EmptyDataText="There are no data records to display." DataKeyNames="employeeID" AllowSorting="false" OnSorting="gvExEvent_Sorting" AutoGenerateColumns = "false" OnRowCreated="gvExEvent_RowCreated" OnRowDataBound="gvExEvent_RowDataBound" OnRowCommand="gvExEvent_RowCommand" OnSelectedIndexChanged="gvExEvent_SelectedIndexChanged" EnableViewState="true">
            <AlternatingRowStyle BackColor="#e4efc7" />
            <HeaderStyle BackColor="#8cb529" ForeColor="white" Height="40px"/>
            <Columns>
                <asp:BoundField DataField="fullName" HeaderText="Agent" SortExpression="fullName" />
                
                <asp:TemplateField HeaderText="Department" ShowHeader="true">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" HeaderText="Department" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblfDepartment" Text='<%# Bind("departmentName") %>' HeaderText="Department" SortExpression="departmentName" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Role" ShowHeader="true">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" HeaderText="Role" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblfRole" Text='<%# Bind("roleName") %>' HeaderText="Role" SortExpression="roleName" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Supervisor" ShowHeader="true">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlSupervisor" runat="server" AutoPostBack="true" HeaderText="Supervisor" OnSelectedIndexChanged="ddlSupervisor_SelectedIndexChanged"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblfSupervisor" Text='<%# Bind("FullSupName") %>' HeaderText="Supervisor" SortExpression="FullSupName" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="true">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CSSClass="btn" HeaderText="Edit" CommandName="EditEmployee" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:Label ID="lblNoData" runat="server" ForeColor="Red" Text="No records to display." />
        </div>
    </asp:Panel>
</asp:Content>
