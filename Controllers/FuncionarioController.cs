using Microsoft.AspNetCore.Mvc;
using SmartLease.DTOs;
using SmartLease.Repositories;
using SmartLease.Models;
namespace smartlease.Controllers;

[ApiController]
[Route("[controller]")]
public class FuncionarioController : ControllerBase
{

    private readonly ILogger<FuncionarioController> _logger;
    private readonly IFuncionarioRepo _IFuncionarioRepo;

    public FuncionarioController(ILogger<FuncionarioController> logger, IFuncionarioRepo funcionarioRepo)
    {
        _logger = logger;
        _IFuncionarioRepo = funcionarioRepo;
    }

    [HttpGet("Listar")] // GET ..../Funcionario/Listar
        public async Task<ActionResult<IEnumerable<FuncionarioDTO>>> listarTodos() {

        var funcionarios = await _IFuncionarioRepo.listarTodos();

        return funcionarios.Select(FuncionarioDTO.DeEntidadeParaDTO).ToList();
    }

    [HttpPost("Cadastrar")] // POST ..../Funcionario/Cadastrar
        public async Task<ActionResult<FuncionarioDTO>> cadastrar(FuncionarioDTO funcionario) {
        Funcionario novoFuncionario = new Funcionario();
        novoFuncionario.Nome = funcionario.Nome;
        novoFuncionario.Salario = funcionario.Salario;
        var resposta = await _IFuncionarioRepo.cadastrar(novoFuncionario);

        return FuncionarioDTO.DeEntidadeParaDTO(resposta);
    }
}
