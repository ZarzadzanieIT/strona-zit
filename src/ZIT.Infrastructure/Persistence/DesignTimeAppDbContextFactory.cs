using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ZIT.Infrastructure.Persistence;

public class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../", "ZIT.Web"));
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        configuration.GetDbContextOptions().Invoke(optionsBuilder);

        return new AppDbContext(optionsBuilder.Options);
    }
}