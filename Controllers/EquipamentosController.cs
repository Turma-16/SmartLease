using Microsoft.AspNetCore.Mvc;
<<<<<<< main
=======
using Microsoft.AspNetCore.Cors;
using SmartLease.Repositories;
using SmartLease.DTOs;
using SmartLease.Models;

>>>>>>> local

namespace smartlease.Controllers;
[EnableCors("LiberaGeral")]
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
