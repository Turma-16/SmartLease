using Microsoft.EntityFrameworkCore;
using SmartLease.Models;

namespace SmartLease.Repositories;

public class EquipamentoRepo : IEquipamentoRepo
{
    private readonly SmartLeaseContext _contexto;
    
    public EquipamentoRepo(SmartLeaseContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> AlterarCusto(int idEquipamento, decimal novoCusto)
    {   
        Equipamento? equipamentoEncontrado = await _contexto._equipamentos.FindAsync(1);
        
        if(equipamentoEncontrado == null)
        {
            Console.WriteLine("Equipamento não encontrado");
            return false;
        }

        equipamentoEncontrado.CustoDiario = novoCusto; 
        _contexto.SaveChanges();

        return true;
    }


    public async Task<bool> CadastrarEquipamento(Equipamento equipamento)
    {
        
        try{
            await _contexto._equipamentos.AddAsync(equipamento); 
            await _contexto.SaveChangesAsync();
            return true;
        }catch(Exception err)
        {
            //<alterar>
            Console.WriteLine(err);
            //<alterar>
            return false;
        }
    }

    public async Task<IEnumerable<Equipamento>> ListarEquipamentos()
    {
        return await _contexto._equipamentos.ToListAsync();
    }
}