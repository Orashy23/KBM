using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class FunctionConfiguration
    {
        public void Configure(EntityTypeBuilder<Function> builder)
        {

            builder.HasKey(d => d.FunctionID);
            builder.Property(d => d.FunctionName)
                .IsRequired()
                .HasMaxLength(100);


        }


    }
}
