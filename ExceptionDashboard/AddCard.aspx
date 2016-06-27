<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCard.aspx.cs" Inherits="ExceptionDashboard.AddCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
    <link rel="stylesheet" href="jquery-ui.css" />
    <script src="jquery-1.10.2.js"></script>
    <script src="jquery-ui.js"></script>
    <script>
        function Changed(textControl) {
            document.getElementById('lblNote').value = textControl.value;
            <%--document.getElementById('<% = lblNote.ClientID %>').value = document.getElementById('<% = txtNote.ClientID %>').value;--%>
        }
        function setTextNoteFocus() {
            document.getElementById('txtNote').className = "";
            document.getElementById('txtNote').value = "";
            document.getElementById('txtNote').className = "txtActive";
        }
    </script>
<script type = "text/javascript">
        function OnClose()
        {
        if(window.opener != null && !window.opener.closed) 
        {
            opener.location.href = './AgentCardView.aspx?agent=' + <% = Request.QueryString["agent"] %>;
            window.opener.HideModalDiv();
            window.close();
        }
        }
        window.onunload = OnClose;
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
</head>
<body>
    <form id="formAddCard" runat="server">
    <div id="addDetails">
        <asp:Label runat="server" ID="lblAddCard" Text="Add Card"></asp:Label><br /><br />
        <asp:Label runat="server" ID="lblAwardMethod" Text="Award Method: "></asp:Label> <asp:DropDownList runat="server" ID="ddlMethod" EnableViewState="true"></asp:DropDownList><br />
        <%--Include note in email? <asp:CheckBox runat="server" ID="emailNote" OnCheckedChanged="emailNote_CheckedChanged" /><br />--%>
        <%--Note: <br />--%>
        
        
    </div>
    <asp:Label runat="server" ID="lblEmailPreview" Text="Preview email to agent:"></asp:Label><br />
    <div id="emailPreview">
        
        <asp:Label runat="server" ID="lblCongrats" Text="Congratulations "></asp:Label>
        <br />
        <br />
        <asp:Label runat="server" ID="lblAwarded"></asp:Label>
        <br />
        <br />
        <!--Pic goes here-->
        <asp:Label id="imgCard" runat="server"></asp:Label>
        <br />
        <br />
        <asp:TextBox runat="server" id="txtNote" class="txtDefault" Height="50px" Width="315px" TextMode="MultiLine" Text="Add a custom note to email.." onfocus="setTextNoteFocus()" ></asp:TextBox>
        <br />
        <br />
        <%--<asp:Label runat="server" ClientIDMode="AutoID" id="lblNote" ></asp:Label>
        <br />
        <br />--%>
        This puts you one step closer to earning an entry into the monthly custom swag drawing.<br /><br />
        Keep up the great work!
    </div>
        <div id="btnAddDiv">
        <asp:Button runat="server" ID="btnAddCard" Text="Add Card" OnClick="btnAddCard_Click"/><br />
        </div>
        </form>
</body>
</html>
