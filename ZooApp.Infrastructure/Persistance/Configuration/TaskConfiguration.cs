using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Infrastructure.Persistance.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<AbstractTask>
{
    public void Configure(EntityTypeBuilder<AbstractTask> builder)
    {
     
        builder.ToTable("ZooKeeperTasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Description).IsRequired();
        builder.Property(t => t.ZooKeeperId).IsRequired();

        builder.HasDiscriminator<string>("TaskType")
            .HasValue<AnimalRelatedTask>("AnimalRelated")
            .HasValue<OtherTask>("Other");
    }
}

public class AnimalRelatedTaskConfiguration : IEntityTypeConfiguration<AnimalRelatedTask>
{
    public void Configure(EntityTypeBuilder<AnimalRelatedTask> builder)
    {

        builder.Property(t => t.AnimalId).HasColumnName("TargetAnimalId");
    }
}