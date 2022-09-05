using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;

public interface IFuncionarioRepo {
    Task<List<Funcionario>> listarTodos();

    Task<List<Funcionario>> listarTodosSemProjeto();

    Task<Funcionario> cadastrar(Funcionario funcionario);
    Task<Funcionario?> buscarPorID(int idFuncionario);
}