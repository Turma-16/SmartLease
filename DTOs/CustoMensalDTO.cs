using System.ComponentModel.DataAnnotations;
using SmartLease.Models;
using SmartLease.Services;

namespace SmartLease.DTOs;

public class CustoMensalDTO
{
    public string DataStr {get;set;} = null!;
    public DateTime Data {get;set;}
    public decimal Custo {get;set;}
    public List<FuncionarioDTO> Funcionarios {get;set;} = null!;
    public List<ReservaDTO> Reservas {get;set;} = null!;

    public static CustoMensalDTO DeEntidadeParaDTO(DateTime data, string dataStr, decimal custo, List<Funcionario>? funcionarios, List<Reserva>? reservas) {
      var listFuncionarios = funcionarios != null 
                            ? funcionarios.Select(FuncionarioDTO.DeEntidadeParaDTO).ToList() 
                            : new List<FuncionarioDTO>();

      var listReservas = reservas != null 
                      ? reservas.Select(ReservaDTO.DeEntidadeParaDTO).ToList() 
                      : new List<ReservaDTO>();

      return new CustoMensalDTO {
        DataStr = dataStr,
        Data = data,
        Custo = custo,
        Funcionarios = listFuncionarios,
        Reservas = listReservas
      };
    }


}