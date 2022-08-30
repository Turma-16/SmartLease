using Microsoft.AspNetCore.Mvc;

namespace smartlease.Controllers;

[ApiController]
[Route("[controller]")]
public class EquipamentoController : ControllerBase
{

    private readonly ILogger<EquipamentoController> _logger;

    public EquipamentoController(ILogger<EquipamentoController> logger)
    {
        _logger = logger;
    }

}
