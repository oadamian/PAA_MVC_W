using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAA_MVC_W.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class IncluirCampoEstadoRolModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Rols",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Rols");
        }
    }
}
