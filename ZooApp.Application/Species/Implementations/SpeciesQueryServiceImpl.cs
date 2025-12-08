using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Species;

namespace ZooApp.Application.Species.Implementations;

internal class SpeciesQueryServiceImpl : ISpeciesQueryService
{

    private readonly ISpeciesRepository _speciesRepository;
    public SpeciesQueryServiceImpl(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }

    public async Task<List<Domain.Species.Species>> GetAllSpeciesAsync()
    {
        return await _speciesRepository.GetAllSpeciesAsync();
    }

    public async Task<Domain.Species.Species> GetSpeciesByIdAsync(int id)
    {
        return await _speciesRepository.GetByIdAsync(id);
    }
}
