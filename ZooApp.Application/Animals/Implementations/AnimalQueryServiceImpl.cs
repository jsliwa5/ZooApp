using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Application.Animals;
using ZooApp.Application.Animals.Results;
using ZooApp.Domain.Animal;

namespace ZooApp.Application.Animals.Implementations;

public class AnimalQueryServiceImpl : IAnimalQueryService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalQueryServiceImpl(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public List<AnimalResult> getAllAnimals()
    {
        var allAnimals = _animalRepository.GetAllAnimals().Result;

        return allAnimals.ConvertAll(animal => new AnimalResult(
            animal.Id,
            animal.Name,
            animal.LastTimeFed,
            animal.LastHealthCheck,
            animal.SpeciesId
            ));
    }

    public AnimalResult getAnimalById(int id)
    {
        var searchedAnimal = _animalRepository.GetById(id).Result;

        if(searchedAnimal is null)
        {
            throw new Exception("Animal with given Id is not found");
        }

        return new AnimalResult(
            searchedAnimal.Id,
            searchedAnimal.Name,
            searchedAnimal.LastTimeFed,
            searchedAnimal.LastHealthCheck,
            searchedAnimal.SpeciesId
            );

    }


}
