using ZIT.Core.DTOs;
using ZIT.Core.Entities;

namespace ZIT.Core.Services;

public interface IAuthService
{
    Task<ApplicationUser?> LoginAsync(LoginDto loginDto);
}