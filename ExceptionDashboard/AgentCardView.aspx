<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="AgentCardView.aspx.cs" Inherits="ExceptionDashboard.AgentCardView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    
    <div id="mainConsultContent">
        <asp:Label ID="lblConsultantName" runat="server" Text=""></asp:Label>
        <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ConsultationView.aspx" CssClass="btnBack"/>
        <asp:Table runat="server" ID="agentCardViewTable"></asp:Table>
        <asp:Button ID="btnClear" runat="server" Text="Reset All" OnClick="btnClear_Click" />
    </div>
</asp:Content>

