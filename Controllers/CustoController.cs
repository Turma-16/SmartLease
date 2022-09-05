using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SmartLease.Repositories;
using SmartLease.Services;
using SmartLease.DTOs;

namespace smartlease.Controllers;

[EnableCors("LiberaGeral")]
[ApiController]
[Route("[controller]")]

public class CustoController : ControllerBase
{

    private readonly ILogger<CustoController> _logger;
    private readonly ICustosService _ICustosService;
    private readonly IProjetoRepo _IProjetoRepo;

    public CustoController(ILogger<CustoController> logger, IEquipamentoRepo repo, ICustosService custosService,  IProjetoRepo projetoRepo)
    {
        _logger = logger;
        _IProjetoRepo = projetoRepo;
        _ICustosService = custosService;
    }

    [HttpPost("Listar")] // GET .../Custo/Listar
    public async Task<ActionResult<List<CustoMensalDTO>>> CustosProjeto([FromBody] DatasDTO request)
    {   

        var projeto = await _IProjetoRepo.buscarPorID(request.ProjetoId);
        
        if(projeto == null) return BadRequest("Projeto não existe na base de dados");

        return await _ICustosService.custosMensaisDeProjeto(projeto, request.DataInicio, request.DataFinal);
    }

    [HttpGet("ListarPeriodoAtivo")] // GET .../Custo/ListarPeriodoAtivo
    public async Task<ActionResult<List<CustoMensalDTO>>> CustosProjeto(int projetoId)
    {
        var projeto = await _IProjetoRepo.buscarPorID(projetoId);

        if(projeto == null) return BadRequest("Projeto não existe na base de dados");

        return await _ICustosService.custosMensaisDeProjetoEmPeriodoAtivo(projeto);
    }
}
