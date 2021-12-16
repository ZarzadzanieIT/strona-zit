using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class Department : AuditableEntity
{
    public string? Name { get; set; }
    public Guid? CoordinatorId { get; set; }
    public Member? Coordinator { get; set; }
    public string? ImageAddress { get; set; }
    public string? Description { get; set; }

    protected Department()
    {
    }

    public Department(string name, string imageAddress, string description, Member? coordinator = null)
    {
        Name = name;
        CoordinatorId = coordinator?.Id;
        Coordinator = coordinator;
        ImageAddress = imageAddress;
        Description = description;
    }
}