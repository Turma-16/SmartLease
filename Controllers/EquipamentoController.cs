using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SmartLease.Repositories;
using SmartLease.DTOs;
using SmartLease.Models;

namespace smartlease.Controllers;

[EnableCors("LiberaGeral")]
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
    public async Task<IEnumerable<EquipamentoDTO>> Listar()
    {
        var equipamentos = await _equipamentoRepo.ListarEquipamentos();
        return equipamentos.Select(EquipamentoDTO.DeEntidadeParaDTO).ToList();
    }

    [HttpPost("Cadastrar")] // POST .../Equipamento/Cadastrar
    public async Task<bool> Cadastrar(Equipamento equipamento)
    {
        return await _equipamentoRepo.CadastrarEquipamento(equipamento);
    }


    [HttpPost("AlterarCusto")] // POST .../Equipamento/AlterarCusto
    public async Task<bool> AlterarCusto(EquipamentoDTO equipamento)
    {
        Console.WriteLine(equipamento);
        return await _equipamentoRepo.AlterarCusto(equipamento.EquipamentoId, equipamento.CustoDiario);
    }

}
