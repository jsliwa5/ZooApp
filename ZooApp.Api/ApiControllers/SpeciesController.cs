using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Species;
using ZooApp.Application.Species.Commands;
using ZooApp.Domain.Species;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/species")]
public class SpeciesController
{

    private readonly ISpeciesCommandService _speciesCommandService;
    private readonly ISpeciesQueryService _speciesQueryService;

    public SpeciesController(ISpeciesCommandService speciesCommandService, ISpeciesQueryService speciesQueryService)
    {
        _speciesCommandService = speciesCommandService;
        _speciesQueryService = speciesQueryService;
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<Species> CreateSpecies([FromBody] CreateSpeciesCommand command)
    {
        return await _speciesCommandService.CreateSpeciesAsync(command);
    }

    [HttpGet]
    [Authorize(Roles = "ZooKeeper, Manager, Vet")]
    public async Task<List<Species>> GetAllSpecies()
    {
        return await _speciesQueryService.GetAllSpeciesAsync();
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize(Roles = "ZooKeeper, Manager, Vet")]
    public async Task<Species> GetSpeciesById([FromQuery] int id)
    {
        return await _speciesQueryService.GetSpeciesByIdAsync(id);
    }


}
