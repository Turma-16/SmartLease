using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;

public class FuncionarioRepo : IFuncionarioRepo {

   private readonly SmartLeaseContext _contexto;

   public FuncionarioRepo(SmartLeaseContext contexto) {
      _contexto = contexto;
   }

   public async Task<List<Funcionario>> listarTodos () {
    
    var funcionarios = await _contexto._funcionarios.ToListAsync();
    return funcionarios;
   }

   public async Task<Funcionario> cadastrar(Funcionario funcionario) {
      await _contexto._funcionarios.AddAsync(funcionario);
      await _contexto.SaveChangesAsync();
      return funcionario;
   }

   public async Task<Funcionario?> buscarPorID(int idFuncionario) {
      var funcionario = await _contexto._funcionarios.FindAsync(idFuncionario);
      return funcionario;
   }
}