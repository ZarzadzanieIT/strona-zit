using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZIT.Core.Entities;

namespace ZIT.Infrastructure.Persistence.Configuration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne<Member>(e => e.Coordinator).WithOne().HasForeignKey<Department>(e => e.CoordinatorId);
        builder.HasMany<Member>(e => e.Members).WithOne(e => e.Department).HasForeignKey(e => e.DepartmentId);
    }
}