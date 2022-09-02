using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;
 
public interface IFuncionarioProjetoRepo {

    Task<List<FuncionarioProjeto>> listarFuncionariosEmProjeto (int projetoId);
    Task<FuncionarioProjeto> cadastrar(FuncionarioProjeto funcionarioProjeto);
    Task<FuncionarioProjeto> desativar(FuncionarioProjeto funcionarioProjeto);
    Task<FuncionarioProjeto?> buscarFuncionarioEmProjeto(int projetoId, int funcionarioId);
    
    Task<FuncionarioProjeto?> buscaUltimoFuncionarioProjeto(int idFuncionario);
}