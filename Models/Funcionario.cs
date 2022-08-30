namespace SmartLease.Models;
using System.ComponentModel.DataAnnotations;
public class Funcionario {
    public int Id {get;set;}    
    
    public string Nome {get;set;} = null!;
    public string Matricula {get;set;} = null!;
    
    public decimal Salario {get;set;}

    //public ICollection<Reserva> Reservas {get;set;}


}