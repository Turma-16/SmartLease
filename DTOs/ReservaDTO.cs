using System.ComponentModel.DataAnnotations;
using SmartLease.Models;
namespace SmartLease.DTOs;

public class ReservaDTO
{
    public int ReservaId {get;set;}
    
    public int FuncionarioId {get;set;}
    
    public int EquipamentoId {get;set;}

    [Range(1,999.99)]
    public decimal CustoReserva {get;set;}
    public DateTime DataReserva {get;set;}

    public static ReservaDTO DeEntidadeParaDTO(Reserva reserva) {

      return new ReservaDTO {
        ReservaId = reserva.Id,
        FuncionarioId = reserva.FuncionarioId,
        EquipamentoId = reserva.EquipamentoId,
        CustoReserva = reserva.CustoReserva,
        DataReserva = reserva.DataReserva
      };
    }
}