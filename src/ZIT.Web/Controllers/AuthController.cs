using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ZIT.Core.DTOs;
using ZIT.Core.Services;
using ZIT.Web.Infrastructure;
using ZIT.Web.Models;

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
        return View(new LoginViewModel());
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginViewModel vm, [FromQuery] string returnUrl)
    {
        if (!ModelState.IsValid)
        {
            vm.Login.Password = string.Empty;
            return View(
                vm
            );
        }

        var result = await _authService.LoginAsync(vm.Login);

        if (result.Failed)
        {
            return View(
                new LoginViewModel(
                    new LoginDto
                    {
                        Email = vm.Login.Email
                    },
                    result.Messages
                )
            );
        }

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            result.Value!.ToClaimsPrincipal(),
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