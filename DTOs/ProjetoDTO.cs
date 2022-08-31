using System.ComponentModel.DataAnnotations;
using SmartLease.Models;


namespace SmartLease.DTOs;

public class ProjetoDTO
{
    public int ProjetoId {get;set;}

    [StringLength(100, ErrorMessage = "O nome do projeto deve ter no máximo 100 caracteres.")]
    public string Nome {get;set;} = null!;

    public static ProjetoDTO DeEntidadeParaDTO(Projeto projeto) {
      return new ProjetoDTO {
        ProjetoId = projeto.Id,
        Nome = projeto.Nome
      };
    }
}