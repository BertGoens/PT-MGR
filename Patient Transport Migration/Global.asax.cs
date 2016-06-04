using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Patient_Transport_Migration {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            StartDatabase();
        }

        private void StartDatabase() {
            // Voor Migrations, geeft error als configuratie ergens niet klopt.
            var db = new Models.MSSQLContext();
            var doctorList = db.tblDokters.First();
        }

    }
}
