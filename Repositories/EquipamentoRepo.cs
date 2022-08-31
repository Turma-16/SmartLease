using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLease.Models;

using SmartLease.Repositories;

namespace SmartLease.Controllers;

public class EquipamentoRepo : Controller
{
    private readonly SmartLeaseContext _contexto;
    
    public EquipamentoRepo(SmartLeaseContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> AlterarCusto(decimal novoCustoDiario, int idEquipamento)
    {   
        Equipamento? equipamentoEncontrado = await _contexto._equipamentos.FindAsync(idEquipamento);
        
        if(equipamentoEncontrado == null)
        {
            return false;
        }

        equipamentoEncontrado.CustoDiario = novoCustoDiario; 
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