using System.ComponentModel.DataAnnotations;
using SmartLease.Models;

namespace SmartLease.DTOs;

// Sendo DTO de resposta, n eh necessário constraints
public class FuncionariosProjetoResponseDTO {

    public ProjetoDTO Projeto {get;set;} = null!;

    public List<FuncionarioDTO> Funcionarios {get;set;} = null!;

    public static FuncionariosProjetoResponseDTO DeEntidadeParaDTO(Projeto projeto, List<Funcionario>? funcionarios) {
      return new FuncionariosProjetoResponseDTO {
        Projeto = ProjetoDTO.DeEntidadeParaDTO(projeto),
        Funcionarios = funcionarios != null ? funcionarios.Select(FuncionarioDTO.DeEntidadeParaDTO).ToList() : new List<FuncionarioDTO>(),
      };
    }    

}