using AMS.Context;
using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace AMS
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;

            log4net.Config.XmlConfigurator.Configure();
            log4net.Util.LogLog.InternalDebugging = true;
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null )
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var serializer = new JavaScriptSerializer();
                    Admin adminObj = (Admin)serializer.Deserialize(authTicket.UserData, typeof(Admin));

                    IIdentity id = new FormsIdentity(authTicket);

                    IPrincipal principal = new GenericPrincipal(id, adminObj.Role.Select(r => r.Name).ToArray());
                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception)
            {
                Response.Cookies["_adminToken"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
                new RedirectToRouteResult("home",null);
            }
           
        }

    }
}





