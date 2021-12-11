using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZIT.Web.Infrastructure;

public class RequireEntitlementFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public RequireEntitlementFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isAuthenticated = context.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        if (!isAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var hasClaim = context.HttpContext?.User?.Claims?.Any(c => c.Type == _claim.Type && c.Value == _claim.Value) ?? false;
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}