using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZIT.Web.Infrastructure;

public class RequireEntitlementFilter : IAuthorizationFilter
{
    readonly Claim _claim;

    public RequireEntitlementFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasClaim = context.HttpContext?.User?.Claims?.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
        if (hasClaim == null || !hasClaim.Value)
        {
            context.Result = new ForbidResult();
        }
    }
}