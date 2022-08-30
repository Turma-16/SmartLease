using Microsoft.AspNetCore.Mvc;

namespace smartlease.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjetoController : ControllerBase
{

    private readonly ILogger<ProjetoController> _logger;

    public ProjetoController(ILogger<ProjetoController> logger)
    {
        _logger = logger;
    }

}
