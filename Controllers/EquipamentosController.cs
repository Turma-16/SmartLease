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
    
    [HttpGet("Listar")] // GET .../Equipamento/Listar
    public async Task<IEnumerable<Equipamento>> Listar()
    {
        return await _equipamentoRepo.ListarEquipamentos();
    }

    [HttpPost("Cadastrar")] // POST .../Equipamento/Cadastrar
    public async Task<bool> Cadastrar(Equipamento equipamento)
    {
        return await _equipamentoRepo.CadastrarEquipamento(equipamento);
    }


    [HttpPost("AlterarCusto")] // POST .../Equipamento/AlterarCusto
    public async Task<bool> AlterarCusto(Equipamento equipamento)
    {
        return await _equipamentoRepo.AlterarCusto(equipamento.Id, equipamento.CustoDiario);
    }

}
