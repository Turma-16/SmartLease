using System.ComponentModel.DataAnnotations;

namespace SmartLease.Models;
public class Equipamento {

    public int Id {get;set;}
    
    public string Nome {get;set;} = null!;
    public string Descricao {get;set;} = null!;
    public decimal CustoDiario {get;set;}

    //public ICollection<Reserva> Reservas {get;set;}
    
    // Criar equipamento
    // Editar o custo do equipamento
    // Listar equipamentos
}