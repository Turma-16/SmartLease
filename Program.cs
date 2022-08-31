using SmartLease.Repositories;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SmartLeaseContext>(opcoes => {
    opcoes.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    opcoes.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
});

builder.Services.AddScoped<IFuncionarioRepo, FuncionarioRepo>();
builder.Services.AddScoped<IProjetoRepo, ProjetoRepo>();
builder.Services.AddScoped<IFuncionarioProjetoRepo, FuncionarioProjetoRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
