using System.Web.Mvc;

namespace CabBooking.Controllers
{
    /// <summary>
    /// <b>Authorises session object<c>`UserID`</c></b><br></br>
    /// If found null then rediredt to <c>/Account/Login/</c>
    /// </summary>
    public class SessionAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["UserID"] == null)
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}