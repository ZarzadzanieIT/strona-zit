using Microsoft.EntityFrameworkCore;
using ZIT.Core.Entities;
using ZIT.Core.Services;
using ZIT.Infrastructure.Persistence;

namespace ZIT.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;

    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> GetByEmail(string email)
        => await _dbContext.Users.Include(x => x.Roles)
            .SingleOrDefaultAsync(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
}