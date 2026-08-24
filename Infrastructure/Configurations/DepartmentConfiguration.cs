using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Reflection.Metadata.BlobBuilder;


    public class DepartmentConfiguration
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        { 
    
        builder.HasKey(d => d.DepartmentID);
        builder.Property(d => d.DepartmentName)
            .IsRequired()
            .HasMaxLength(100);


        }


    }

