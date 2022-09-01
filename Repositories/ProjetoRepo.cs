using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;

public class ProjetoRepo : IProjetoRepo {

private readonly SmartLeaseContext _contexto;

public ProjetoRepo(SmartLeaseContext contexto) {
       _contexto = contexto;
}

 public async Task<List<Projeto>> listarTodos () {
    
    var projeto = await _contexto._projetos.ToListAsync();
    return projeto;
 }

 public async Task<Projeto> cadastrar(Projeto projeto) {
    await _contexto._projetos.AddAsync(projeto);
    await _contexto.SaveChangesAsync();
    return projeto;
 }  
  public async Task<Projeto?> buscarPorID(int idProjeto) {

    var projeto = await _contexto._projetos.FindAsync(idProjeto);
    return projeto;
 }
}