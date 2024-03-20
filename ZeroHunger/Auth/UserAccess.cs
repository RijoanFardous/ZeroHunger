using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Auth
{
    public class UserAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = (User)httpContext.Session["user"];

            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}