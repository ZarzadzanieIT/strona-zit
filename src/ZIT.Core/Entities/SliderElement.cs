using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class SliderElement : AuditableEntity
{
    public string? Title { get; set; }
    public string? ImageAddress { get; set; }
    public string? Description { get; set; }

    protected SliderElement()
    {
        
    }

    public SliderElement(string? title, string? imageAddress, string? description)
    {
        Title = title;
        ImageAddress = imageAddress;
        Description = description;
    }
}