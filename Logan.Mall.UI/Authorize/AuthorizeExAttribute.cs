using Logan.Mall.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Logan.Mall.UI.Authorize
{
    public class AuthorizeExAttribute : AuthorizeAttribute
    {
        public string PermissionName { get; private set; }

        public AuthorizeExAttribute(string permissionName)
        {
            this.PermissionName = permissionName;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                return false;
            }
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (base.AuthorizeCore(httpContext))
                {
                    return true;
                }

            }
            httpContext.Response.StatusCode = 403;
            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    filterContext.Result = new RedirectResult("/Error");
                else
                    filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl + "?returnUrl=" + filterContext.HttpContext.Request.UrlReferrer);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }
    }
}