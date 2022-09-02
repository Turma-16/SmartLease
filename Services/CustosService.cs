namespace SmartLease.Services;

using SmartLease.Repositories;
using SmartLease.Models;
using SmartLease.DTOs;

public class CustosService : ICustosService {

    private readonly ILogger<FuncionarioProjetoService> _logger;
    private readonly IFuncionarioProjetoRepo _IFuncionarioProjetoRepo;


    public CustosService(ILogger<FuncionarioProjetoService> logger, IFuncionarioProjetoRepo funcionarioProjetoRepo)
    {
        _logger = logger;
        _IFuncionarioProjetoRepo = funcionarioProjetoRepo;
    }

    public async Task<List<CustoMensalDTO>> verificaProjetosDeFuncionario(Projeto projeto) {

        var response = new List<CustoMensalDTO>();

        var primeiraDataTrabalhadaProjeto = await _IFuncionarioProjetoRepo.buscaPrimeiraDataTrabalhadaProjeto(projeto.Id);
        
        var ultimaDataTrabalhadaProjeto = await _IFuncionarioProjetoRepo.buscaUltimaDataTrabalhadaProjeto(projeto.Id);

        var custoTotalMensalProjeto = 0M;

        for (int i = primeiraDataTrabalhadaProjeto; i = ultimaDataTrabalhadaProjeto.AddMonth(1); i.AddMonth(1)) 
        {
          custoTotalMensalProjeto = 0M;

          var funcionariosAtivosEmData = await _IFuncionarioProjetoRepo.buscaFuncionariosAtivosEmData(projeto.Id, i);
          
          foreach (var f in funcionariosAtivosEmData) {
            custoTotalMensalProjeto += funcionariosAtivosEmData.Salario;
          }

          response.Add(CustoMensalDTO.DeEntidadeParaDTO(
            this.dataToString(primeiraDataTrabalhadaProjeto, custoTotalMensalProjeto)));
        }

        return response;
    }

    public string dataToString(DateTime data) {
      return data.ToString("MM/yyyy");
    }

}
Array.Sort( myKeys, myValues, 1, 3 );
      Console.WriteLine( "" );