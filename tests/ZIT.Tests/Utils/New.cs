using System.Collections.Generic;
using ZIT.Core.Entities;

namespace ZIT.Tests.Utils;

public class New
{
    public static ApplicationUser ApplicationUser(string? name = null, string? email = null, string? password = null,
        ICollection<ApplicationRole>? roles = null, ICollection<string>? entitlements = null)
        => new(name, email, password, roles, entitlements);

    public static ApplicationRole ApplicationRole(string? name = null, ICollection<string>? entitlements = null,
        ApplicationRole? parentRole = null)
        => new(name, entitlements, parentRole);

    public static Department Department(string? name = null, string? imageAddress = null, string? description = null,
        Member? coordinator = null)
        => new(name, imageAddress, description, coordinator);

    public static Member Member(string? name = null, string? surname = null, string? photoAddress = null,
        string? description = null, Department? department = null)
        => new(name, surname, photoAddress, description, department);

    public static Post Post(string? title = null, string? slugTitle = null, string? summary = null,
        string? content = null, string[]? tags = null)
        => new(title, slugTitle, summary, content, tags);

    public static SliderElement SliderElement(string? title = null, string? imageAddress = null,
        string? description = null)
        => new(title, imageAddress, description);
}