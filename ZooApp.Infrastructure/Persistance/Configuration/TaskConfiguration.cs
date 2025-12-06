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
        // 1. Nazwa tabeli dla całej hierarchii
        builder.ToTable("ZooKeeperTasks");

        // 2. Klucz główny
        builder.HasKey(t => t.Id);

        // 3. Konfiguracja pól wspólnych
        builder.Property(t => t.Description).IsRequired();
        builder.Property(t => t.ZooKeeperId).IsRequired();

        // 4. Konfiguracja TPH (Discriminator)
        // EF utworzy kolumnę "TaskType", która rozróżni klasy
        builder.HasDiscriminator<string>("TaskType")
            .HasValue<AnimalRelatedTask>("AnimalRelated")
            .HasValue<OtherTask>("Other");

        // 5. Konfiguracja pól specyficznych dla AnimalRelatedTask
        // Mimo że konfigurujemy AbstractTask, możemy odnieść się do pól dzieci rzutując builder
    }
}

// Opcjonalnie: Jeśli AnimalRelatedTask miałoby bardzo specyficzną konfigurację,
// można stworzyć osobną klasę: class AnimalRelatedTaskConfiguration : IEntityTypeConfiguration<AnimalRelatedTask>
// Ale przy TPH często wystarczy konfiguracja w klasie bazowej, chyba że chcesz dbać o nullable itp.
public class AnimalRelatedTaskConfiguration : IEntityTypeConfiguration<AnimalRelatedTask>
{
    public void Configure(EntityTypeBuilder<AnimalRelatedTask> builder)
    {
        // W TPH pola klasy pochodnej są domyślnie nullable w bazie, 
        // bo rekordy OtherTask będą miały tu NULL. 
        builder.Property(t => t.AnimalId).HasColumnName("TargetAnimalId");
    }
}