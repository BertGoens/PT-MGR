using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.VM.UserLoginVM;

namespace Patient_Transport_Migration.Controllers {
    public class AccountController : Controller {
        // GET: Account
        public ViewResult Index() {
            return View("Login", new UserLoginVM());
        }

        public ViewResult Login() {
            return View("Login", new UserLoginVM());
        }

        [HttpPost]
        public ActionResult Login(UserLoginVM vm) {
            if (ModelState.IsValid) {
                // Try Login
                if (Membership.ValidateUser(vm.Gebruikersnaam, vm.Paswoord)) {
                    // Create cookie to remember
                    var authTicket = new FormsAuthenticationTicket(
                        1, vm.Gebruikersnaam,
                        DateTime.Now, DateTime.Now.AddMinutes(15),
                        false, vm.Gebruikersnaam);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    Redirect("/Home/");
                }
                ViewBag.ErrorMessage = "Authenticatie mislukt!";
            }
            return View("Login", vm);
        }

        [HttpGet]
        public ActionResult Logout() {
            Session.Clear();
            FormsAuthentication.SignOut();
            return View("Login", new UserLoginVM());
        }
    }
}