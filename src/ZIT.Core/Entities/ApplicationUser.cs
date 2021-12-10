using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class ApplicationUser : AuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<string> Entitlements { get; set; }
    public ICollection<ApplicationRole> Roles { get; set; }

    public IEnumerable<string> AllEntitlements
        => Roles.SelectMany(x => x.AllEntitlements)
            .Concat(Entitlements)
            .Distinct();

    protected ApplicationUser()
    {
    }

    public ApplicationUser(string name, string email, string password, ICollection<ApplicationRole> roles,
        ICollection<string> entitlements)
    {
        Name = name;
        Email = email;
        Password = password;
        Entitlements = entitlements;
        Roles = roles;
    }

    public void AddRole(ApplicationRole role)
    {
        if (Roles.Contains(role))
        {
            return;
        }

        Roles.Add(role);
        role.AddUserToRole(this);
    }
}