using ZIT.Core.DTOs;
using ZIT.Core.Entities;

namespace ZIT.Core.Services;

public interface IAuthService
{
    Task<ApplicationResult<UserDto?>> LoginAsync(LoginDto loginDto);
}