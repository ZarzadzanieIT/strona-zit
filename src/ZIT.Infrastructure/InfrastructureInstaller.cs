using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZIT.Infrastructure.Persistence;

namespace ZIT.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(builder =>
            builder.UseInMemoryDatabase("ZIT"));

        services.AddTransient<DataSeeder>();

        return services;
    }
}