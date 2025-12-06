using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.ZooKeeper.Tasks;

public class ZooKeeperConfiguration : IEntityTypeConfiguration<ZooKeeper>
{
    public void Configure(EntityTypeBuilder<ZooKeeper> builder)
    {
        builder.HasKey(z => z.Id);
        builder.Property(z => z.FirstName).IsRequired();
        builder.Property(z => z.LastName).IsRequired();

        // Relacja do zadań
        // Musimy powiedzieć EF, że kolekcja zawiera AbstractTask, a nie ITask
        builder.HasMany<AbstractTask>("_tasks") // Odwołujemy się do pola prywatnego!
            .WithOne()
            .HasForeignKey(t => t.ZooKeeperId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("_tasks").AutoInclude(); // Opcjonalne: zawsze ładuj zadania
    }
}