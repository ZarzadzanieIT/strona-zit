using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ZIT.Core.DTOs;
using ZIT.Core.Services;
using ZIT.Web.Infrastructure;

namespace ZIT.Web.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpGet("login")]
    public IActionResult LoginAsync([FromQuery] string returnUrl)
    {
        return View(new LoginDto());
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto dto, [FromQuery] string returnUrl)
    {
        var query = Request.QueryString.Value;
        var user = await _authService.LoginAsync(dto);
        if (user == null)
        {
            return View(new LoginDto
            {
                Email = dto.Email
            });
        }

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user.ToClaimsPrincipal(),
            new AuthenticationProperties
            {
                IsPersistent = true
            });

        return returnUrl switch
        {
            var url when !string.IsNullOrWhiteSpace(url) => LocalRedirect(url),
            _ => RedirectToAction("Index", "Home")

        };
    }

    [Route("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return LocalRedirect("/");
    }

    [Route("forbidden")]
    public async Task<IActionResult> ForbiddenAsync()
    {
        return View();
    }
}