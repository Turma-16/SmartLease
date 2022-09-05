using System.ComponentModel.DataAnnotations;
using SmartLease.Models;

namespace SmartLease.DTOs;

public class FuncionarioDTO
{
    public int FuncionarioId {get;set;}
    //limite para strings na validação https://docs.microsoft.com/pt-br/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
    [StringLength(100, ErrorMessage = "O nome do funcionário deve ter no máximo 100 caracteres.")]
    public string Nome {get;set;} = null!; 
    [Range(100, 9999.99, ErrorMessage = "O salário do funcionário não pode exceder o valor de 9999.99 .")]
    public decimal Salario {get;set;}

    public static FuncionarioDTO DeEntidadeParaDTO(Funcionario funcionario) {
      return new FuncionarioDTO {
        FuncionarioId = funcionario.Id,
        Nome = funcionario.Nome,
        Salario = funcionario.Salario,
      };
    }
    
}