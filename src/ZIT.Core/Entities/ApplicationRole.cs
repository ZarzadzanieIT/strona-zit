using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class ApplicationRole : AuditableEntity
{
    public string? Name { get; set; }
    public ICollection<string>? Entitlements { get; set; }
    public ICollection<ApplicationUser>? UsersInRole { get; set; }
    public Guid? ParentRoleId { get; set; }
    public ApplicationRole? ParentRole { get; set; }
    public ICollection<ApplicationRole>? ChildrenRoles { get; set; }

    public IEnumerable<string> AllEntitlements
        => (ChildrenRoles ?? Array.Empty<ApplicationRole>())
            .SelectMany(x => x.AllEntitlements)
            .Concat(Entitlements ?? Array.Empty<string>())
            .Distinct();

    protected ApplicationRole()
    {
        
    }

    public ApplicationRole(string? name, ICollection<string>? entitlements, ApplicationRole? parentRole = null)
    {
        Name = name;
        Entitlements = entitlements;
        ParentRoleId = parentRole?.Id;
        ParentRole = parentRole;
        UsersInRole = new List<ApplicationUser>();
        ChildrenRoles = new List<ApplicationRole>();
    }

    public void AddUserToRole(ApplicationUser user)
    {
        if (UsersInRole == null)
        {
            throw new NullReferenceException($"Can not add user to role when {nameof(UsersInRole)} is null");
        }

        if (UsersInRole.Contains(user))
        {
            return;
        }

        UsersInRole.Add(user);
        ChildrenRoles?.ToList()
            .ForEach(x => 
                x.AddUserToRole(user));
        user.AddRole(this);
    }
}