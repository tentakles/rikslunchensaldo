using System;
using RiksLunchenSaldo;

namespace RiksLunchenSaldoWebUi
{
    public partial class Balance : System.Web.UI.Page
    {
        public RiksLunchenSaldoData BalanceData { get; set; }
        public bool HasData { get; set; }
        public string IncomingCardNo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            BalanceData = new RiksLunchenSaldoData();

            var cardnumber = Page.RouteData.Values["cardId"] as string;
            
            if(cardnumber==null)
                Response.Redirect("index.html");

            IncomingCardNo = cardnumber;
            var success = false;
            int counter = 0;
            int maxtries = 10;
            while (!success && counter < maxtries)
            {
                try
                {
                    BalanceData = RiksLunchenSaldoProvider.GetSaldo(cardnumber);
                    success = true;
                    HasData = true;
                }
                catch (Exception) { }
                counter++;
            }

            if (BalanceData.lastTopupDate.CompareTo(DateTime.MinValue) == 0)
                HasData = false;
            
       //     HasData = true;
            
            panelHasData.Visible = HasData;
            panelHasNoData.Visible = !HasData;
            panelHasError.Visible = !BalanceData.valid;

        }
    }
}