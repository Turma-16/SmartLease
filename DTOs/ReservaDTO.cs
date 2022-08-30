using System.ComponentModel.DataAnnotations;
using SmartLease.Models;
namespace SmartLease.DTOs;

public class ReservaDTO
{
    public int IdReserva {get;set;}
    [Range(1,100)]
    public int? IdFuncionario {get;set;}
    [Range(1,100)]
    public int? IdEquipamento {get;set;}
    [Range(0,999.99)]
    public decimal CustoReserva {get;set;}

    public static ReservaDTO DeEntidadeParaDTO(Reserva reserva) {
      return new ReservaDTO {
        IdReserva = reserva.Id,
        IdFuncionario = reserva.IdFuncionario,
        IdEquipamento = reserva.IdEquipamento,
      };
    }
}