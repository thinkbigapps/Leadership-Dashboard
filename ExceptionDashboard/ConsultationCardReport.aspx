﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="ConsultationCardReport.aspx.cs" Inherits="ExceptionDashboard.ConsultationCardReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div id="reportLeft">
        <asp:Label ID="lblReportMonth" runat="server" Text="Consultation Card Report for:"></asp:Label>
        <asp:DropDownList ID="ddlReportMonth" runat="server"></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblMostAwarded" runat="server" Text="Most Awarded Card"></asp:Label>
        <asp:Label ID="lblMostAwardedImage" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblLeastAwarded" runat="server" Text="Least Awarded Card"></asp:Label>
        <asp:Label ID="lblLeastAwardedImage" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblTargetedFirst" runat="server" Text="Cards agents targeted first"></asp:Label>
        <asp:Label ID="lblTargetedFirstImages" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblTargetedLast" runat="server" Text="Cards agents targeted last"></asp:Label>
        <asp:Label ID="lblTargetedLastImages" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblCompletionTime" runat="server" Text="Average entry completion time:"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblAwardMethod" runat="server" Text="Card Award Method"></asp:Label>
        <br />
        <asp:Table ID="awardMethodTable" runat="server">

        </asp:Table>
    </div>
    <div id="reportRight">
        <asp:Label ID="lblEntriesByDept" runat="server" Text="Total entries by department"></asp:Label>
        <br />
        <asp:Table ID="entriesByDeptTable" runat="server">

        </asp:Table>
        <br />
        <br />
        <asp:Label ID="lblTopTeamsByDept" runat="server" Text="Top teams by department"></asp:Label>
        <asp:Table ID="topTeamsByDeptTable" runat="server">

        </asp:Table>
    </div>
</asp:Content>
