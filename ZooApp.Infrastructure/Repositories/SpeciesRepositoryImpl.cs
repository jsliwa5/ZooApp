using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Species;
using ZooApp.Infrastructure.Persistence;

namespace ZooApp.Infrastructure.Repositories;

public class SpeciesRepositoryImpl : ISpeciesRepository
{

    private readonly ZooDbContext _dbContext;

    public SpeciesRepositoryImpl(ZooDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Species>> GetAllSpeciesAsync()
    {
        return await _dbContext.Species.ToListAsync();
    }

    public async Task<Species> SaveSpeciesAsync(Species species)
    {
        _dbContext.Species.Add(species);
        await _dbContext.SaveChangesAsync();
        return species;
    }
}
