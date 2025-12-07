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

        builder.HasMany(z => z.Tasks) // Mapujemy publiczną właściwość!
            .WithOne()
            .HasForeignKey(t => t.ZooKeeperId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(z => z.Tasks)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}