using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace smartlease.Controllers;

[EnableCors("LiberaGeral")]
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
