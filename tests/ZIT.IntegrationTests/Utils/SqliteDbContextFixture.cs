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
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var optionsBuilderProvider = Infrastructure.InfrastructureInstaller.GetDbContextOptions(DatabaseProvider.SQLite,
            _ => "DataSource=tests.db;");

        optionsBuilderProvider.Invoke(optionsBuilder);
        AppDbContext = new AppDbContext(optionsBuilder.Options);

        ResetDatabase();
    }

    internal void ResetDatabase()
    {
        AppDbContext.Database.EnsureDeleted();
        AppDbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        AppDbContext.Database.EnsureDeleted();
        AppDbContext.Dispose();
    }
}