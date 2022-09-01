using Microsoft.AspNetCore.Mvc;
using SmartLease.DTOs;
using SmartLease.Repositories;
using SmartLease.Models;
namespace smartlease.Controllers;

[ApiController]
[Route("[controller]")]
public class FuncionarioProjetoController : ControllerBase
{

    private readonly ILogger<FuncionarioProjetoController> _logger;
    private readonly IFuncionarioProjetoRepo _IFuncionarioProjetoRepo;
    private readonly IProjetoRepo _IProjetoRepo;
    private readonly IFuncionarioRepo _IFuncionarioRepo;

    public FuncionarioProjetoController(ILogger<FuncionarioProjetoController> logger, IFuncionarioProjetoRepo funcionarioProjetoRepo, IProjetoRepo projetoRepo, IFuncionarioRepo funcionarioRepo)
    {
        _logger = logger;
        _IFuncionarioProjetoRepo = funcionarioProjetoRepo;
        _IProjetoRepo = projetoRepo;
        _IFuncionarioRepo = funcionarioRepo;
    }

    [HttpGet("Listar")] // GET ..../FuncionarioProjeto/Listar
    public async Task<ActionResult<FuncionariosProjetoResponseDTO>> listarFuncionariosEmProjeto(int projetoId) {

        var projeto = await _IProjetoRepo.buscarPorID(projetoId);

        if(projeto == null) return BadRequest("Projeto não existe na base de dados");
        
        var funcionariosProjeto = await _IFuncionarioProjetoRepo.listarFuncionariosEmProjeto(projeto.Id);

        var funcionarios = funcionariosProjeto.Select(funcionarioProjeto => funcionarioProjeto.Funcionario).ToList();

        return FuncionariosProjetoResponseDTO.DeEntidadeParaDTO(projeto, funcionarios);
    }

    [HttpPost("Cadastrar")] // POST ..../FuncionarioProjeto/Cadastrar
        public async Task<ActionResult<FuncionarioProjetoDTO>> cadastrar(FuncionarioProjetoDTO funcionarioProjetoDTO) {
        FuncionarioProjeto novoFuncionarioProjeto = new FuncionarioProjeto();

        var funcionario = await _IFuncionarioRepo.buscarPorID(funcionarioProjetoDTO.FuncionarioId);
        if (funcionario == null) return BadRequest("Funcionário não existe na base de dados");
        var projeto = await _IProjetoRepo.buscarPorID(funcionarioProjetoDTO.ProjetoId);
        if(projeto == null) return BadRequest("Projeto não existe na base de dados");
        
        novoFuncionarioProjeto.FuncionarioId = funcionarioProjetoDTO.FuncionarioId;
        novoFuncionarioProjeto.ProjetoId = funcionarioProjetoDTO.ProjetoId;
        novoFuncionarioProjeto.Ativo = true;
        novoFuncionarioProjeto.DataEntrada = funcionarioProjetoDTO.DataEntrada;
        novoFuncionarioProjeto.DataSaida = null;
        var resposta = await _IFuncionarioProjetoRepo.cadastrar(novoFuncionarioProjeto);
        
        return FuncionarioProjetoDTO.DeEntidadeParaDTO(resposta);
        
    }
}
