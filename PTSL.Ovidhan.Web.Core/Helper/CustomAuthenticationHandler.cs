using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace PTSL.Ovidhan.Web.Core.Helper;

public class CustomAuthenticationScheme : AuthenticationSchemeOptions {}

public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationScheme>
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationScheme> options,
        ILoggerFactory logger, 
        UrlEncoder encoder,
        ISystemClock clock,
        IHttpContextAccessor contextAccessor): base(options, logger, encoder, clock)
    {
        this._contextAccessor = contextAccessor;
        ArgumentNullException.ThrowIfNull(_contextAccessor.HttpContext);
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var tokenString = _contextAccessor?.HttpContext?.Session.GetString("Token");
        var userEmailString = _contextAccessor?.HttpContext?.Session.GetString("UserEmail");
        var roleNameString = _contextAccessor?.HttpContext?.Session.GetString("RoleName");

        if (string.IsNullOrEmpty(tokenString) || string.IsNullOrEmpty(userEmailString) || string.IsNullOrEmpty(roleNameString))
        {
            return Task.FromResult(AuthenticateResult.Fail("Token or UserEmail or RoleName is not found"));
        }

        var claims = new[]
        {
            new Claim("Token", tokenString),
            new Claim("UserEmail", userEmailString),
            new Claim("RoleName", roleNameString),
        };
        var claimsIdentity = new ClaimsIdentity(claims, nameof(CustomAuthenticationHandler));
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
