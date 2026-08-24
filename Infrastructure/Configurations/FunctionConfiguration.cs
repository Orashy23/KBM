using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class FunctionConfiguration : IEntityTypeConfiguration<Function>
{
    public void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.HasKey(f => f.FunctionID);

        builder.Property(f => f.FunctionName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(f => f.CreatedDate).IsRequired();
        builder.Property(f => f.UpdatedDate).IsRequired();
    }
}