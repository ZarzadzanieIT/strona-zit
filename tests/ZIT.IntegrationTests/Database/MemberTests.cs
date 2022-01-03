using System;
using System.Data.Common;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ZIT.Infrastructure.Persistence;
using ZIT.IntegrationTests.Utils;
using ZIT.Tests.Utils;

namespace ZIT.IntegrationTests.Database;

[Collection("SqliteCollection")]
public class MemberTests : IClassFixture<SqliteDbContextFixture>, IDisposable
{
    private readonly SqliteDbContextFixture _sqlDbContextFixture;
    private readonly AppDbContext _context;
    public MemberTests(SqliteDbContextFixture sqlDbContextFixture)
    {
        _sqlDbContextFixture = sqlDbContextFixture;
        _context = _sqlDbContextFixture.AppDbContext;
    }

    [Fact]
    public async Task Add_WhenDepartmentIsNotSet_ShouldThrowDbUpdateException()
    {
        // Arrange
        var member = New.Member();

        Func<Task> func = async () =>
        {
            // Act
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        };

        // Assert
        await func.Should().ThrowAsync<DbUpdateException>();
    }

    public void Dispose()
    {
        _sqlDbContextFixture.ResetDatabase();
    }
}