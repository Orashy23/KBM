using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
{
    public void Configure(EntityTypeBuilder<Industry> builder)
    {
        // Primary Key
        builder.HasKey(i => i.IndustryID);

        // Properties
        builder.Property(i => i.IndustryName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(i => i.CreatedDate)
               .IsRequired();

        builder.Property(i => i.ModifiedDate)
               .IsRequired(false);
    }
}