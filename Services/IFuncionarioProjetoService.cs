namespace SmartLease.Services;
using SmartLease.Models;

public interface IFuncionarioProjetoService {
  bool dataEntradaValida(DateTime? ultimaDataSaida, DateTime novaDataEntrada);
  Task<string?> verificaProjetosDeFuncionario(Funcionario funcionario, Projeto projeto, DateTime novaDataEntrada);
}