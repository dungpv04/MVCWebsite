using MVCWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCWebsite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private ILoginServices loginServices;
        public UserController(ILoginServices _loginServices)
        {
            this.loginServices = _loginServices;
        }
        public ActionResult Login()
        {
            LoginModel model = new LoginModel
            {
                ifValid = true,

            };
            return View(model);
        }

        public ActionResult LoginSubmit(LoginModel loginData)
        {
            LoginModel user = loginServices.LoginSubmit(loginData);

            if(!user.ifValid)
            {
                return View("Login", user);
            }
            FormsAuthentication.SetAuthCookie(user.username, false);
            var a = FormsAuthentication.FormsCookieName;
            return RedirectToAction("Index", "Book");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        public ActionResult SignUp()
        {
            return View(new RegisterModel { ifValid = true, ifMatch = true });
        }

        public ActionResult SignUpSubmit(RegisterModel newUser)
        {
            var user = loginServices.SignUpSubmit(newUser);
            if(!user.ifValid || !user.ifMatch)
            {
                return View("SignUp", user);
            }
            return RedirectToAction("Login", "User");
        }

    }
}