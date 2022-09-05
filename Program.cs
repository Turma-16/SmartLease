using SmartLease.Repositories;
using Microsoft.EntityFrameworkCore;
using SmartLease.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SmartLeaseContext>(opcoes => {
    opcoes.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    opcoes.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
});

builder.Services.AddScoped<IFuncionarioRepo, FuncionarioRepo>();
builder.Services.AddScoped<IProjetoRepo, ProjetoRepo>();
builder.Services.AddScoped<IFuncionarioProjetoRepo, FuncionarioProjetoRepo>();
builder.Services.AddScoped<IEquipamentoRepo, EquipamentoRepo>();
builder.Services.AddScoped<IFuncionarioProjetoService, FuncionarioProjetoService>();
builder.Services.AddScoped<ICustosService, CustosService>();
builder.Services.AddScoped<IReservaRepo, ReservaRepo>();

builder.Services.AddCors(opcoes => {
    opcoes.AddPolicy("LiberaGeral", politica => {
        politica.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
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
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
