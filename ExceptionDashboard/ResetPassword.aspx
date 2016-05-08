<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ExceptionDashboard.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div id="createAccountMain">
        <asp:Label ID="lblResetSubmitted" runat="server" Text="An email has been sent to the address on file with information to reset your password."></asp:Label>
        <asp:Table runat="server" ID="tbResetPassword">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ColumnSpan="2">
                    <h2>Reset Password</h2>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>

            <asp:TableRow ID="trUsername">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Username:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtUsername"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow ID="trPassword">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Password:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="trConfirmPass">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Confirm Password:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtPassword2" TextMode="Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow ID="trEmail">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Email Address:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtEmailAddress"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Button runat="server" ID="submitResetPassword" Text="Submit" OnClick="submitResetPassword_Click"/>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Button runat="server" ID="submitConfirmReset" Text="Reset" OnClick="submitConfirmReset_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
