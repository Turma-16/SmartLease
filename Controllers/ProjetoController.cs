using Microsoft.AspNetCore.Mvc;
using SmartLease.DTOs;
using Microsoft.AspNetCore.Cors;
using SmartLease.Repositories;
using SmartLease.Models;
namespace smartlease.Controllers;

[EnableCors("LiberaGeral")]
[ApiController]
[Route("[controller]")]
public class ProjetoController : ControllerBase
{

    private readonly ILogger<ProjetoController> _logger;
    private readonly IProjetoRepo _IProjetoRepo;

    public ProjetoController(ILogger<ProjetoController> logger, IProjetoRepo projetoRepo)
    {
        _logger = logger;
        _IProjetoRepo = projetoRepo;
    }

    [HttpGet("Listar")] // GET ..../Projeto/Listar
        public async Task<ActionResult<IEnumerable<ProjetoDTO>>> listarTodos() {

        var projetos = await _IProjetoRepo.listarTodos();

        return projetos.Select(ProjetoDTO.DeEntidadeParaDTO).ToList();
    }

    [HttpPost("Cadastrar")] // POST ..../Projeto/Cadastrar
        public async Task<ActionResult<ProjetoDTO>> cadastrar(ProjetoDTO Projeto) {
        Projeto novoProjeto = new Projeto();
        novoProjeto.Nome = Projeto.Nome;
        var resposta = await _IProjetoRepo.cadastrar(novoProjeto);

        return ProjetoDTO.DeEntidadeParaDTO(resposta);
    }

}
