using Sitecore;
using Sitecore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SitecoreCookbook.Controllers
{
    public class SecurityController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string userName, string password, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                string domainUser = Sitecore.Context.Domain.GetFullName(userName);
                if (Sitecore.Security.Authentication.AuthenticationManager.Login(domainUser, password, rememberMe))
                {
                    string returnUrl = System.Web.HttpContext.Current.Request["url"];
                    if (!Url.IsLocalUrl(returnUrl))
                        returnUrl = "/";
                    return Redirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string password, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string domainUser = Context.Domain.GetFullName(userName);

                    if (password != confirmPassword)
                        ModelState.AddModelError("", "Both passwords does not match.");

                    if (Sitecore.Security.Accounts.User.Exists(domainUser))
                        ModelState.AddModelError("", "User already exists.");
                    
                    System.Web.Security.Membership.CreateUser(domainUser, password);
                    if (AuthenticationManager.Login(domainUser, password))
                        return Redirect("/");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string password, string confirmPassword)
        {
            string domainUser = Context.Domain.GetFullName(userName);

            System.Web.Security.Membership.CreateUser(domainUser, password);
            
            if (AuthenticationManager.Login(domainUser, password))
            {
                string returnUrl = System.Web.HttpContext.Current.Request["url"];
                if (!Url.IsLocalUrl(returnUrl))
                    returnUrl = "/";
                return Redirect(returnUrl);
            }

            return View();
        }
	}
}