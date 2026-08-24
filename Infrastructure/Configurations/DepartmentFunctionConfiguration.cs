using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Reflection.Metadata.BlobBuilder;



namespace Infrastructure.Configurations;

public class DepartmentFunctionConfiguration : IEntityTypeConfiguration<DepartmentFunction>
{
    public void Configure(EntityTypeBuilder<DepartmentFunction> builder)
    {
        // 1. Composite Primary Key
        builder.HasKey(df => new { df.FunctionID, df.DepartmentID });

        builder.HasOne(df => df.Function)
               .WithMany(f => f.DepartmentFunctions)
               .HasForeignKey(df => df.FunctionID)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(df => df.Department)
               .WithMany(d => d.DepartmentFunctions)
               .HasForeignKey(df => df.DepartmentID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
