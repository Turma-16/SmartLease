using Microsoft.AspNetCore.Mvc;

namespace smartlease.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservaController : ControllerBase
{

    private readonly ILogger<ReservaController> _logger;

    public ReservaController(ILogger<ReservaController> logger)
    {
        _logger = logger;
    }

}
