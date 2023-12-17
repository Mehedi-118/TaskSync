using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PTSL.Ovidhan.Web.Core.Helper.Enum;

namespace PTSL.Ovidhan.Web.Core.Helper.Auth;

public class RequireAuthRole : Attribute, IAuthorizationFilter
{
    public string Roles { get; set; } = string.Empty;
    public string RedirectTo { get; set; } = string.Empty;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleName = context.HttpContext.Session.GetString(SessionKey.RoleName);

        if (Roles.Split(',').Contains(roleName) == false)
        {
            context.HttpContext.Session.SetString("Message", "You do not have permission to view this page");
            context.HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            if (roleName == RoleNames.Beneficiary)
            {
                context.Result = RedirectToBeneficiaryIndex();
            }
            else
            {
                context.Result = RedirectToPage(RedirectTo);
            }
        }
    }

    public IActionResult RedirectToPage(string redirectTo)
    {
        var split = redirectTo.Split('/');

        if (split.Length == 2 )
        {
            return new RedirectToRouteResult(new RouteValueDictionary { { "action", split[1] }, { "controller", split[0] } });
        }

        return new RedirectToRouteResult(new RouteValueDictionary { { "action", "Login" }, { "controller", "Account" } });
    }

    public IActionResult RedirectToBeneficiaryIndex()
    {
        return new RedirectToRouteResult(new RouteValueDictionary { { "action", "BeneficiaryIndex" }, { "controller", "Home" } });
    }
}

