using SmartLease.Models;
namespace SmartLease.Repositories;
public interface IEquipamentoRepo
{
    public Task<bool> CadastrarEquipamento(Equipamento equipamento);
    public Task<Equipamento?> BuscarPorId(int idFuncionario);
    public Task<IEnumerable<Equipamento>> ListarEquipamentos();
    public Task<bool> AlterarCusto(int idEquipamento, decimal novoCusto);
    
}