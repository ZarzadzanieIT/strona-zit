using Microsoft.EntityFrameworkCore;
using ZIT.Core.Entities;
using ZIT.Infrastructure.Authorization;
using ZIT.Infrastructure.Persistence.Configuration;

namespace ZIT.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

#pragma warning disable CS8618
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
#pragma warning restore CS8618
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}