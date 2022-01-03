using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class Post : AuditableEntity
{
    public string? Title { get; set; }
    public string? SlugTitle { get; set; }
    public string? Summary { get; set; }
    public string? Content { get; set; }
    public string[]? Tags { get; set; }

    protected Post()
    {
    }
    public Post(string? title, string? slugTitle, string? summary, string? content, string[]? tags)
    {
        Title = title;
        SlugTitle = slugTitle;
        Summary = summary;
        Content = content;
        Tags = tags;

    }
}