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
            //document.getElementById('<% = lblNote.ClientID %>').value = document.getElementById('<% = txtNote.ClientID %>').value;
        }
    </script>
    <script>
        function OnClose() {
            window.opener.HideModalDiv();
        }
        window.onunload = OnClose;
    </script>
</head>
<body>
    <form id="formAddCard" runat="server">
    <div id="addDetails">
        <asp:Label runat="server">Add Card: </asp:Label><br /><br />
        Method: <asp:DropDownList runat="server" ID="ddlMethod" EnableViewState="true"></asp:DropDownList><br />
        Include note in email? <asp:CheckBox runat="server" AutoPostBack="true" ID="emailNote" OnCheckedChanged="emailNote_CheckedChanged" /><br />
        Note: <br />
        <asp:TextBox runat="server" id="txtNote" onkeyup="document.getElementById('lblNote').innerHTML=this.value;" ></asp:TextBox>
        <asp:Button runat="server" ID="btnAddCard" Text="Add Card" OnClick="btnAddCard_Click"/><br />
    </div>
    </form>
    <div id="emailPreview">
        Email to agent:<br />
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
        <asp:Label runat="server" ClientIDMode="AutoID" id="lblNote" ></asp:Label>
        <br />
        <br />
        This puts you one step closer to earning an entry into the monthly custom swag drawing.<br /><br />
        Keep up the great work!
    </div>
</body>
</html>
