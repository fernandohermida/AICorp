<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AiCorporationApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server" id="AiForm">  
      <p>Amount</p>  
      <asp:TextBox ID="Amount" runat="server"/>
      <p>OrderID</p>  
      <asp:TextBox ID="OrderID" runat="server"/>
      <p>TransactionType</p>
      <asp:TextBox ID="TransactionType" runat="server"/>

      <asp:button id="SubmitButton" text="Submit" OnClick="SubmitButton_OnClick" runat="server"></asp:button>
    </form>
</body>
</html>
