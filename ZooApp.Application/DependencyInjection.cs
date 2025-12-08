using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using ZooApp.Application.Animals;
using ZooApp.Application.Animals.Implementations;

namespace ZooApp.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<ISpeciesCommandService, SpeciesCommandServiceImpl>();
        //services.AddScoped<ISpeciesQueryService, SpeciesQueryServiceImpl>();
        services.AddScoped<IAnimalCommandService, AnimalCommandServiceImpl>();
        services.AddScoped<IAnimalQueryService, AnimalQueryServiceImpl>();

        return services;
    }

}
