<%@ Page Title="" Language="C#" MasterPageFile="~/ExEvent.Master" AutoEventWireup="true" CodeBehind="AgentCardView.aspx.cs" Inherits="ExceptionDashboard.AgentCardView" %>
    <asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="jquery-ui.css" />
    <script src="jquery-1.10.2.js"></script>
    <script src="jquery-ui.js"></script>
    <script type = "text/javascript">
        var popUpObj;
        function showModalPopUp() {
        var agent = $('#<%= lblAgent.ClientID %>').text();
        var cardName = $('#<%= lblCard.ClientID %>').text();
        var requested = $('#<%= lblRequested.ClientID %>').text();
            popUpObj = window.open("AddCard.aspx?agent=" + agent + "&cardName=" + cardName + "&requested=" + requested,
            "ModalPopUp",
            "toolbar=no," +
            "scrollbars=no," +
            "location=no," +
            "statusbar=no," +
            "titlebar=no," +
            "menubar=no," +
            "resizable=0," +
            "width=780," +
            "left=20," +
            "height=680,"
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
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
    <div id = "divBackground" style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; left:0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8;display:none"></div>
    <div id="mainConsultContent">
        <asp:Label ID="lblAgent" runat="server" ></asp:Label>
        <asp:Label ID="lblCard" runat="server" ></asp:Label>
        <asp:Label ID="lblRequested" runat="server" text="false"/>
        <asp:Label ID="lblConsultantName" runat="server" Text=""></asp:Label>
        <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ConsultationView.aspx" CssClass="btnBack"/>
        <asp:Table runat="server" ID="agentCardViewTable"></asp:Table>
        <asp:Button ID="btnClear" runat="server" Text="Reset All" OnClick="btnClear_Click" />
    </div>
    </asp:Content>

