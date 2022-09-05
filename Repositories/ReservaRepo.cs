using Microsoft.EntityFrameworkCore;
using SmartLease.Models;
using SmartLease.DTOs;

namespace SmartLease.Repositories;

public class ReservaRepo : IReservaRepo
{
    private readonly SmartLeaseContext _contexto;
    
    public ReservaRepo(SmartLeaseContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> CadastrarReserva(Reserva reserva)
    {   

        Reserva? reservaNoMesmoDia = _contexto._reservas.FirstOrDefault( 
            r => (r.DataReserva == reserva.DataReserva && 
                    r.EquipamentoId == reserva.EquipamentoId));

         if(reservaNoMesmoDia != null || reserva.DataReserva < DateTime.Now.Date) { return false; }

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
    public async Task<List<Reserva>?> BuscarPorDataEFuncionario(DateTime data, int idFuncionario)
    {
        return await _contexto._reservas.Where(r => 
            r.DataReserva == data && r.FuncionarioId == idFuncionario).ToListAsync();    
    }
   
}

