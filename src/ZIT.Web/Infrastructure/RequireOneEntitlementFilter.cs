using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZIT.Web.Infrastructure;

public class RequireOneEntitlementFilter : IAuthorizationFilter
{
    private readonly Claim[] _claims;

    public RequireOneEntitlementFilter(Claim[] claims)
    {
        _claims = claims;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasClaim =
            context.HttpContext?.User?.Claims?.Any(x => _claims.Any(y => y.Type == x.Type && y.Value == x.Value)) 
            ?? false;
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}