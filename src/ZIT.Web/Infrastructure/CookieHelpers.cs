using System.Reflection.Metadata.Ecma335;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ZIT.Web.Infrastructure;

public static class CookieHelpers
{
    private static int GetStatusCode(bool isAuthenticated)
        => isAuthenticated switch
        {
            true => 403,
            false => 401
        };

    public static Func<RedirectContext<CookieAuthenticationOptions>, Task> HandleRedirectBasedOnUrl()
    => async context =>
    {
        var isAuthenticated = context.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        var requestPath = context.Request.Path;
        if (requestPath.Value != null && requestPath.Value.ToLowerInvariant().Contains("api"))
        {
            context.Response.StatusCode = GetStatusCode(isAuthenticated);

            await context.Response.WriteAsync("");
            return;
        }

        var returnUrlParameter = context.Options.ReturnUrlParameter;
        if (!isAuthenticated)
        {
            context.Response.Redirect($"{context.Options.LoginPath}?{returnUrlParameter}={HttpUtility.UrlEncode(requestPath)}");
            return;
        }


        

        context.Response.Redirect(context.RedirectUri);
    };
}