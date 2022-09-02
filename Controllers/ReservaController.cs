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
    public async Task<IEnumerable<ReservaDTO>> Listar()
    {
        var reservas = await _reservaRepo.ListarReservas();
        return reservas.Select(ReservaDTO.DeEntidadeParaDTO).ToList();
    }

    [HttpPost("Cadastrar")] // POST .../Reserva/Cadastrar
    public async Task<bool> Cadastrar(ReservaDTO reservaDTO)
    {    

        Funcionario? func = await _funcionarioRepo.buscarPorID(reservaDTO.FuncionarioId);
        
        if(func == null){
            return false;
        }
       
        Equipamento? equip = await _equipamentoRepo.BuscarPorId(reservaDTO.EquipamentoId);

        if(equip == null){
            return false;
        }
            Reserva res = new Reserva(){
                Id = reservaDTO.ReservaId,
                DataReserva = reservaDTO.DataReserva,
                CustoReserva = reservaDTO.CustoReserva,
                EquipamentoId = reservaDTO.EquipamentoId,
                FuncionarioId = reservaDTO.FuncionarioId,
                Funcionario = func,
                Equipamento = equip,
            };
    
        return await _reservaRepo.CadastrarReserva(res);
    }

    [HttpDelete("Cancelar")] // DELETE .../Reserva/Cancelar
    public async Task<bool> Cancelar(ReservaDTO reservaParaCancelar)
    {   
        Reserva? reservaCadastrada = await _reservaRepo.BuscarPorId(reservaParaCancelar.ReservaId);
        
        if (reservaCadastrada == null) return false;        
        
        return await _reservaRepo.CancelarReserva(reservaCadastrada);
    }
}
