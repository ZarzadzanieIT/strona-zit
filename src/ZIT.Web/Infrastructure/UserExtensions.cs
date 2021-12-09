using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using ZIT.Core.Entities;
using ZIT.Infrastructure.Authorization;

namespace ZIT.Web.Infrastructure;

public static class UserExtensions
{
    public static ClaimsPrincipal ToClaimsPrincipal(this ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            //new(CustomClaimTypes.UserId, user.Id.ToString()),
            //new(CustomClaimTypes.CreatedAt, user.CreatedAt.ToString("d")),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email)
            //new(ClaimTypes.Role, user.Role)

        };

        claims.AddRange(user.GetAllEntitlements().Select(x => new Claim(Auth.Claim.Type, x)));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        return principal;
    }
}