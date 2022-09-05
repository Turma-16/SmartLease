using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;
 
public interface IFuncionarioProjetoRepo {

    Task<List<FuncionarioProjeto>> listarFuncionariosEmProjeto (int projetoId, bool allFuncionarios);
    Task<FuncionarioProjeto> cadastrar(FuncionarioProjeto funcionarioProjeto);
    Task<FuncionarioProjeto> desativar(FuncionarioProjeto funcionarioProjeto);
    Task<FuncionarioProjeto?> buscarFuncionarioEmProjeto(int projetoId, int funcionarioId);
    Task<FuncionarioProjeto?> buscaUltimoFuncionarioProjeto(int idFuncionario);
    Task<List<Funcionario>> buscaFuncionariosAtivosEmData(int projetoId, DateTime data);
    Task<DateTime?> buscaPrimeiraDataTrabalhadaProjeto(int projetoId);
    Task<DateTime?> buscaUltimaDataTrabalhadaProjeto(int projetoId);
    Task<bool> ehProjetoAtivo(int projetoId);
}