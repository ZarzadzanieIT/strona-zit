using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZIT.Core.Services;
using ZIT.Infrastructure.Options;
using ZIT.Infrastructure.Persistence;
using ZIT.Infrastructure.Services;

namespace ZIT.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(configuration.GetDbContextOptions());

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService>(provider => (provider.GetRequiredService<IUserService>() as IAuthService)!);

        var entitlementsProvider = new EntitlementsProvider();
        services.AddSingleton<IEntitlementsProvider>(entitlementsProvider);
        services.AddTransient<DataSeeder>();

        return services;
    }

    internal static Action<DbContextOptionsBuilder> GetDbContextOptions(this IConfiguration configuration)
    {
        var databaseOptions = configuration.Get<DatabaseOptions>();

        return GetDbContextOptions(databaseOptions.DatabaseProvider, configuration.GetConnectionString);
    }

    internal static Action<DbContextOptionsBuilder> GetDbContextOptions(DatabaseProvider databaseProvider, Func<string, string> connectionStringProvider)
    {
        return builder =>
        {
            switch (databaseProvider)
            {
                case DatabaseProvider.InMemory:
                    builder.UseInMemoryDatabase("ZIT");
                    break;
                case DatabaseProvider.SQLite:
                    builder.UseSqlite(connectionStringProvider("SQLite"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseProvider), databaseProvider,
                        "Unknown database provider");
            }
        };
    }
}