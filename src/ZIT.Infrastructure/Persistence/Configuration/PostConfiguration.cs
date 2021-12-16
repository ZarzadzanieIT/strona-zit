using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZIT.Core.Entities;

namespace ZIT.Infrastructure.Persistence.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Tags)
            .HasConversion(
                v => string.Join(',', v!),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
    }
}