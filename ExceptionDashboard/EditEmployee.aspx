<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="ExceptionDashboard.EditEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Employee</title>
    <link rel="stylesheet" href="style.css" />
    <link rel="stylesheet" href="jquery-ui.css" />
    <script src="jquery-1.10.2.js"></script>
    <script src="jquery-ui.js"></script>
    <script>
        function CloseWindow() {
            window.close();
        }
        function OnClose() {
            window.opener.HideModalDiv();
        }
        window.onunload = OnClose;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="editEmployeeMain">
        <h3 id="headerText">Edit Employee</h3>
        
        <asp:Table ID="EditEmployeeTable" runat="server">
<%--        <asp:TableRow>
                <asp:TableCell>
                    
                </asp:TableCell>
                <asp:TableCell>
                    
                </asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Text="Name: " ID="lblName" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label Text="" ID="lblNameField" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblRole" Text="Role: " runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblRoleField" Text="" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblDepartment" Text="Department: " runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" EnableViewState="true" AutoPostBack="true"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblSupervisor" Text="Supervisor: " runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlSupervisor" runat="server"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" ID="deactivateEmployee">
                    <asp:CheckBox ID="ckDeactivate" runat="server" />
                    <asp:Label ID="lblDeactivate" runat="server" Text="Deactivate"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" ID="editEmployeeButtons">
                    <asp:Button runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click" />
                
                    <asp:Button runat="server" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" OnClientClick="javascript:window.close();"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
