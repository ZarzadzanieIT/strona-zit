﻿using System.Net.Http.Headers;
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
        var databaseOptions = configuration.Get<DatabaseOptions>();

        services.AddDbContext<AppDbContext>(builder =>
        {
            switch (databaseOptions.DatabaseProvider)
            {
                case DatabaseProvider.InMemory:
                    builder.UseInMemoryDatabase("ZIT");
                    break;
                case DatabaseProvider.Sqlite:
                    builder.UseSqlite(configuration.GetConnectionString("Sqlite"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseOptions), databaseOptions,
                        "Unknown database provider");
            }
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService>(provider => (provider.GetRequiredService<IUserService>() as IAuthService)!);

        var entitlementsProvider = new EntitlementsProvider();
        services.AddSingleton<IEntitlementsProvider>(entitlementsProvider);
        services.AddTransient<DataSeeder>();

        return services;
    }
}