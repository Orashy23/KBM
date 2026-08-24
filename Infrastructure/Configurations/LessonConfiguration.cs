using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        // 1. Primary Key
        builder.HasKey(l => l.LessonID);

        // 2. Property Rules & SQL Data Types
        builder.Property(l => l.ProjectName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(l => l.TitleName)
               .IsRequired()
               .HasMaxLength(200);

        // Optional detail fields — explicitly nullable so the columns allow NULL.
        builder.Property(l => l.Description)
               .IsRequired(false)
               .HasMaxLength(500);

        builder.Property(l => l.ValueProposition)
               .IsRequired(false)
               .HasMaxLength(500);

        builder.Property(l => l.TargetAudience)
               .IsRequired(false)
               .HasMaxLength(250);

        builder.Property(l => l.PersonToContact)
               .IsRequired(false)
               .HasMaxLength(150);

        builder.Property(l => l.ImageURL)
               .IsRequired(false)
               .HasMaxLength(500);


        builder.Property(l => l.ModifiedDate)
               .IsRequired();

        // 3. Foreign Key: Function (1 : N)
        builder.HasOne(l => l.Function)
               .WithMany(f => f.Lessons)
               .HasForeignKey(l => l.FunctionID)
               .OnDelete(DeleteBehavior.Restrict);

        // 4. Foreign Key: Department (1 : N)
        builder.HasOne(l => l.Department)
               .WithMany(d => d.Lessons)
               .HasForeignKey(l => l.DepartmentID)
               .OnDelete(DeleteBehavior.Restrict);

        // 5. Foreign Key: Industry (1 : N)
        builder.HasOne(l => l.Industry)
               .WithMany(i => i.Lessons)
               .HasForeignKey(l => l.IndustryID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}