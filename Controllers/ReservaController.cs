using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SmartLease.DTOs;
using SmartLease.Repositories;
using SmartLease.Models;

namespace smartlease.Controllers;

[EnableCors("LiberaGeral")]
[ApiController]
[Route("[controller]")]
public class ReservaController : ControllerBase
{

    private readonly ILogger<ReservaController> _logger;
    private readonly IReservaRepo _reservaRepo;
    private readonly IFuncionarioRepo _funcionarioRepo;
    private readonly IEquipamentoRepo _equipamentoRepo;

    public ReservaController(ILogger<ReservaController> logger, IReservaRepo reservaRepo, IFuncionarioRepo funcionarioRepo, IEquipamentoRepo equipamentoRepo)
    {
        _logger = logger;

        _reservaRepo = reservaRepo;
        _funcionarioRepo = funcionarioRepo;
        _equipamentoRepo = equipamentoRepo;
    }

    [HttpGet("Listar")] // GET .../Reserva/Listar
    public async Task<IEnumerable<Reserva>> Listar()
    {
        var reservas = await _reservaRepo.ListarReservas();
        // return reservas.Select(ReservaDTO.DeEntidadeParaDTO).ToList();
        return reservas;
    }


    [HttpPost("Cadastrar")] // POST .../Reserva/Cadastrar
    public async Task<string> Cadastrar(ReservaDTO reservaDTO)
    {   
        Funcionario? func = await _funcionarioRepo.buscarPorID(reservaDTO.FuncionarioId);
        if(func == null){
            return ("Funcionario não encontrado");
        }
       
        Equipamento? equip = await _equipamentoRepo.BuscarPorId(reservaDTO.EquipamentoId);
        if(equip == null){
            return ("Equipamento não encontrado");
        }

        Reserva res = new Reserva(){
            Id = reservaDTO.ReservaId,
            DataReserva = reservaDTO.DataReserva,
            CustoReserva = reservaDTO.CustoReserva,
            EquipamentoId = reservaDTO.EquipamentoId,
            Equipamento = equip,
            FuncionarioId = reservaDTO.FuncionarioId,
            Funcionario = func,
        };

        bool operacao = await _reservaRepo.CadastrarReserva(res);
        if(!operacao)
        {
            return ("Falha ao reservar");
        }

        return "Reserva realizada com sucesso!";
    }

    public class ObjetoComId
    {
        public int Id {get; set;}
    }

    [HttpPost("Cancelar")] // POST .../Reserva/Cancelar
    public async Task<bool> Cancelar(ObjetoComId reserva)
    {   
        Console.WriteLine(reserva.Id);
        Reserva? reservaCadastrada = await _reservaRepo.BuscarPorId(reserva.Id);
        
        if (reservaCadastrada == null) return false;        
        
        return await _reservaRepo.CancelarReserva(reservaCadastrada);
       
    }
}
