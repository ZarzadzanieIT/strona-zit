using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZIT.Core.DTOs;
using ZIT.Core.Services;
using ZIT.Infrastructure.Authorization;
using ZIT.Web.Infrastructure;

namespace ZIT.Web.Controllers.Api.Admin;

[ApiController]
[RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Read)]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(AddUserDto addUserDto)
    {
        await _userService.AddAsync(addUserDto);
        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var user = await _userService.GetAsync(id);
        return user switch
        {
            { } => Ok(user),
            _ => NotFound()
        };
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, UpdateUserDto updateUserDto)
    {
        if (id != updateUserDto.Id)
        {
            return BadRequest("User Id mismatch");
        }
        await _userService.UpdateAsync(updateUserDto);
        return Ok();
    }

    [RequireOneEntitlement(Auth.Claim.Type, Auth.Entitlements.Users.All, Auth.Entitlements.Users.Write)]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}