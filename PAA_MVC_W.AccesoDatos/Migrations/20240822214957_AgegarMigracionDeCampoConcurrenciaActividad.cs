using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAA_MVC_W.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgegarMigracionDeCampoConcurrenciaActividad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnidadAuditoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cc = table.Column<int>(type: "int", nullable: false),
                    Siglas = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nombre_UA = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadAuditoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DireccionGenerals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cc = table.Column<int>(type: "int", nullable: false),
                    Siglas = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre_DG = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UnidadAuditoraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionGenerals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DireccionGenerals_UnidadAuditoras_UnidadAuditoraId",
                        column: x => x.UnidadAuditoraId,
                        principalTable: "UnidadAuditoras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NombreActividad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjetivoActividad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadProducto = table.Column<int>(type: "int", nullable: false),
                    TipoProducto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MedioVerificacionProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha4 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObjetivoClave = table.Column<bool>(type: "bit", nullable: false),
                    ActividadControl = table.Column<bool>(type: "bit", nullable: false),
                    Supuestos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especificaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AplicaIndicador = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UnidadAuditoraId = table.Column<int>(type: "int", nullable: false),
                    DireccionGeneralId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actividades_DireccionGenerals_DireccionGeneralId",
                        column: x => x.DireccionGeneralId,
                        principalTable: "DireccionGenerals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Actividades_UnidadAuditoras_UnidadAuditoraId",
                        column: x => x.UnidadAuditoraId,
                        principalTable: "UnidadAuditoras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Bis = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UnidadAuditoraId = table.Column<int>(type: "int", nullable: false),
                    DireccionGeneralId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulos_DireccionGenerals_DireccionGeneralId",
                        column: x => x.DireccionGeneralId,
                        principalTable: "DireccionGenerals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articulos_UnidadAuditoras_UnidadAuditoraId",
                        column: x => x.UnidadAuditoraId,
                        principalTable: "UnidadAuditoras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAplicacion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    UnidadAuditoraId = table.Column<int>(type: "int", nullable: false),
                    DireccionGeneralId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAplicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioAplicacion_DireccionGenerals_DireccionGeneralId",
                        column: x => x.DireccionGeneralId,
                        principalTable: "DireccionGenerals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioAplicacion_UnidadAuditoras_UnidadAuditoraId",
                        column: x => x.UnidadAuditoraId,
                        principalTable: "UnidadAuditoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fraccions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frac = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bis = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fraccions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fraccions_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_DireccionGeneralId",
                table: "Actividades",
                column: "DireccionGeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_UnidadAuditoraId",
                table: "Actividades",
                column: "UnidadAuditoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_DireccionGeneralId",
                table: "Articulos",
                column: "DireccionGeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_UnidadAuditoraId",
                table: "Articulos",
                column: "UnidadAuditoraId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionGenerals_UnidadAuditoraId",
                table: "DireccionGenerals",
                column: "UnidadAuditoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Fraccions_ArticuloId",
                table: "Fraccions",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAplicacion_DireccionGeneralId",
                table: "UsuarioAplicacion",
                column: "DireccionGeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAplicacion_UnidadAuditoraId",
                table: "UsuarioAplicacion",
                column: "UnidadAuditoraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Fraccions");

            migrationBuilder.DropTable(
                name: "UsuarioAplicacion");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "DireccionGenerals");

            migrationBuilder.DropTable(
                name: "UnidadAuditoras");
        }
    }
}
