using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class ApplicationUser : AuditableEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public ICollection<string>? Entitlements { get; set; }
    public ICollection<ApplicationRole>? Roles { get; set; }

    public IEnumerable<string> AllEntitlements
        => (Roles ?? Array.Empty<ApplicationRole>())
            .SelectMany(x => x.AllEntitlements)
            .Concat(Entitlements ?? Array.Empty<string>())
            .Distinct();

    protected ApplicationUser()
    {
    }

    public ApplicationUser(string? name, string? email, string? password, ICollection<ApplicationRole>? roles,
        ICollection<string>? entitlements)
    {
        Name = name;
        Email = email;
        Password = password;
        Entitlements = entitlements;
        Roles = roles;
    }

    public void AddRole(ApplicationRole role)
    {
        if (Roles == null)
        {
            throw new NullReferenceException($"Can not role when {nameof(Roles)} is null");
        }

        if (Roles.Contains(role))
        {
            return;
        }

        Roles.Add(role);
        role.AddUserToRole(this);
    }
}