using Microsoft.EntityFrameworkCore;
using SmartLease.Models;
using System.Linq;
using SmartLease.DTOs;

namespace SmartLease.Repositories;

public class RelatorioRepo : IRelatorioRepo
{
    private readonly SmartLeaseContext _contexto;
    
    public RelatorioRepo(SmartLeaseContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<Double> consultarCustoTotal(int projetoId, DateTime mesAnoDesejado)
    {   
        // Retorna custo total do salario
        // var SomaSalarios = from FP in _contexto._funcionarios_projetos join F in _contexto._funcionarios on FP.FuncionarioId equals F.Id 
        // where FP.Ativo == true && FP.ProjetoId == projetoId
        // group F by new {FP.ProjetoId} into grouped select new {
        //     ProjetoId = grouped.Key,
        //     SomaSalario = grouped.Sum(func => func.Salario)
        // };

        var SomaReservas = from FP in _contexto._funcionarios_projetos join F in _contexto._funcionarios on FP.FuncionarioId equals F.Id join R in _contexto._reservas
        on F.Id equals R.FuncionarioId 
        where FP.Ativo == true || (R.DataReserva.Month >= FP.DataEntrada.Month)
        where (R.DataReserva.Month == FP.DataEntrada.Month &&  R.DataReserva.Year == FP.DataEntrada.Year)
        && (R.DataReserva.Month == mesAnoDesejado.Month &&  R.DataReserva.Year == mesAnoDesejado.Year)

        where FP.Ativo == true && FP.ProjetoId == projetoId
        group F by new {FP.ProjetoId} into grouped select new {
            ProjetoId = grouped.Key,
            SomaSalario = grouped.Sum(func => func.Salario)
        };

        // Reserva? reservaNoMesmoDia = _contexto._reservas.FirstOrDefault( 
        //     r => (r.DataReserva == reserva.DataReserva && 
        //             r.EquipamentoId == reserva.EquipamentoId));

        // if(reservaNoMesmoDia != null || reserva.DataReserva < DateTime.Now.Date) { return false; }

        try{
            await _contexto._reservas.AddAsync(reserva); 
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

    public async Task<IEnumerable<Reserva>> ListarReservas()
    {
        return await _contexto._reservas.ToListAsync();
    }

    public async Task<bool> CancelarReserva(Reserva reservaParaCancelar)
    {
        //Reserva reservaParaCancelar = await _contexto._reservas.FirstAsync(r => r.Id == idReservaParaCancelar);
        if (reservaParaCancelar == null) { return false; }
        
        _contexto._reservas.Remove(reservaParaCancelar);
        await _contexto.SaveChangesAsync();
        
        return true;
    }

    public async Task<Reserva?> BuscarPorId(int idReserva)
    {
        return await _contexto._reservas.FindAsync(idReserva);
    }
   
}