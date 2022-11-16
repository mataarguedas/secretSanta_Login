using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace secretSanta_Login.Permissions
{
    public class ValidateSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["username"] == null) {
                filterContext.Result = new RedirectResult("~/Access/Login");
            }

            base.OnActionExecuting(filterContext);
        }

    }
}