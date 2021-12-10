using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using ZIT.Core.DTOs;
using ZIT.Core.Entities;
using ZIT.Infrastructure.Authorization;

namespace ZIT.Web.Infrastructure;

public static class UserExtensions
{
    public static ClaimsPrincipal ToClaimsPrincipal(this UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userDto.Name!),
            new(ClaimTypes.Email, userDto.Email!)

        };

        claims.AddRange(userDto.Roles?.Select(x => new Claim(ClaimTypes.Role, x!)) ?? Array.Empty<Claim>());
        claims.AddRange(userDto.AllEntitlements?.Select(x => new Claim(Auth.Claim.Type, x)) ?? Array.Empty<Claim>());

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        return principal;
    }
}