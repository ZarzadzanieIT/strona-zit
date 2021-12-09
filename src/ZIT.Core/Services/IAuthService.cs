using ZIT.Core.Entities;

namespace ZIT.Core.Services;

public interface IAuthService
{
    Task<ApplicationUser?> GetByEmail(string email);
}