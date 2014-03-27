<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Balance.aspx.cs" Inherits="RiksLunchenSaldoWebUi.Balance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rikslunchen saldo</title>
</head>
<body>
    
    <h1>Rikslunchen saldo</h1>
    <form id="form1" runat="server">
    <div>
        	Kortnummer:  <%= BalanceData.cardNo %> <br/>
        	Saldo:  <b><%= BalanceData.balance %></b> kr  <br/>
        	Senaste påfyllningsdatum::  <%= BalanceData.lastTopupDate.ToString("yyyy-MM-dd") %> <br>
        	Giltigt:  <%= BalanceData.valid?"Ja":"Nej" %> 
    </div>
    </form>
</body>
</html>
