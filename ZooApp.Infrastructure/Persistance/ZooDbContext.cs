using Microsoft.EntityFrameworkCore;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Species;
using ZooApp.Domain.Vets;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Infrastructure.Persistance;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options) { }

     public DbSet<Animal> Animals { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<Vet> Vets { get; set; }
    //public DbSet<Visit> Visits { get; set; } //disabled to enforce aggregate root
    public DbSet<ZooKeeper> ZooKeepers { get; set; }

    public DbSet<AbstractTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZooDbContext).Assembly);
    }

}