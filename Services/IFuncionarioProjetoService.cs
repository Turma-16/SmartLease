namespace SmartLease.Services;

public interface IFuncionarioProjetoService {
  bool dataEntradaValida(DateTime? ultimaDataSaida, DateTime novaDataEntrada);    
}