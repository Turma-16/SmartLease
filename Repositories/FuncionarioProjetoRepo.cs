using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;

public class FuncionarioProjetoRepo : IFuncionarioProjetoRepo {

private readonly SmartLeaseContext _contexto;

public FuncionarioProjetoRepo(SmartLeaseContext contexto) {
    _contexto = contexto;
}

 public async Task<List<FuncionarioProjeto>> listarFuncionariosEmProjeto (int projetoId) {
    
    var funcionarioprojeto = await _contexto._funcionarios_projetos
    .Where(funcproj => funcproj.ProjetoId == projetoId).Include("Funcionario").ToListAsync();
    return funcionarioprojeto;
 }

 public async Task<FuncionarioProjeto> cadastrar(FuncionarioProjeto funcionarioProjeto) {
    await _contexto._funcionarios_projetos.AddAsync(funcionarioProjeto);
    await _contexto.SaveChangesAsync();
    return funcionarioProjeto;
 } 
 public async Task<Funcionario?> buscarPorID(int idFuncionario) {

    var funcionario = await _contexto._funcionarios.FindAsync(idFuncionario);
    return funcionario;
 } 

 public async Task<FuncionarioProjeto> desativar(FuncionarioProjeto funcionarioProjeto) {

   await _contexto.SaveChangesAsync();
   return funcionarioProjeto;
    
 }

  public async Task<FuncionarioProjeto?> buscarFuncionarioEmProjeto(int idProjeto,int idFuncionario) {

    var funcionarioProjeto = await _contexto._funcionarios_projetos
                              .Where(funcproj => funcproj.Ativo == true && 
                              funcproj.FuncionarioId == idFuncionario && 
                              funcproj.ProjetoId == idProjeto)
                              .FirstOrDefaultAsync();
    return funcionarioProjeto; 
 } 

public async Task<FuncionarioProjeto?> buscaUltimoFuncionarioProjeto(int idFuncionario) {

   var funcionario = await _contexto._funcionarios
                     .Where(func => func.Id == idFuncionario)
                     .Include("FuncionarioProjetos")
                     .FirstOrDefaultAsync();

   if (funcionario!.FuncionarioProjetos != null) { // apesar desse warning, conferimos antes se o func existe.

      var funcProj = funcionario.FuncionarioProjetos.OrderByDescending(funcproc => funcproc.DataSaida);
      return funcProj.ElementAt(0);
   }
   return null;
 }

}