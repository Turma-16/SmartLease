using SmartLease.Models;

public interface IEquipamentos
{
    public Task<Equipamento> CadastrarEquipamento(Equipamento equipamento);

    public Task<IEnumerable<Equipamento>> ListarEquipamento();

}