using System;
using RiksLunchenSaldo;

namespace RiksLunchenSaldoWebUi
{
    public partial class Balance : System.Web.UI.Page
    {
        public RiksLunchenSaldoData BalanceData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            BalanceData = new RiksLunchenSaldoData();

            var cardnumber = Page.RouteData.Values["cardId"] as string;
            
            if(cardnumber==null)
                Response.Redirect("index.html");

            var success = false;
            int counter = 0;
            int maxtries = 20;
            while (!success && counter < maxtries)
            {
                try
                {
                    BalanceData = RiksLunchenSaldoProvider.GetSaldo(cardnumber);
                    success = true;
                }
                catch (Exception) { }
                counter++;
            }
        }
    }
}