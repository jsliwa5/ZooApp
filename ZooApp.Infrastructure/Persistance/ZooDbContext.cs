using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Managers;
using ZooApp.Domain.Species;
using ZooApp.Domain.Vets;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.ZooKeeper.Tasks;
using ZooApp.Infrastructure.Identity;

namespace ZooApp.Infrastructure.Persistance;

public class ZooDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{

    public double GetZooKeeperMonthlyHours(int zooKeeperId, int month, int year)
        => throw new NotSupportedException();
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
    {
    }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<Vet> Vets { get; set; }
    //public DbSet<Visit> Visits { get; set; } //disabled to enforce aggregate root
    public DbSet<ZooKeeper> ZooKeepers { get; set; }
    public DbSet<Manager> Managers { get; set; }

    public DbSet<AbstractTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZooDbContext).Assembly);

        modelBuilder.HasDbFunction(typeof(ZooDbContext).GetMethod(nameof(GetZooKeeperMonthlyHours))!)
               .HasName("fn_get_zookeeper_monthly_hours");
    }

}