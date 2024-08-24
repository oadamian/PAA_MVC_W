using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAA_MVC_W.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarmodelosUsuarioRolporequivicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_DireccionGenerals_DireccionGeneralId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_UnidadAuditoras_UnidadAuditoraId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Rol_RolId",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuario_UsuarioId",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.RenameTable(
                name: "UsuarioRol",
                newName: "UsuarioRols");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Rols");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRol_UsuarioId_RolId",
                table: "UsuarioRols",
                newName: "IX_UsuarioRols_UsuarioId_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRol_RolId",
                table: "UsuarioRols",
                newName: "IX_UsuarioRols_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_UnidadAuditoraId",
                table: "Usuarios",
                newName: "IX_Usuarios_UnidadAuditoraId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_DireccionGeneralId",
                table: "Usuarios",
                newName: "IX_Usuarios_DireccionGeneralId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRols",
                table: "UsuarioRols",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rols",
                table: "Rols",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRols_Rols_RolId",
                table: "UsuarioRols",
                column: "RolId",
                principalTable: "Rols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRols_Usuarios_UsuarioId",
                table: "UsuarioRols",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_DireccionGenerals_DireccionGeneralId",
                table: "Usuarios",
                column: "DireccionGeneralId",
                principalTable: "DireccionGenerals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_UnidadAuditoras_UnidadAuditoraId",
                table: "Usuarios",
                column: "UnidadAuditoraId",
                principalTable: "UnidadAuditoras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRols_Rols_RolId",
                table: "UsuarioRols");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRols_Usuarios_UsuarioId",
                table: "UsuarioRols");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_DireccionGenerals_DireccionGeneralId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_UnidadAuditoras_UnidadAuditoraId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRols",
                table: "UsuarioRols");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rols",
                table: "Rols");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "UsuarioRols",
                newName: "UsuarioRol");

            migrationBuilder.RenameTable(
                name: "Rols",
                newName: "Rol");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_UnidadAuditoraId",
                table: "Usuario",
                newName: "IX_Usuario_UnidadAuditoraId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_DireccionGeneralId",
                table: "Usuario",
                newName: "IX_Usuario_DireccionGeneralId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRols_UsuarioId_RolId",
                table: "UsuarioRol",
                newName: "IX_UsuarioRol_UsuarioId_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRols_RolId",
                table: "UsuarioRol",
                newName: "IX_UsuarioRol_RolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_DireccionGenerals_DireccionGeneralId",
                table: "Usuario",
                column: "DireccionGeneralId",
                principalTable: "DireccionGenerals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_UnidadAuditoras_UnidadAuditoraId",
                table: "Usuario",
                column: "UnidadAuditoraId",
                principalTable: "UnidadAuditoras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Rol_RolId",
                table: "UsuarioRol",
                column: "RolId",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuario_UsuarioId",
                table: "UsuarioRol",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
