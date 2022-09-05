using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;

public class FuncionarioProjetoRepo : IFuncionarioProjetoRepo {

private readonly SmartLeaseContext _contexto;

public FuncionarioProjetoRepo(SmartLeaseContext contexto) {
    _contexto = contexto;
}

 public async Task<List<FuncionarioProjeto>> listarFuncionariosEmProjeto (int projetoId, bool allFuncionarios) {
    
    var funcionarioprojeto = await _contexto._funcionarios_projetos
    .Where(funcproj => funcproj.ProjetoId == projetoId && (allFuncionarios || funcproj.Ativo)).Include("Funcionario").ToListAsync();
    return funcionarioprojeto;
 }

 public async Task<FuncionarioProjeto> cadastrar(FuncionarioProjeto funcionarioProjeto) {
    await _contexto._funcionarios_projetos.AddAsync(funcionarioProjeto);
    await _contexto.SaveChangesAsync();
    return funcionarioProjeto;
 }

public async Task<List<Funcionario>> buscaFuncionariosAtivosEmData(int projetoId, DateTime data) {
   var funcionariosProjeto = await _contexto._funcionarios_projetos
                              .Include("Funcionario")
                              .Where(funcproj =>
                                 funcproj.ProjetoId == projetoId
                                 && data.Month >= funcproj.DataEntrada.Month
                                 && (!funcproj.DataSaida.HasValue || data.Month <= funcproj.DataSaida.Value.Month)
                              ).ToListAsync();

   return funcionariosProjeto.Select(funcproj => funcproj.Funcionario).ToList();
}

 public async Task<FuncionarioProjeto> desativar(FuncionarioProjeto funcionarioProjeto) {

   await _contexto.SaveChangesAsync();
   return funcionarioProjeto;
    
 }
public async Task<DateTime?> buscaPrimeiraDataTrabalhadaProjeto(int projetoId) {
   var funcionarioProjeto = await _contexto._funcionarios_projetos
      .Where(funcproj => funcproj.ProjetoId == projetoId)
      .OrderBy(funcproj => funcproj.DataEntrada)
      .FirstOrDefaultAsync();

   return funcionarioProjeto?.DataEntrada;
 }

 public async Task<DateTime?> buscaUltimaDataTrabalhadaProjeto(int projetoId) {
   var funcionarioProjeto = await _contexto._funcionarios_projetos
      .Where(funcproj => funcproj.ProjetoId == projetoId)
      .OrderByDescending(funcproj => funcproj.DataSaida)
      .FirstOrDefaultAsync();

   return funcionarioProjeto?.DataSaida;
 }

  public async Task<bool> ehProjetoAtivo(int projetoId) {
   var funcionariosProjeto = await _contexto._funcionarios_projetos
                           .Where(funcproj => 
                              funcproj.ProjetoId == projetoId && 
                              funcproj.DataSaida == null).ToListAsync();

   return funcionariosProjeto.Any();
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
      return funcProj.ElementAtOrDefault(0);
   }
   return null;
 }

 public async Task<List<FuncionarioProjeto>> listarFuncionariosEmProjetoAtivo (int projetoId) {

   var funcionarioprojeto = await _contexto._funcionarios_projetos
    .Where(funcproj => funcproj.ProjetoId == projetoId && funcproj.Ativo == true).Include("Funcionario").ToListAsync();
    return funcionarioprojeto;

 }

}