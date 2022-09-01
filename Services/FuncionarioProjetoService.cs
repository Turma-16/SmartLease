namespace SmartLease.Services;
public class FuncionarioProjetoService : IFuncionarioProjetoService {

    private readonly ILogger<FuncionarioProjetoService> _logger;

    public FuncionarioProjetoService(ILogger<FuncionarioProjetoService> logger)
    {
        _logger = logger;
    }

    public bool dataEntradaValida(DateTime? ultimaDataSaida, DateTime novaDataEntrada) {
      if(!ultimaDataSaida.HasValue) return true;
      if(ultimaDataSaida >= novaDataEntrada) return false;
      if(ultimaDataSaida.Value.Year.Equals(novaDataEntrada.Year) && 
        ultimaDataSaida.Value.Month.Equals(novaDataEntrada.Month)) return false;

      return true;
    }

}