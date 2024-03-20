using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Auth
{
    public class RestaurantAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = (User)httpContext.Session["user"];

            if (user != null && user.Type.Equals("Restaurant"))
            {
                return true;
            }
            return false;
        }
    }
}