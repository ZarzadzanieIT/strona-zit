using System.Collections.Generic;
using ZIT.Core.Entities;
using ZIT.Infrastructure.Authorization;

namespace ZIT.Infrastructure.Persistence;

public class DataSeeder
{
    private readonly AppDbContext _dbContext;

    public DataSeeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        var adminRole = new ApplicationRole(Guid.NewGuid(), "Admin", new List<string>
        {
            Auth.Entitlements.Panel
        });
        var userRole = new ApplicationRole(Guid.NewGuid(), "User", new List<string>());

        var adminUser = new ApplicationUser(Guid.NewGuid(), "Admin", "admin@admin.com", "admin123",
            new List<ApplicationRole> { adminRole }, Array.Empty<string>());
        var userUser = new ApplicationUser(Guid.NewGuid(), "User", "user@user.com", "user123",
            new List<ApplicationRole> { adminRole }, Array.Empty<string>());

        _dbContext.Roles.Add(adminRole);
        _dbContext.Roles.Add(userRole);
        _dbContext.Users.Add(adminUser);
        _dbContext.Users.Add(userUser);
        await _dbContext.SaveChangesAsync();
    }
}