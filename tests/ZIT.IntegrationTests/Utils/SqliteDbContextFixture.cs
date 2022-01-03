using System;
using Microsoft.EntityFrameworkCore;
using ZIT.Infrastructure.Options;
using ZIT.Infrastructure.Persistence;

namespace ZIT.IntegrationTests.Utils;

public class SqliteDbContextFixture : IDisposable
{
    public AppDbContext AppDbContext { get; }
    public SqliteDbContextFixture()
    {
        AppDbContext = GetTestDbContext();
    }

    internal void ResetDatabase()
    {
        ResetDatabase(AppDbContext);
    }

    private static void ResetDatabase(DbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
    internal static AppDbContext GetTestDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var optionsBuilderProvider = Infrastructure.InfrastructureInstaller.GetDbContextOptions(DatabaseProvider.SQLite,
            _ => $"DataSource={Guid.NewGuid()}.db;");

        optionsBuilderProvider.Invoke(optionsBuilder);
        var context = new AppDbContext(optionsBuilder.Options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }

    public void Dispose()
    {
        AppDbContext.Database.EnsureDeleted();
        AppDbContext.Dispose();
    }
}