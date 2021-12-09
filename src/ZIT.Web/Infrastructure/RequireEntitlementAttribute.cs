using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ZIT.Web.Infrastructure;

public class RequireEntitlementAttribute : TypeFilterAttribute
{
    public RequireEntitlementAttribute(string claimType, string claimValue) : base(typeof(RequireEntitlementFilter))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}