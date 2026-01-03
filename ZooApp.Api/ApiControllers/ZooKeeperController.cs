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

    [HttpPost]
    public async Task<ZooKeeperResult> AddZooKeeper([FromBody] CreateZooKeeperCommand command)
    {
        return await _zooKeeperCommandService.addZooKeeperAsync(command);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ZooKeeperResult> GetZooKeeperById([FromRoute] int id)
    {
        return await _zooKeeperQueryService.GetZooKeeperByIdAsync(id);
    }

}
