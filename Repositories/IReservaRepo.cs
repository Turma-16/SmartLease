using SmartLease.Models;
using SmartLease.DTOs;

namespace SmartLease.Repositories;

public interface IReservaRepo
{
    public Task<bool> CadastrarReserva(Reserva reserva);
    public Task<Reserva?> BuscarPorId(int idReserva);
    public Task<List<Reserva>?> BuscarPorDataEFuncionario(DateTime data, int idFuncionario);
    public Task<IEnumerable<Reserva>> ListarReservas();
    public Task<bool> CancelarReserva(Reserva reservaParaCancelar);
    
}