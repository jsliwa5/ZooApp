using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Species;

namespace ZooApp.Infrastructure.Persistance.Configuration;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.LastTimeFed)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(a => a.LastHealthCheck)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.HasOne<Species>()
               .WithMany()             
               .HasForeignKey(a => a.SpeciesId) 
               .IsRequired();

    }
}
