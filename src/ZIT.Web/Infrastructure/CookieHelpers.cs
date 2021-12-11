using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ZIT.Web.Infrastructure;

public static class CookieHelpers
{
    public static Func<RedirectContext<CookieAuthenticationOptions>, Task> HandleRedirectBasedOnUrl(
        int responseStatusCodeForApiCalls)
    => async context =>
    {
        var requestPath = context.Request.Path;

        if (requestPath.Value != null && requestPath.Value.ToLowerInvariant().Contains("api"))
        {
            context.Response.StatusCode = responseStatusCodeForApiCalls;

            await context.Response.WriteAsync("");
            return;
        }

        context.Response.Redirect(context.RedirectUri);
    };
}