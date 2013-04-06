using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LookFund.Models.Membership;

namespace LookFund.Controllers.Filter
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            var model = httpContext.Session["UserKey"] as MEmploye;
            if (model == null)
                return false;
            return true;
            //if (!httpContext.User.Identity.IsAuthenticated)
            //    return false;
            //System.Web.Security.FormsAuthentication.SetAuthCookie(

            //httpContext.User.Identity.

            //httpContext.Response.Redirect();

        }
    }
}