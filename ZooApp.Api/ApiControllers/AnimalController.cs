using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Animals.Commands;
using ZooApp.Domain.Animal;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/animals")]
public class AnimalController
{

    private readonly IAnimalRepository _animalRepository;

    public AnimalController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public List<Animal> GetAllAnimals()
    {
        return _animalRepository.GetAllAnimals().Result;
    }

    [HttpPost]
    public Animal AddAnimal([FromBody] CreateAnimalCommand command) { 
        
        return _animalRepository.Save(Animal.CreateNew(command.Name, command.SpeciesId)).Result;
    }
}
