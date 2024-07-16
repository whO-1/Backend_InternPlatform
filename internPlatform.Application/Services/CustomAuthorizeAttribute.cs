using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace internPlatform.Application.Services
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private string _redirectUrl;
        private string _role;
        public CustomAuthorizeAttribute() { }
        public CustomAuthorizeAttribute(string redirectUrl, string role) : base() 
        {
           _redirectUrl = redirectUrl;
            _role = role;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else {
                string authUrl = this._redirectUrl;
                if (String.IsNullOrEmpty(authUrl))
                {
                    authUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["RolesAuthRedirectUrl"];

                }

                if (!String.IsNullOrEmpty(authUrl))
                {
                    string[] url = authUrl.Split(new char[] { '/' });


                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { area = url[0], controller = url[1], action = url[2] })
                    );
                }
            }

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Custom authorization logic
            string[] roles = this.Roles.Split(new char[] { ',' });
            foreach (var item in roles)
            {
                if( httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole(item) )
                {
                    return true;
                }
            }

            return false; // Or your custom logic
        }
    }
}