using System.Web;
using System.Web.Mvc;

namespace film.Infrastructure
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Role"] != null && Roles.Contains((string)httpContext.Session["Role"]))
                return true;
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Auth/Auth");
        }
    }
}