using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Comercial.Security
{
    public class ValidateSesionAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["User"] == null) {
                filterContext.Result = new RedirectResult("~/Auth/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}