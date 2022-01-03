using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZIT.Core.Entities;

namespace ZIT.Infrastructure.Persistence.Configuration;

public class SliderElementConfiguration : IEntityTypeConfiguration<SliderElement>
{
    public void Configure(EntityTypeBuilder<SliderElement> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Position).IsUnique();
        builder.HasCheckConstraint("POSITION_GREATER_OR_EQUAL_ZERO", "Position >= 0");
    }
}