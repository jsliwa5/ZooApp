using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Species;

public interface ISpeciesRepository
{
    Task<Species> SaveSpeciesAsync(Species species);
    Task <List<Species>> GetAllSpeciesAsync();
}
