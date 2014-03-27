<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Balance.aspx.cs" Inherits="RiksLunchenSaldoWebUi.Balance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rikslunchen saldo</title>
    <link href="saldo.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="coloredtext largelineheight">
            <a href="index.html">Välj ett annat kort</a><br />
            <a href="/">Telefant</a>
        </div>

        <div class="centered">


            <asp:Panel ID="panelHasData" runat="server">

                <div class="coloredtext">
                    Aktuellt saldo:
            <div class="largetext bold">
              <%= String.Format("{0:0.00}", BalanceData.balance) %> kr  
                
            </div>
                    
                    Senaste påfyllt:  <%= BalanceData.lastTopupDate.ToString("yyyy-MM-dd") %><br>

                    <asp:Panel ID="panelHasError" runat="server">
                        <div class="errortext bold">OBS! Kortet ej giltigt</div>
                    </asp:Panel>
                </div>

            </asp:Panel>


            <asp:Panel ID="panelHasNoData" runat="server">
                <div class="coloredtext">
                    Kortnummer:  <%= IncomingCardNo %><br />


                    <div class="errortext bold">Fel: Kunde inte hämta saldo</div>

                </div>


            </asp:Panel>



        </div>
    </form>

</body>
</html>
