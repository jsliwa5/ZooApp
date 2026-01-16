using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.ZooKeepers;
using ZooApp.Application.ZooKeepers.Commands;
using ZooApp.Application.ZooKeepers.Results;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/zookeepers")]
public class ZooKeeperController
{

    private readonly IZooKeeperQueryService _zooKeeperQueryService;
    private readonly IZooKeeperCommandService _zooKeeperCommandService;

    public ZooKeeperController(IZooKeeperQueryService zooKeeperQueryService, IZooKeeperCommandService zooKeeperCommandService)
    {
        _zooKeeperQueryService = zooKeeperQueryService;
        _zooKeeperCommandService = zooKeeperCommandService;
    }

    //[HttpPost]
    //public async Task<ZooKeeperResult> AddZooKeeper([FromBody] CreateZooKeeperCommand command)
    //{
    //    return await _zooKeeperCommandService.addZooKeeperAsync(command);
    //} //disabled since now zookeepers are created by registering

    [HttpGet]
    [Route("{id}")]
    public async Task<ZooKeeperResult> GetZooKeeperById([FromRoute] int id)
    {
        return await _zooKeeperQueryService.GetZooKeeperByIdAsync(id);
    }

    [HttpPost]
    [Route("tasks/auto")]
    public async Task CreateAndAsignTaskAutomatically(AsignTaskCommand command)
    {
        await _zooKeeperCommandService.AsignTaskAutomatically(command);
    }

    [HttpGet]
    [Route("{id}/tasks")]
    public async Task<List<TaskResult>> GetTasksForZooKeeper(
        [FromRoute] int id,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to
        )
    {
        return await _zooKeeperQueryService.GetTaskForZooKeeperAsync(id, from, to);
    }
}
