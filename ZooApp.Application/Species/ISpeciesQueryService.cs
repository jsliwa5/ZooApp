using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Species;

namespace ZooApp.Application.Species;

public interface ISpeciesQueryService
{

    Task<List<Domain.Species.Species>> GetAllSpeciesAsync();
    Task<Domain.Species.Species> GetSpeciesByIdAsync(int id);
}
