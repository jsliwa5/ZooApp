using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooApp.Domain.Managers;

namespace ZooApp.Infrastructure.Persistance.Configuration;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(m => m.LastName).IsRequired().HasMaxLength(100);

        builder.Property(m => m.UserId).IsRequired();
        builder.HasIndex(m => m.UserId).IsUnique();
    }
}
