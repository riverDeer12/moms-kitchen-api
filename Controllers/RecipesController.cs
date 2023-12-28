using Microsoft.AspNetCore.Mvc;
using MomsKitchen.Entities;

namespace MomsKitchen.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController : ControllerBase
{

    private readonly ILogger<RecipesController> _logger;

    public RecipesController(ILogger<RecipesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Recipe> Get()
    {
        return new[] { new Recipe()};
    }
}