using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZIT.Core.Entities;

namespace ZIT.Infrastructure.Persistence.Configuration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne<Member>(e => e.Coordinator).WithOne(e => e.Department).HasForeignKey<Member>(e => e.DepartmentId);
    }
}