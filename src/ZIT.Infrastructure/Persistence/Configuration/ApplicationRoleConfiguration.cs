using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZIT.Core.Entities;

namespace ZIT.Infrastructure.Persistence.Configuration;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Entitlements)
            .HasConversion(
                v => string.Join(',', v!),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        builder.HasMany<ApplicationUser>(e => e.UsersInRole)
            .WithMany(e => e.Roles);
        builder.HasOne<ApplicationRole>(x => x.ParentRole)
            .WithMany(x => x.ChildrenRoles)
            .HasForeignKey(x => x.ParentRoleId);
    }
}