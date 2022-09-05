using System.ComponentModel.DataAnnotations;

namespace SmartLease.DTOs;

public class DatasDTO
{

    public int ProjetoId {get;set;}
    public DateTime DataInicio {get;set;}
    public DateTime DataFinal {get;set;}
    public static DatasDTO DeEntidadeParaDTO(int projetoId, DateTime dataInicio, DateTime dataFinal) {

      return new DatasDTO {
        ProjetoId = projetoId,
        DataInicio = dataInicio,
        DataFinal = dataFinal
      };
    }


}