using EmreOrenRentACar.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmreOrenRentACar.Code
{
    public class IsAdmin : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = httpContext.Session["UserInfo"] as UserInfo;
            if (session != null && session.RoleName == "Admin")
            {
                return true;
            }

            return false;
        }
    }
}