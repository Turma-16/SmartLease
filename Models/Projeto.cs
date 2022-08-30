using System.ComponentModel.DataAnnotations;

namespace SmartLease.Models;
public class Projeto {

    public int Id { get; set; }
    public string Nome { get; set; } = null!;

    //ICollection<Funcionario> Funcionarios { get; set; };
    
}