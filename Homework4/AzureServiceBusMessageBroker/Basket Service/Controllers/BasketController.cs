using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers;

public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;

    public BasketController(ILogger<BasketController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return Ok();
    }
}
