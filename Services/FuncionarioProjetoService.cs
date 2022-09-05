namespace SmartLease.Services;

using SmartLease.Repositories;
using SmartLease.Models;

public class FuncionarioProjetoService : IFuncionarioProjetoService {

    private readonly ILogger<FuncionarioProjetoService> _logger;
        private readonly IFuncionarioProjetoRepo _IFuncionarioProjetoRepo;


    public FuncionarioProjetoService(ILogger<FuncionarioProjetoService> logger, IFuncionarioProjetoRepo funcionarioProjetoRepo)
    {
        _logger = logger;
        _IFuncionarioProjetoRepo = funcionarioProjetoRepo;
    }

    public bool dataEntradaValida(DateTime? ultimaDataSaida, DateTime novaDataEntrada) {
      if(!ultimaDataSaida.HasValue) return true;
      if(ultimaDataSaida >= novaDataEntrada) return false;
      if(ultimaDataSaida.Value.Year.Equals(novaDataEntrada.Year) && 
        ultimaDataSaida.Value.Month.Equals(novaDataEntrada.Month)) return false;

      return true;
    }

    public async Task<string?> verificaProjetosDeFuncionario(Funcionario funcionario, Projeto projeto, DateTime novaDataEntrada) {

        var ultimoFuncionarioProjeto = await _IFuncionarioProjetoRepo.buscaUltimoFuncionarioProjeto(funcionario.Id);

        if(ultimoFuncionarioProjeto != null) {
            if(ultimoFuncionarioProjeto!.Ativo) {
                if(ultimoFuncionarioProjeto.ProjetoId == projeto.Id){
                    return "Funcionário já cadastrado neste projeto.";
                }
                return "Funcionário já cadastrado em outro projeto.";
            }
        }

        var ultimaDataSaida = ultimoFuncionarioProjeto?.DataSaida;
        var dataEntradaValida = this.dataEntradaValida(ultimaDataSaida, novaDataEntrada);
        if (!dataEntradaValida) return "Funcionário não pode entrar em outro projeto no momento.";

        return null; //TODO ok por aqui?
    }

}