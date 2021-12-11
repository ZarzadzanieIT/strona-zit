using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZIT.Core.Services;
using ZIT.Infrastructure.Persistence;
using ZIT.Infrastructure.Services;

namespace ZIT.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(builder =>
            builder.UseInMemoryDatabase("ZIT"));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService>(provider => (provider.GetRequiredService<IUserService>() as IAuthService)!);

        services.AddTransient<DataSeeder>();

        return services;
    }
}