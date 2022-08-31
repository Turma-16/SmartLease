using Microsoft.AspNetCore.Mvc;
using SmartLease.Repositories;
using SmartLease.Models;

namespace smartlease.Controllers;

[ApiController]
[Route("[controller]")]

public class EquipamentoController : ControllerBase
{

    private readonly ILogger<EquipamentoController> _logger;
    private readonly IEquipamentoRepo _equipamentoRepo;

    public EquipamentoController(ILogger<EquipamentoController> logger, IEquipamentoRepo repo )
    {
        _logger = logger;
        _equipamentoRepo = repo;
    }
    
    [HttpGet("")] 

    public async Task<IEnumerable<Equipamento>> ListarEquipamentos()
    {
        return await _equipamentoRepo.ListarEquipamentos();
    }
}
