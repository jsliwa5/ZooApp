using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Animal;

namespace ZooApp.Infrastructure.Persistance.Configuration;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Species Value Object Configuration
        builder.OwnsOne(a => a.Species, speciesBuilder =>
        {
            speciesBuilder.Property(s => s.Name)
                .HasColumnName("SpeciesName") 
                .IsRequired();

            speciesBuilder.Property(s => s.FeedingIntervalInHours)
                .HasColumnName("FeedingInterval");

            speciesBuilder.Property(s => s.Kingdom)
                .HasColumnName("Kingdom")
                .HasConversion<string>();
        });
    }
}
