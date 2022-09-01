using SmartLease.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartLease.Repositories;

public interface IProjetoRepo {

    Task<List<Projeto>> listarTodos();
    Task<Projeto> cadastrar(Projeto projeto);

    Task<Projeto?> buscarPorID(int idProjeto);
}