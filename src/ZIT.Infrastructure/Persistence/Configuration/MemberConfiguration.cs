using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZIT.Core.Entities;

namespace ZIT.Infrastructure.Persistence.Configuration;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne<Department>(e => e.Department).WithOne(e => e.Coordinator).HasForeignKey<Department>(e => e.CoordinatorId);
    }
}