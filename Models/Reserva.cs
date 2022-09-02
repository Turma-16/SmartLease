
namespace SmartLease.Models;
public class Reserva {

    public int Id { get; set; }
    
    public decimal CustoReserva { get; set; }
    public DateTime DataReserva { get; set; }

    public int FuncionarioId { get; set; } 
    public Funcionario Funcionario { get; set; } = null!;

    public int EquipamentoId { get; set; }    
    public Equipamento Equipamento { get; set; } = null!;
}