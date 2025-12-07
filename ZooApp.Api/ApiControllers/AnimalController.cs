using Microsoft.AspNetCore.Mvc;

namespace ZooApp.Api.ApiControllers;

[ApiController]
[Route("api/animal")]
public class AnimalController
{

    [HttpGet]
    public string Get()
    {
        return "Hello from AnimalController";
    }
}
