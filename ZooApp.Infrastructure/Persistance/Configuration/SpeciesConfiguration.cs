using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Species;

namespace ZooApp.Infrastructure.Persistance.Configuration;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("Species");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50); 

        builder.Property(s => s.FeedingIntervalInHours)
            .IsRequired();

        builder.Property(s => s.Kingdom)
            .HasConversion<string>()
            .HasMaxLength(20);

    }
}
