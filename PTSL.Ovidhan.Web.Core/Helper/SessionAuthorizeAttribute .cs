using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Text.Json;

namespace PTSL.Ovidhan.Web.Core.Helper
{
	/*
	public class SessionAuthorizeAttribute : ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext context)
        {
			var sessionString = context.HttpContext.Session.GetString("Token");
			if (string.IsNullOrEmpty(sessionString))
			{
				context.Result = RedirectToLogin();
			}
        }

		/*
        public void OnAuthorization(AuthorizationFilterContext context)
		{
			var sessionString = context.HttpContext.Session.GetString("Token");
			if (string.IsNullOrEmpty(sessionString))
			{
				context.Result = RedirectToLogin();
			}
		}

		public IActionResult RedirectToLogin()
		{
			return new RedirectToRouteResult(new RouteValueDictionary{{ "action", "Login" }, { "controller", "Account" }});
		}
	}
	*/

	/*
	public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
			var user = context.HttpContext.User;
			if (user is null)
			{
				context.Result = RedirectToLogin();
			}
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
			var user = context.HttpContext.User;
			if (user is null)
			{
				context.Result = RedirectToLogin();
			}
        }

        public IActionResult RedirectToLogin()
		{
			return new RedirectToRouteResult(new RouteValueDictionary{{ "action", "Login" }, { "controller", "Account" }});
		}
	}
	*/

	public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			//var sessionString = context.HttpContext.Session.GetString("Token");
			var identity = context.HttpContext.User.Identity;
			if (identity is null || identity.IsAuthenticated == false)
			{
				context.Result = RedirectToLogin();
			}
        }

        public IActionResult RedirectToLogin()
		{
			return new RedirectToRouteResult(new RouteValueDictionary{{ "action", "Login" }, { "controller", "Account" }});
		}
	}
}