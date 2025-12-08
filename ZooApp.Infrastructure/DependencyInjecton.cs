using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ZooApp.Infrastructure.Repositories;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Vets;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.Species;
using ZooApp.Infrastructure.Persistance;

namespace ZooApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ZooDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped < IAnimalRepository, AnimalRepositoryImpl > ();

        services.AddScoped <IVetRepository, VetRepositoryImpl>();

        services.AddScoped<IZooKeeperRepository, ZooKeeperRepositoryImpl>();

        services.AddScoped<ISpeciesRepository, SpeciesRepositoryImpl>();

        return services;
    }
}