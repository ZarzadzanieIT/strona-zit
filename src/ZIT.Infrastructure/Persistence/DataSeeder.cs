using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        var adminRole = new ApplicationRole(
            "Admin",
            new List<string>
            {
                Auth.Entitlements.Panel,
                Auth.Entitlements.Users.All
            });
        var userRole = new ApplicationRole(
            "User", 
            new List<string>
            {
                Auth.Entitlements.Default
            },
            adminRole);

        var adminUser = new ApplicationUser(
            "Admin",
            "admin@admin.com",
            PasswordHelper.CalculateHash("admin123"),
            new List<ApplicationRole>(),
            Array.Empty<string>());
        var moderatorUser = new ApplicationUser(
            "Moderator",
            "mod@mod.com",
            PasswordHelper.CalculateHash("mod123"),
            new List<ApplicationRole>(),
            new List<string> { Auth.Entitlements.Panel, Auth.Entitlements.Users.Read });
        var userUser = new ApplicationUser(
            "User",
            "user@user.com",
            PasswordHelper.CalculateHash("user123"),
            new List<ApplicationRole>(),
            Array.Empty<string>());

        _dbContext.Roles.Add(adminRole);
        _dbContext.Roles.Add(userRole);
        await _dbContext.SaveChangesAsync();
        var roles = await _dbContext.Roles.ToListAsync();

        userRole = roles.Single(x => x.Name == "User");
        adminRole = roles.Single(x => x.Name == "Admin");
        userUser.AddRole(userRole);
        moderatorUser.AddRole(userRole);
        adminUser.AddRole(adminRole);

        _dbContext.Users.Add(adminUser);
        _dbContext.Users.Add(moderatorUser);
        _dbContext.Users.Add(userUser);
        await _dbContext.SaveChangesAsync();
    }
}