using Microsoft.EntityFrameworkCore;
// using ZooApp.Domain.Entities; 

namespace ZooApp.Infrastructure.Persistence;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options) { }

    // public DbSet<Animal> Animals { get; set; }
}