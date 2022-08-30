
namespace SmartLease.Models;
public class Reserva {

    public int Id { get; set; }
    public decimal CustoReserva { get; set; }
    public DateTime DataReserva { get; set; }
    public int IdFuncionario { get; set; }
    public Funcionario funcionario { get; set; } = null!;
    public int IdEquipamento { get; set; }
    public Equipamento equipamento { get; set; } = null!;
}