using ZooApp.Application.Species.Commands;
using ZooApp.Domain.Species;

namespace ZooApp.Application.Species.Implementations;

public class SpeciesCommandServiceImpl : ISpeciesCommandService
{

    private readonly ISpeciesRepository _speciesRepository;

    public SpeciesCommandServiceImpl(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }

    public async Task<Domain.Species.Species> CreateSpeciesAsync(CreateSpeciesCommand command)
    {

        var kingdomEnum = Enum.Parse<Domain.Species.AnimalKingdom>(command.Kingdom, true);

        if (!Enum.IsDefined(typeof(Domain.Species.AnimalKingdom), kingdomEnum))
        {
            throw new ArgumentException($"Invalid kingdom value: {command.Kingdom}");
        }

        var speciesToBeSaved = Domain.Species.Species.CreateNew(
            command.Name,
            command.FeedingIntervalInHours,
            kingdomEnum
        );

        return await _speciesRepository.SaveSpeciesAsync(speciesToBeSaved);
    }
}
