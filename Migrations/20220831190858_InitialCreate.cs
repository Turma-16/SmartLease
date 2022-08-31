using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartlease.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustoDiario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__equipamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_projetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__projetos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustoReserva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva__equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "_equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva__funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "_funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioProjeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: true),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioProjeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionarioProjeto__funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "_funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioProjeto__projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "_projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioProjeto_FuncionarioId",
                table: "FuncionarioProjeto",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioProjeto_ProjetoId",
                table: "FuncionarioProjeto",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_EquipamentoId",
                table: "Reserva",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_FuncionarioId",
                table: "Reserva",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionarioProjeto");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "_projetos");

            migrationBuilder.DropTable(
                name: "_equipamentos");

            migrationBuilder.DropTable(
                name: "_funcionarios");
        }
    }
}
