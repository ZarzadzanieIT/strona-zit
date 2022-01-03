using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ZIT.Infrastructure.Persistence;
using ZIT.Tests.Utils;

namespace ZIT.Tests.Database;

public class MemberTests : IDisposable
{
    private readonly SqliteDbContextFixture _sqlDbContextFixture;
    private readonly AppDbContext _context;
    public MemberTests()
    {
        _sqlDbContextFixture = new SqliteDbContextFixture();
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
        _sqlDbContextFixture.Dispose();
    }
}