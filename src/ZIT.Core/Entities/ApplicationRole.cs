using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class ApplicationRole : AuditableEntity
{
    public string Name { get; set; }
    public ICollection<string> Entitlements { get; set; }

    public ApplicationRole(Guid id, string name)
    {
        Id = id;
        Name = name;
        Entitlements = new List<string>();
    }

    public ApplicationRole(Guid id, string name, ICollection<string> entitlements)
    {
        Id = id;
        Name = name;
        Entitlements = entitlements;
    }
}