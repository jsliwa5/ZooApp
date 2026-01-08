using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Vets;
using ZooApp.Application.Vets.Commands;
using ZooApp.Application.Vets.Results;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/vets")]
public class VetController
{

    private readonly IVetCommandService _vetCommandService;
    private readonly IVetQueryService _vetQueryService;

    public VetController(IVetCommandService vetCommandService, IVetQueryService vetQueryService)
    {
        _vetCommandService = vetCommandService;
        _vetQueryService = vetQueryService;
    }

    [HttpGet]
    public async Task<List<VetResult>> GetVets()
    {
        return await _vetQueryService.GetAllVetsAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<VetResult> GetVetById([FromRoute] int id)
    {
        return await _vetQueryService.GetVetByIdAsync(id);
    }

    [HttpPost]
    public async Task<VetResult> AddVet([FromBody] CreateVetCommand command)
    {
        return await _vetCommandService.AddVetAsync(command);
    }

    [HttpPost]
    [Route("{vetId}/schedule-visit")]
    public async Task ScheduleVisit([FromRoute] int vetId, [FromBody] ScheduleVisitCommand command)
    {
        await _vetCommandService.ScheduleVisitAsync(vetId, command);
    }


}
