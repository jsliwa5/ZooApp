using Microsoft.EntityFrameworkCore;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Vet;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.ZooKeeper.Tasks; 

namespace ZooApp.Infrastructure.Persistence;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options) { }

     public DbSet<Animal> Animals { get; set; }
    public DbSet<Vet> Vets { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<ZooKeeper> ZooKeepers { get; set; }
    public DbSet<AnimalRelatedTask> AnimalRelatedTasks { get; set; }
    public DbSet<OtherTask> OtherTasks { get; set; }
}