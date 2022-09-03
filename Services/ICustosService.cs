namespace SmartLease.Services;
using SmartLease.Models;
using SmartLease.DTOs;

public interface ICustosService {
    Task<List<CustoMensalDTO>> custosMensaisDeProjeto(Projeto projeto, DateTime dataInicialBusca, DateTime dataFinalBusca);
    Task<List<CustoMensalDTO>> custosMensaisDeProjetoEmPeriodoAtivo(Projeto projeto);
}