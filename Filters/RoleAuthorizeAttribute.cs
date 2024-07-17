//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System;

//namespace SimpleASPLogin.Filters
//{
//    public class RoleAuthorizeAttribute : ActionFilterAttribute
//    {
//        private readonly string _role;

//        public RoleAuthorizeAttribute(string role)
//        {
//            _role = role;
//        }

//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            var session = context.HttpContext.Session;
//            var userRole = session.GetString("UserRole");

//            if (userRole == null || !userRole.Equals(_role, StringComparison.OrdinalIgnoreCase))
//            {
//                context.Result = new RedirectToActionResult("Login", "User", null);
//            }
//            base.OnActionExecuting(context);
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleASPLogin.Filters
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var role = session.GetString("Role");
            var rurl = context.HttpContext.Request.Path;
            if (role == null || !_roles.Contains(role))
            {
                //var returnUrl = rurl; //context.HttpContext.Request.Path;
                //var accessDeniedUrl = $"{returnUrl}&accessDenied=true";

                context.Result = new RedirectResult("/Home/Index");
                //context.HttpContext.Items["AccessDenied"] = true;
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
