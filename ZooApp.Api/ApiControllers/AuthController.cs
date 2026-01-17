using Microsoft.AspNetCore.Mvc;
using ZooApp.Application.Auth;
using ZooApp.Application.Auth.Commands;
using ZooApp.Application.Auth.Results;

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
    public async Task<AuthResult> Login(LoginCommand command)
    {
        return await _authService.LoginAsync(command.Email, command.Password); 
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

    [HttpPost]
    [Route("register/manager")]
    public async Task<IActionResult> RegisterManager(RegisterManagerCommand command)
    {
        await _authService.RegisterManagerAsync(command.Email, command.Password, command.FirstName, command.LastName);
        return Ok();
    }


}
