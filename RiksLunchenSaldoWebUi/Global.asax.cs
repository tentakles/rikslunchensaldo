using System.Web.Routing;

namespace RiksLunchenSaldoWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
     
            RegisterRoutes(RouteTable.Routes);
    
        }

        private void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.MapPageRoute("getsaldo","{cardId}","~/Balance.aspx");
        }
    }
}
