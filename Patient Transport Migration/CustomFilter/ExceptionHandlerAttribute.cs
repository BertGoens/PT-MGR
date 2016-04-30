using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.CustomFilter {
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter {
        public void OnException(ExceptionContext filterContext) {
            if (!filterContext.ExceptionHandled) {

                ExceptionLogger logger = new ExceptionLogger() {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    LogTime = DateTime.Now
                };

                MSSQLContext ctx = new MSSQLContext();
                ctx.ExceptionLoggers.Add(logger);
                ctx.SaveChanges();

                filterContext.ExceptionHandled = true;
            }
        }
    }
}