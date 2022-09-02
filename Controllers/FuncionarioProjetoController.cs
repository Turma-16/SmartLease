using Microsoft.AspNetCore.Mvc;
using SmartLease.DTOs;
using SmartLease.Repositories;
using Microsoft.AspNetCore.Cors;
using SmartLease.Models;
using SmartLease.Services;
using System;
namespace smartlease.Controllers;

[EnableCors("LiberaGeral")]
[ApiController]
[Route("[controller]")]
public class FuncionarioProjetoController : ControllerBase
{

    private readonly ILogger<FuncionarioProjetoController> _logger;
    private readonly IFuncionarioProjetoRepo _IFuncionarioProjetoRepo;
    private readonly IProjetoRepo _IProjetoRepo;
    private readonly IFuncionarioRepo _IFuncionarioRepo;
    private readonly IFuncionarioProjetoService _IFuncionarioProjetoService;

    public FuncionarioProjetoController(ILogger<FuncionarioProjetoController> logger, IFuncionarioProjetoRepo funcionarioProjetoRepo, IProjetoRepo projetoRepo, IFuncionarioRepo funcionarioRepo, IFuncionarioProjetoService funcionarioProjetoService)
    {
        _logger = logger;
        _IFuncionarioProjetoRepo = funcionarioProjetoRepo;
        _IProjetoRepo = projetoRepo;
        _IFuncionarioRepo = funcionarioRepo;
        _IFuncionarioProjetoService = funcionarioProjetoService;
    }

    [HttpGet("Listar")] // GET ..../FuncionarioProjeto/Listar?projetoId=<projetoId>?allFuncionarios=<false> (se allFuncionarios for true, lista tbm os não-ativos)
    public async Task<ActionResult<FuncionariosProjetoResponseDTO>> listarFuncionariosEmProjetoAtivo(int projetoId, bool allFuncionarios = false) {

        var projeto = await _IProjetoRepo.buscarPorID(projetoId);

        if(projeto == null) return BadRequest("Projeto não existe na base de dados");
        
        var funcionariosProjeto = await _IFuncionarioProjetoRepo.listarFuncionariosEmProjeto(projeto.Id, allFuncionarios);

        var funcionarios = funcionariosProjeto.Select(funcionarioProjeto => funcionarioProjeto.Funcionario).ToList();

        return FuncionariosProjetoResponseDTO.DeEntidadeParaDTO(projeto, funcionarios);
    }

    [HttpPut("Desativar")] // GET ..../FuncionarioProjeto/Desativar
    public async Task<ActionResult<FuncionarioProjetoDTO>> desativarFuncionariosEmProjeto(int projetoId, int funcionarioId) {

        //REFATORAR PQ ISSO TA DUPLICADO NA FUNCAO ABAIXO
        var projeto = await _IProjetoRepo.buscarPorID(projetoId);

        if(projeto == null) return BadRequest("Projeto não existe na base de dados");
        
        var funcionarioProjeto = await _IFuncionarioProjetoRepo.buscarFuncionarioEmProjeto(projeto.Id, funcionarioId);

        if(funcionarioProjeto == null) return BadRequest("Funcionário não é ativo no projeto");

        funcionarioProjeto.Ativo = false;
        funcionarioProjeto.DataSaida = DateTime.Now.AddMinutes(5);

        return FuncionarioProjetoDTO.DeEntidadeParaDTO(funcionarioProjeto);
    }

    [HttpPost("Cadastrar")] // POST ..../FuncionarioProjeto/Cadastrar
        public async Task<ActionResult<FuncionarioProjetoDTO>> cadastrar(FuncionarioProjetoDTO funcionarioProjetoDTO) {
        FuncionarioProjeto novoFuncionarioProjeto = new FuncionarioProjeto();

        var funcionario = await _IFuncionarioRepo.buscarPorID(funcionarioProjetoDTO.FuncionarioId);
        if (funcionario == null) return BadRequest("Funcionário não existe na base de dados");

        var projeto = await _IProjetoRepo.buscarPorID(funcionarioProjetoDTO.ProjetoId);
        if (projeto == null) return BadRequest("Projeto não existe na base de dados");

        var novaDataEntrada = DateTime.Now;
        var errorResponse = await _IFuncionarioProjetoService.verificaProjetosDeFuncionario(funcionario, projeto, novaDataEntrada);

        if(errorResponse != null) return BadRequest(errorResponse);

        novoFuncionarioProjeto.FuncionarioId = funcionarioProjetoDTO.FuncionarioId;
        novoFuncionarioProjeto.ProjetoId = funcionarioProjetoDTO.ProjetoId;
        novoFuncionarioProjeto.Ativo = true;
        novoFuncionarioProjeto.DataEntrada = novaDataEntrada;
        novoFuncionarioProjeto.DataSaida = null;
        
        var resposta = await _IFuncionarioProjetoRepo.cadastrar(novoFuncionarioProjeto);
        
        return FuncionarioProjetoDTO.DeEntidadeParaDTO(resposta);        
    }



}
