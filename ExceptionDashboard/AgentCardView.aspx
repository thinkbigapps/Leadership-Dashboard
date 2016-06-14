<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="AgentCardView.aspx.cs" Inherits="ExceptionDashboard.AgentCardView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div id="mainConsultContent">
        <asp:Label ID="lblConsultantName" runat="server" Text=""></asp:Label>
        <asp:Table runat="server" ID="agentCardViewTable">
        
        </asp:Table>
    </div>
</asp:Content>

