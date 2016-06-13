<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="ConsultationView.aspx.cs" Inherits="ExceptionDashboard.ConsultationView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
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
            <asp:Table ID="consultTable" CssClass="grid" runat="server" >
                <asp:TableHeaderRow BackColor="#8cb529" ForeColor="white" Height="40px">
                    <asp:TableHeaderCell>
                        <asp:Label runat="server" Text="Agent"></asp:Label>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <asp:Label runat="server" Text="Cards Earned"></asp:Label>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <asp:Label runat="server" Text="# Earned"></asp:Label>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <asp:Label runat="server" Text="Entries"></asp:Label>
                    </asp:TableHeaderCell>
<%--                    <asp:TableHeaderCell>
                        <asp:Label runat="server" Text="Team Rank"></asp:Label>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <asp:Label runat="server" Text="Dept Rank"></asp:Label>
                    </asp:TableHeaderCell>--%>
                    <asp:TableHeaderCell>
                        <asp:Label runat="server" Text="Edit Cards"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </asp:Panel>
</asp:Content>
