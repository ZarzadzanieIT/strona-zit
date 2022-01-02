using ZIT.Core.Common;

namespace ZIT.Core.Entities;

public class Department : AuditableEntity
{
    private Member? _coordinator;
    public string? Name { get; set; }
    public Guid? CoordinatorId { get; set; }

    public Member? Coordinator
    {
        get => _coordinator;
        set
        {
            _coordinator = value;
            if (value != null)
            {
                value.Department = this;
            }
        }
    }

    public string? ImageAddress { get; set; }
    public string? Description { get; set; }
    public ICollection<Member> Members { get; set; }

    protected Department()
    {
    }

    public Department(string? name, string? imageAddress, string? description, Member? coordinator = null)
    {
        Name = name;
        CoordinatorId = coordinator?.Id;
        Coordinator = coordinator;
        Members = new HashSet<Member>();
        if (coordinator != null)
        {
            AddMember(coordinator);
        }
        ImageAddress = imageAddress;
        Description = description;
    }

    public void AddMember(Member member)
    {
        member.Department = this;
        Members.Add(member);
    }
}