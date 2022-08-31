namespace SmartLease.Models;
public class Funcionario {
    public int Id {get;set;}    
    
    public string Nome {get;set;} = null!;
    
    public decimal Salario {get;set;}

    public ICollection<Reserva>? Reservas {get;set;}
    
    public List<FuncionarioProjeto>? FuncionarioProjetos {get;set;}
}