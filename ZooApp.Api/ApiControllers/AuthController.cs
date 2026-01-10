using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Auth;
using ZooApp.Application.Auth.Commands;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        return await _authService.LoginAsync(command.Email, command.Password) is { } result
            ? Ok(result)
            : Unauthorized();
    }

    [HttpPost]
    [Route("register/zookeeper")]
    public async Task<IActionResult> RegisterZooKeeper(RegisterZooKeeperCommand command)
    {
        await _authService.RegisterZooKeeperAsync(command.Email, command.Password, command.FirstName, command.LastName, command.HoursLimit);
        return Ok();
    }

    [HttpPost]
    [Route("register/vet")]
    public async Task<IActionResult> RegisterVet(RegisterVetCommand command)
    {
        await _authService.RegisterVetAsync(command.Email, command.Password, command.FirstName, command.LastName, command.HoursLimit);
        return Ok();
    }


}
