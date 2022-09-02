using Microsoft.EntityFrameworkCore;
using SmartLease.Models;

namespace SmartLease.Repositories;

public class SmartLeaseContext : DbContext 
{
    public DbSet<Funcionario> _funcionarios {get;set;} = null!;
    public DbSet<Equipamento> _equipamentos {get;set;} = null!;
    public DbSet<Projeto> _projetos {get;set;} = null!;
    public DbSet<FuncionarioProjeto> _funcionarios_projetos {get;set;} = null!;
    public DbSet<Reserva> _reservas {get;set;} = null!;
    public SmartLeaseContext()
    {
        
    }

    public SmartLeaseContext(DbContextOptions<SmartLeaseContext> opcoes) 
    :base(opcoes)
    {

    }

}