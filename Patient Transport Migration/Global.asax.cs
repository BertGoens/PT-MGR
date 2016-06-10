using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            StartDatabase();
        }

        // Voor Migrations, geeft error als configuratie ergens niet klopt.
        private void StartDatabase() {
            var context = new Context();
            // NIET AANZETTEN IN PRODUCTION!
            //var contextConfig = new Patient_Transport_Migration.Migrations.Configuration().ForceSeed(context);
            var doctorList = context.tblDokters.ToList();
        }

        protected void Application_Error(object sender, EventArgs e) {
            Exception exception = Server.GetLastError();

            var context = new Context();
            context.tblExceptionLogger.Add(new Models.POCO.ExceptionLogger {
                ExceptionMessage = exception.Message,
                ExceptionStackTrace = exception.StackTrace,
            });
            context.SaveChanges();
        }
    }
}
