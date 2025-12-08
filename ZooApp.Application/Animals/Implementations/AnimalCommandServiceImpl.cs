using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Application.Animals;
using ZooApp.Application.Animals.Commands;
using ZooApp.Application.Animals.Results;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Species;

namespace ZooApp.Application.Animals.Implementations;

public class AnimalCommandServiceImpl : IAnimalCommandService
{

    private readonly IAnimalRepository _animalRepository;
    private readonly ISpeciesRepository _speciesRepository;
    public AnimalCommandServiceImpl(IAnimalRepository animalRepository, ISpeciesRepository speciesRepository)
    {
        _animalRepository = animalRepository;
        _speciesRepository = speciesRepository;
    }


    public AnimalResult CreateAnimal(CreateAnimalCommand command)
    {
        if(_speciesRepository.ExistsByIdAsync(command.SpeciesId).Result)
        {
            throw new ArgumentException($"Species with id {command.SpeciesId} does not exist.");
        }

        var savedAnimal = _animalRepository.Save(
                Animal.CreateNew(
                    command.Name,
                    command.SpeciesId
                )
            ).Result;

        return new AnimalResult(
            savedAnimal.Id,
            savedAnimal.Name,
            savedAnimal.LastTimeFed,
            savedAnimal.LastHealthCheck,
            savedAnimal.SpeciesId
            );
            
    }

    public async Task DeleteByIdAsync(int animalId)
    {
        var animalToBeDeleted = _animalRepository.GetById(animalId).Result;
        if (animalToBeDeleted == null)
        {
            throw new ArgumentException($"Animal with id {animalId} does not exist.");
        }

        await _animalRepository.Delete(animalToBeDeleted);
    }

    public async Task FeedAnimalAsync(int animalId)
    {
        var animalToBeFed = _animalRepository.GetById(animalId).Result;
        if (animalToBeFed == null)
        {
            throw new ArgumentException($"Animal with id {animalId} does not exist.");
        }
        animalToBeFed.Feed(DateTime.UtcNow);
        await _animalRepository.Save(animalToBeFed);
    }

    public async Task PerformHealthCheckAsync(int animalId)
    {
        var animalToHaveItsHealthChecked = _animalRepository.GetById(animalId).Result;
        if (animalToHaveItsHealthChecked == null)
        {
            throw new ArgumentException($"Animal with id {animalId} does not exist.");
        }
        animalToHaveItsHealthChecked.PerformHealthCheck(DateTime.UtcNow);
        await _animalRepository.Save(animalToHaveItsHealthChecked);
    }
}
