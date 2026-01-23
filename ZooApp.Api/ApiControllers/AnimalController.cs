using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Animals;
using ZooApp.Application.Animals.Commands;
using ZooApp.Application.Animals.Results;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/animals")]
public class AnimalController : ControllerBase
{

    private readonly IAnimalCommandService _animalCommandService;
    private readonly IAnimalQueryService _animalQueryService;

    public AnimalController(IAnimalCommandService animalCommandService, IAnimalQueryService animalQueryService)
    {
        _animalCommandService = animalCommandService;
        _animalQueryService = animalQueryService;
    }


    [HttpGet]
    [Authorize(Roles = "ZooKeeper, Manager, Vet")]
    public List<AnimalResult> GetAllAnimals()
    {
        return _animalQueryService.getAllAnimals();
    }


    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<AnimalResult> AddAnimal([FromBody] CreateAnimalCommand command) { 
        
        return await _animalCommandService.CreateAnimalAsync(command);
    }


    [HttpGet]
    [Authorize(Roles = "ZooKeeper, Manager, Vet")]
    [Route("{id}")]
    public AnimalResult GetAnimalById([FromRoute] int id)
    {
        return _animalQueryService.getAnimalById(id);
    }


    [HttpPost("{id}/feed")]
    [Authorize(Roles = "ZooKeeper")]
    public async Task<IActionResult> Feed([FromRoute] int id)
    {
        await _animalCommandService.FeedAnimalAsync(id);
        return Ok();
    }

    [HttpPost("{id}/healthcheck")]
    [Authorize(Roles = "Vet")]
    public async Task<IActionResult> PerformHealthCheck([FromRoute] int id)
    {
        await _animalCommandService.PerformHealthCheckAsync(id);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteAnimal([FromRoute] int id)
    {
        await _animalCommandService.DeleteByIdAsync(id);
        return Ok();
    }
}
