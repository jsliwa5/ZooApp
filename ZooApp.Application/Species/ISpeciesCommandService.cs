using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Application.Species.Commands;
using ZooApp.Domain.Species;

namespace ZooApp.Application.Species;

public interface ISpeciesCommandService
{
    Task<Domain.Species.Species> CreateSpeciesAsync(CreateSpeciesCommand command);

}
