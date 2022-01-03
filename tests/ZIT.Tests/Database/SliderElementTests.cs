using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ZIT.Infrastructure.Persistence;
using ZIT.Tests.Utils;

namespace ZIT.Tests.Database;

public class SliderElementTests : IDisposable
{
    private readonly SqliteDbContextFixture _sqlDbContextFixture;
    private readonly AppDbContext _context;
    public SliderElementTests()
    {
        _sqlDbContextFixture = new SqliteDbContextFixture();
        _context = _sqlDbContextFixture.AppDbContext;
    }

    [Fact]
    public async Task Add_WhenPositionIsEqualToZero_ShouldAddToDatabase()
    {

        // Arrange
        var sliderElement = New.SliderElement(position: 0);


        // Act
        _context.SliderElements.Add(sliderElement);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedSliderElement = await _context.SliderElements.FirstAsync();
        retrievedSliderElement.Should().BeEquivalentTo(sliderElement, options => options.Excluding(x => x.Id));
    }

    [Fact]
    public async Task Add_MultipleElements_WhenDifferentPositions_ShouldAddToDatabase()
    {
        // Arrange
        var sliderElements = Enumerable.Range(0, 5).Select(x => New.SliderElement(title: $"Title{x}", position: x)).ToList();


        // Act
        _context.SliderElements.AddRange(sliderElements);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedSliderElements = await _context.SliderElements.ToListAsync();
        retrievedSliderElements.Should().BeEquivalentTo(sliderElements, options => options.Excluding(x => x.Id));
    }

    [Fact]
    public async Task Add_WhenPositionIsLessThanZero_ShouldThrowDbUpdateException()
    {
        // Arrange
        var sliderElement = New.SliderElement(position: -1);


        Func<Task> func = async () =>
        {
            // Act
            _context.SliderElements.Add(sliderElement);
            await _context.SaveChangesAsync();
        };

        // Assert
        await func.Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Add_WhenPositionIsAlreadyOccupied_ShouldThrowDbUpdateException()
    {
        // Arrange
        var sliderElement = New.SliderElement(position: 0);
        _context.SliderElements.Add(sliderElement);
        await _context.SaveChangesAsync();
        var secondSliderElement = New.SliderElement(position: 0);

        Func<Task> func = async () =>
        {
            // Act
            _context.SliderElements.Add(secondSliderElement);
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