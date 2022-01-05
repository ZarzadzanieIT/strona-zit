using ZIT.Core.Entities;
using ZIT.Core.Services;

namespace ZIT.Infrastructure.Services;

public class SliderElementsService : ISliderElementsService
{
    private readonly List<SliderElement> _sliderElements;

    public SliderElementsService(List<SliderElement> sliderElements)
    {
        _sliderElements = sliderElements;
    }

    public bool IsListEmpty()
    {
        return _sliderElements.Count == 0;
    }
}