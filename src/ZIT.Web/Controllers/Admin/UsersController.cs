using Microsoft.AspNetCore.Mvc;
using ZIT.Core.DTOs;
using ZIT.Core.Services;
using ZIT.Infrastructure.Authorization;
using ZIT.Web.Infrastructure;

namespace ZIT.Web.Controllers.Admin;

[RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Read)]
[Route("panel/[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        return View(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var user = await _userService.GetAsync(id);

        return View(user);
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpGet("add")]
    public IActionResult AddAsync()
    {
        return View(new AddUserDto());
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpPost("add")]
    public async Task<IActionResult> AddAsync(AddUserDto addUserDto)
    {
        await _userService.AddAsync(addUserDto);

        return RedirectToAction("Index");
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpGet("update")]
    public IActionResult UpdateAsync()
    {
        return View(new UpdateUserDto());
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdateUserDto addUserDto)
    {
        await _userService.UpdateAsync(addUserDto);

        return RedirectToAction("Index");
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _userService.DeleteAsync(id);

        return RedirectToAction("Index");
    }

}