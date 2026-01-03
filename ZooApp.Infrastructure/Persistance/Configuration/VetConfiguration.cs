using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Vets;


namespace ZooApp.Infrastructure.Persistance.Configuration;

public class VetConfiguration : IEntityTypeConfiguration<Vet>
{
    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(v => v.LastName).IsRequired().HasMaxLength(100);

        builder.HasMany(v => v.Visits)
            .WithOne() 
            .HasForeignKey(visit => visit.VetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); 

        builder.Metadata.FindNavigation(nameof(Vet.Visits))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Description).IsRequired();

        builder.Property(v => v.VetId).IsRequired();
    }
}
