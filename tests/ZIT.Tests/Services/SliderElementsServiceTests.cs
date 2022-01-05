using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using ZIT.Core.Entities;
using ZIT.Infrastructure.Services;
using ZIT.Tests.Utils;

namespace ZIT.Tests.Services;

public class SliderElementsServiceTests
{
    private readonly SliderElement _sliderOne;
    private readonly SliderElement _sliderTwo;
    private readonly List<SliderElement> _sliders;

    public SliderElementsServiceTests()
    {
        _sliderOne = New.SliderElement(position: 0);
        _sliderTwo = New.SliderElement(position: 1);
        _sliders = new List<SliderElement>
        {
            _sliderOne,
            _sliderTwo
        };
    }

    [Fact]
    public void IsListEmpty_WhenServiceInitializedWithEmptyList_ShouldReturnTrue()
    {
        // Arrange
        var service = new SliderElementsService(Array.Empty<SliderElement>().ToList());

        // Act
        var result = service.IsListEmpty();

        // Assert
        result.Should().BeTrue();
    }
}