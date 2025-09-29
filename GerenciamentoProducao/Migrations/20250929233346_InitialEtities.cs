using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoProducaoo.Migrations
{
    /// <inheritdoc />
    public partial class InitialEtities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamiliaCaixilho",
                columns: table => new
                {
                    IdFamiliaCaixilho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoFamilia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PesoTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliaCaixilho", x => x.IdFamiliaCaixilho);
                });

            migrationBuilder.CreateTable(
                name: "TipoCaixilho",
                columns: table => new
                {
                    IdTipoCaixilho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoCaixilho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCaixilho", x => x.IdTipoCaixilho);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    IdTipoUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.IdTipoUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    IdTipoUsuario = table.Column<int>(type: "int", nullable: false),
                    TipoUsuarioIdTipoUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_TipoUsuario_TipoUsuarioIdTipoUsuario",
                        column: x => x.TipoUsuarioIdTipoUsuario,
                        principalTable: "TipoUsuario",
                        principalColumn: "IdTipoUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Obra",
                columns: table => new
                {
                    IdObra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Construtora = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obra", x => x.IdObra);
                    table.ForeignKey(
                        name: "FK_Obra_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Producao",
                columns: table => new
                {
                    IdProducao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProducao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataProducao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Produzido = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EhLiberado = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    FamiliaCaixilhoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producao", x => x.IdProducao);
                    table.ForeignKey(
                        name: "FK_Producao_FamiliaCaixilho_FamiliaCaixilhoId",
                        column: x => x.FamiliaCaixilhoId,
                        principalTable: "FamiliaCaixilho",
                        principalColumn: "IdFamiliaCaixilho",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producao_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Caixilho",
                columns: table => new
                {
                    IdCaixilho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCaixilho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Largura = table.Column<int>(type: "int", nullable: false),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PesoUnitario = table.Column<float>(type: "real", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false),
                    IdFamiliaCaixilho = table.Column<int>(type: "int", nullable: false),
                    FamiliaCaixilhoIdFamiliaCaixilho = table.Column<int>(type: "int", nullable: true),
                    IdTipoCaixilho = table.Column<int>(type: "int", nullable: false),
                    TipoCaixilhoIdTipoCaixilho = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixilho", x => x.IdCaixilho);
                    table.ForeignKey(
                        name: "FK_Caixilho_FamiliaCaixilho_FamiliaCaixilhoIdFamiliaCaixilho",
                        column: x => x.FamiliaCaixilhoIdFamiliaCaixilho,
                        principalTable: "FamiliaCaixilho",
                        principalColumn: "IdFamiliaCaixilho");
                    table.ForeignKey(
                        name: "FK_Caixilho_Obra_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obra",
                        principalColumn: "IdObra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caixilho_TipoCaixilho_TipoCaixilhoIdTipoCaixilho",
                        column: x => x.TipoCaixilhoIdTipoCaixilho,
                        principalTable: "TipoCaixilho",
                        principalColumn: "IdTipoCaixilho");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caixilho_FamiliaCaixilhoIdFamiliaCaixilho",
                table: "Caixilho",
                column: "FamiliaCaixilhoIdFamiliaCaixilho");

            migrationBuilder.CreateIndex(
                name: "IX_Caixilho_ObraId",
                table: "Caixilho",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixilho_TipoCaixilhoIdTipoCaixilho",
                table: "Caixilho",
                column: "TipoCaixilhoIdTipoCaixilho");

            migrationBuilder.CreateIndex(
                name: "IX_Obra_UsuarioIdUsuario",
                table: "Obra",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Producao_FamiliaCaixilhoId",
                table: "Producao",
                column: "FamiliaCaixilhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producao_UsuarioIdUsuario",
                table: "Producao",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioIdTipoUsuario",
                table: "Usuario",
                column: "TipoUsuarioIdTipoUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caixilho");

            migrationBuilder.DropTable(
                name: "Producao");

            migrationBuilder.DropTable(
                name: "Obra");

            migrationBuilder.DropTable(
                name: "TipoCaixilho");

            migrationBuilder.DropTable(
                name: "FamiliaCaixilho");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoUsuario");
        }
    }
}
