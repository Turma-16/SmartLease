using System.ComponentModel.DataAnnotations;
using SmartLease.Models;
using SmartLease.Services;

namespace SmartLease.DTOs;

public class CustoMensalDTO
{
    public string Data {get;set;}
    public decimal Custo {get;set;}

    public static CustoMensalDTO DeEntidadeParaDTO(string data, decimal custo) {
      return new CustoMensalDTO {
        Data = data,
        Custo = custo
      };
    }


}