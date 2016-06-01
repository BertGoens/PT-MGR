using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Patient_Transport_Migration {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            StartDatabase();
        }

        /*
        protected void Application_Error(object sender, EventArgs e) {
            Exception ex = Server.GetLastError();
            System.Diagnostics.Debug.Print(ex.ToString());
            
            TODO Uncomment
            ExceptionLogger logger = new ExceptionLogger() {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                //ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ControllerName = sender.ToString(),
                LogTime = DateTime.Now
            };

            var context = new MSSQLContext();
            context.tblExceptionLogger.Add(logger);
            context.SaveChanges();

            //Server.ClearError();

            //Response.Redirect("/Home/Error");
            
    }*/

        private void StartDatabase() {
            // Voor Migrations, geeft error als configuratie ergens niet klopt.
            var db = new Models.MSSQLContext();
            var doctorList = db.tblDokters.First();
        }

    }
}
