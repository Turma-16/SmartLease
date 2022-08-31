using System.ComponentModel.DataAnnotations;
using SmartLease.Models;

namespace SmartLease.DTOs;

public class EquipamentoDTO
{
    public int IdEquipamento {get;set;}

    //botar limite para strings na validação https://docs.microsoft.com/pt-br/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
    [StringLength(100, ErrorMessage = "O nome do equipamento deve ter no máximo 100 caracteres.")]
    public string Nome {get;set;} = null!;

    [StringLength(200, ErrorMessage = "A descrição do equipamento deve ter no máximo 200 caracteres.")]
    public string Descricao {get;set;} = null!;

    [Range(0,999.99)]

    public decimal CustoDiario {get;set;}

    public static EquipamentoDTO DeEntidadeParaDTO(Equipamento equipamento) {
      return new EquipamentoDTO {
        IdEquipamento = equipamento.Id,
        Nome = equipamento.Nome,
        Descricao = equipamento.Descricao,
        CustoDiario = equipamento.CustoDiario
      };
    }
    
}