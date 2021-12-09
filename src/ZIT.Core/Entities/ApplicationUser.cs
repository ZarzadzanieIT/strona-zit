using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class ApplicationUser : AuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<string> Entitlements { get; set; }
    public ICollection<ApplicationRole> Roles { get; set; }

    public ApplicationUser(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        Entitlements = Array.Empty<string>();
        Roles = Array.Empty<ApplicationRole>();
    }

    public ApplicationUser(Guid id, string name, string email, string password, ICollection<ApplicationRole> roles,
        ICollection<string> entitlements)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Entitlements = entitlements;
        Roles = roles;
    }

    public IEnumerable<string> GetAllEntitlements()
        => Roles.SelectMany(x => x.Entitlements).Concat(Entitlements);
}