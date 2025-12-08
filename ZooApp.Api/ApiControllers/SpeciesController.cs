using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Species.Commands;
using ZooApp.Domain.Species;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/species")]
public class SpeciesController
{

    private readonly ISpeciesRepository _speciesRepository;

    public SpeciesController(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }

    [HttpPost]
    public Species CreateSpecies([FromBody] CreateSpeciesCommand command)
    {
        Enum.TryParse<AnimalKingdom>(command.Kingdom, ignoreCase: true, out var kingdomEnum);
        var species = Species.CreateNew(command.Name, command.FeedingIntervalInHours, kingdomEnum);
        
        return _speciesRepository.SaveSpeciesAsync(species).Result;
    }

}
