﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZIT.Core.DTOs;
using ZIT.Core.Services;
using ZIT.Infrastructure.Persistence;
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
    public IActionResult LoginAsync()
    {
        return View(new LoginDto());
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto dto)
    {
        var user = await _authService.GetByEmail(dto.Email);
        if (user == null || !user.Password.Equals(dto.Password))
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

        return RedirectToAction("Index", "Home");
    }

    [Route("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return LocalRedirect("/");
    }
}