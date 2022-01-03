using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ZIT.Infrastructure.Persistence;
using ZIT.IntegrationTests.Utils;
using ZIT.Tests.Utils;

namespace ZIT.IntegrationTests.Database;

public class PostTests : IDisposable
{
    private readonly SqliteDbContextFixture _sqlDbContextFixture;
    private readonly AppDbContext _context;

    public PostTests()
    {
        _sqlDbContextFixture = new SqliteDbContextFixture();
        _context = _sqlDbContextFixture.AppDbContext;
    }

    [Fact]
    public async Task Add_ShouldAddPostToDatabase()
    {
        // Arrange
        var post = New.Post();

        // Act
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedPost = await _context.Posts.FirstAsync();
        retrievedPost.Should().BeEquivalentTo(post, options => options.Excluding(x => x.Id));
    }

    [Fact]
    public async Task Add_ShouldGenerateIdForPost()
    {
        // Arrange
        var post = New.Post();

        // Act
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedPost = await _context.Posts.FirstAsync();
        retrievedPost.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Add_WhenTagsAreSet_ShouldSaveTagsProperlyInDatabase()
    {
        // Arrange
        var tags = new[] { "tag1", "tag2", "tag3" };
        var post = New.Post(tags: tags);

        // Act
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedPost = await _context.Posts.FirstAsync();
        retrievedPost.Tags.Should().BeEquivalentTo(tags);
    }

    public void Dispose()
    {
        _sqlDbContextFixture.Dispose();
    }
}