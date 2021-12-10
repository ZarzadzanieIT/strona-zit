using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ZIT.Web.Infrastructure;

public class RequireOneEntitlementAttribute : TypeFilterAttribute
{
    public RequireOneEntitlementAttribute(string claimType, params string[] claimValues) : base(typeof(RequireOneEntitlementFilter))
    {
        Arguments = claimValues.Select(x => new Claim(claimType, x)).ToArray();
    }
}