namespace SmartLease.Services;

using SmartLease.Repositories;
using SmartLease.Models;
using SmartLease.DTOs;

public class CustosService : ICustosService {

    private readonly ILogger<FuncionarioProjetoService> _logger;
    private readonly IFuncionarioProjetoRepo _IFuncionarioProjetoRepo;
    private readonly IReservaRepo _IReservaRepo;

    public CustosService(ILogger<FuncionarioProjetoService> logger, IFuncionarioProjetoRepo funcionarioProjetoRepo, IReservaRepo reservaRepo)
    {
        _logger = logger;
        _IFuncionarioProjetoRepo = funcionarioProjetoRepo;
        _IReservaRepo = reservaRepo;
    }

    public async Task<List<CustoMensalDTO>> custosMensaisDeProjeto(Projeto projeto, DateTime dataInicialBusca, DateTime dataFinalBusca) {

      return await _somaCustosDeProjetoEmPeriodo(projeto, dataInicialBusca, dataFinalBusca);
    }

/* Essa função usa como dataInicial do relatorio a primeira data em q o projeto foi ativo, e ultima data 12 meses depois ou  now(), o que vier primeiro
  A diferença entre custosMensaisDeProjetoEmPeriodoAtivo e custosMensaisDeProjeto é que custosMensaisDeProjetoEmPeriodoAtivo, se algum dia tiver sido ativa, 
  retornara a resposta a partir dessa data, não requere parâmetros adicionais e evita buscas em meses não ativos. 
  custosMensaisDeProjeto é para fins de relatório, com parâmetros de datas cuja resposta pode ser vazia (o que é útil no gráfico).
*/
    public async Task<List<CustoMensalDTO>> custosMensaisDeProjetoEmPeriodoAtivo(Projeto projeto) {

        var busca_primeiraDataTrabalhadaProjeto = await _IFuncionarioProjetoRepo.buscaPrimeiraDataTrabalhadaProjeto(projeto.Id);
        
        if(!busca_primeiraDataTrabalhadaProjeto.HasValue) return new List<CustoMensalDTO>();
        
        var primeiraData = busca_primeiraDataTrabalhadaProjeto.Value;
        
        var ultimaData = getUltimaDataDefault(primeiraData); // determina limite do relatório. ou a data atual ou 12 meses depois da primeira data, o que vier primeiro.

        //se ainda temos funcionarios cuja dataSaida == null
        var busca_ehProjetoAtivo = await _IFuncionarioProjetoRepo.ehProjetoAtivo(projeto.Id);

        if(!busca_ehProjetoAtivo) {

          var busca_ultimaDataTrabalhadaProjeto = await _IFuncionarioProjetoRepo.buscaUltimaDataTrabalhadaProjeto(projeto.Id);

          if(busca_ultimaDataTrabalhadaProjeto.HasValue)
            ultimaData = busca_ultimaDataTrabalhadaProjeto.Value;
        }

        return await _somaCustosDeProjetoEmPeriodo(projeto, primeiraData, ultimaData);
    }

    /* Essa função é pra ser chamada somente ao possuir um período de datas em que sabemos que existiam funcionários escritos no projeto! */
    private async Task<List<CustoMensalDTO>> _somaCustosDeProjetoEmPeriodo(Projeto projeto, DateTime dataInicialBusca, DateTime dataFinalBusca) {        

        var custoTotalMensalProjeto = 0M;
        var response = new List<CustoMensalDTO>();

        if(dataInicialBusca.Month > dataFinalBusca.Month) return response;

        for (DateTime i = dataInicialBusca; i <= dataFinalBusca; i = i.AddMonths(1)) 
        {
          custoTotalMensalProjeto = 0M;

          var funcionariosAtivosEmData = await _IFuncionarioProjetoRepo.buscaFuncionariosAtivosEmData(projeto.Id, i);
          List <Reserva> reservasEmData = new List<Reserva>();

            foreach (var f in funcionariosAtivosEmData) {
              custoTotalMensalProjeto += f.Salario;

              var reservas = await _IReservaRepo.BuscarPorDataEFuncionario(i, f.Id);
              
              if(reservas != null) {

                foreach(Reserva reserva in reservas) {
                  
                  if(reserva.DataReserva >= dataInicialBusca && reserva.DataReserva <= dataFinalBusca)
                  {
                    reservasEmData.Add(reserva);
                    custoTotalMensalProjeto += reserva.CustoReserva;
                  }
                }
              }
          }

          response.Add(CustoMensalDTO.DeEntidadeParaDTO(
              i,
              this.dataToString(i), 
              custoTotalMensalProjeto,
              funcionariosAtivosEmData,
              reservasEmData));
        }

        return response;
    }

    

    public string dataToString(DateTime data) {
      return data.ToString("MMMM yyyy");
    }

    public DateTime minData(DateTime data1, DateTime data2) {
      return data1 < data2 ? data1 : data2;
    }

    public DateTime getUltimaDataDefault(DateTime primeiraData) {
      return minData(DateTime.Now, primeiraData.AddMonths(12));
    }

}