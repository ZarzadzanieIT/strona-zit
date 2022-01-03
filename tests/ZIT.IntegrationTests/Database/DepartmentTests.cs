using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ZIT.Infrastructure.Persistence;
using ZIT.IntegrationTests.Utils;
using ZIT.Tests.Utils;

namespace ZIT.IntegrationTests.Database;

[Collection("SqliteCollection")]
public class DepartmentTests : IClassFixture<SqliteDbContextFixture>, IDisposable
{
    private readonly SqliteDbContextFixture _sqlDbContextFixture;
    private readonly AppDbContext _context;
    public DepartmentTests(SqliteDbContextFixture sqlDbContextFixture)
    {
        _sqlDbContextFixture = sqlDbContextFixture;
        _context = _sqlDbContextFixture.AppDbContext;
    }

    [Fact]
    public async Task Add_ShouldAddDepartmentToDatabase()
    {
        // Arrange
        var department = New.Department();

        // Act
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedDepartment = await _context.Departments.FirstAsync();
        retrievedDepartment.Should().BeEquivalentTo(department, options => options.Excluding(x => x.Id));
    }

    [Fact]
    public async Task Add_ShouldGenerateIdForDepartment()
    {
        // Arrange
        var department = New.Department();

        // Act
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedDepartment = await _context.Departments.FirstAsync();
        retrievedDepartment.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task SetCoordinator_WhenDepartmentAlreadyExists_ShouldAddMemberAsDepartmentCoordinator()
    {
        // Arrange
        var member = New.Member();
        var department = New.Department();
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        // Act
        department = await _context.Departments.FirstAsync();
        department.Coordinator = member;
        await _context.SaveChangesAsync();

        // Assert
        var retrievedDepartment = await _context.Departments.Include(x => x.Coordinator).FirstAsync();
        retrievedDepartment.Coordinator.Should().BeEquivalentTo(member, options => options.Excluding(x => x.Id));
    }

    [Fact]
    public async Task Get_WhenDepartmentHasCoordinatorAndCoordinatorIsIncluded_ShouldReturnDepartmentWithCoordinator()
    {
        // Arrange
        var member = New.Member();
        var department = New.Department();
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        department = await _context.Departments.FirstAsync();
        department.Coordinator = member;
        await _context.SaveChangesAsync();

        // Act
        var retrievedDepartment = await _context.Departments.Include(x => x.Coordinator).FirstAsync();

        // Assert
        retrievedDepartment.Coordinator.Should().BeEquivalentTo(member, options => options.Excluding(x => x.Id));
    }

    public void Dispose()
    {
        _sqlDbContextFixture.ResetDatabase();
    }
}