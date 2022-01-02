using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class Member : AuditableEntity
{
    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhotoAddress { get; set; }
    public string? Description { get; set; }

    protected Member()
    {
    }

    public Member(string? name, string? surname, string? photoAddress, string? description, Department? department = null)
    {
        DepartmentId = department?.Id;
        Department = department;
        department?.AddMember(this);
        Name = name;
        Surname = surname;
        PhotoAddress = photoAddress;
        Description = description;
    }
}