using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.DepartmentID);

        builder.Property(d => d.DepartmentName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.CreatedDate).IsRequired();
        builder.Property(d => d.UpdatedDate).IsRequired();
    }
}