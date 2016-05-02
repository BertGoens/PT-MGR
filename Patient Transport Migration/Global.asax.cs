using System;
using System.Web.Mvc;
using System.Web.Routing;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration {
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e) {
            Exception ex = Server.GetLastError();

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
        }

    }
}
