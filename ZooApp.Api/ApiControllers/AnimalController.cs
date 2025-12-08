using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Animals;
using ZooApp.Application.Animals.Commands;
using ZooApp.Application.Animals.Results;
using ZooApp.Domain.Animal;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/animals")]
public class AnimalController
{

    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalCommandService _animalCommandService;
    private readonly IAnimalQueryService _animalQueryService;

    public AnimalController(IAnimalRepository animalRepository, IAnimalCommandService animalCommandService, IAnimalQueryService animalQueryService)
    {
        _animalRepository = animalRepository;
        _animalCommandService = animalCommandService;
        _animalQueryService = animalQueryService;
    }

    [HttpGet]
    public List<AnimalResult> GetAllAnimals()
    {
        return _animalQueryService.getAllAnimals();
    }

    [HttpPost]
    public AnimalResult AddAnimal([FromBody] CreateAnimalCommand command) { 
        
        return _animalCommandService.CreateAnimal(command);
    }

    [HttpGet]
    [Route("{id}")]
    public AnimalResult GetAnimalById([FromRoute] int id)
    {
        return _animalQueryService.getAnimalById(id);
    }
}
