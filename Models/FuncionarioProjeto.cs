using System.ComponentModel.DataAnnotations;
namespace SmartLease.Models;

/* 
vira uma classe pq tem atributos próprios (ver diagrama do banco de dados). 
temos ela por enquanto, só será necessária se resolvermos tornar mais complexa o cálculo de 
custos de projeto levando em conta um funcionário que sai de um projeto no meio do mês. 
senão, pode excluir e botar a informação de projeto na entidade de funcionario.
- turno da noite
*/
public class FuncionarioProjeto {

    public int Id {get;set;}
    public int FuncionarioId {get;set;}
    public Funcionario Funcionario {get;set;} = null!;
    public int ProjetoId {get;set;}
    public Projeto Projeto {get;set;} = null!;
    public bool Ativo {get;set;}
    public DateTime? DataSaida {get;set;}
    public DateTime DataEntrada {get;set;}
}