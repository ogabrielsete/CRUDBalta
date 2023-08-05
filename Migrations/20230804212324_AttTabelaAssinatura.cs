using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteBalta.Migrations
{
    /// <inheritdoc />
    public partial class AttTabelaAssinatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assinatura_Aluno_AlunoId",
                table: "Assinatura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assinatura",
                table: "Assinatura");

            migrationBuilder.RenameTable(
                name: "Assinatura",
                newName: "AssinaturaDB");

            migrationBuilder.RenameColumn(
                name: "Expirado",
                table: "AssinaturaDB",
                newName: "Ativo");

            migrationBuilder.RenameIndex(
                name: "IX_Assinatura_AlunoId",
                table: "AssinaturaDB",
                newName: "IX_AssinaturaDB_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssinaturaDB",
                table: "AssinaturaDB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssinaturaDB_Aluno_AlunoId",
                table: "AssinaturaDB",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssinaturaDB_Aluno_AlunoId",
                table: "AssinaturaDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssinaturaDB",
                table: "AssinaturaDB");

            migrationBuilder.RenameTable(
                name: "AssinaturaDB",
                newName: "Assinatura");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Assinatura",
                newName: "Expirado");

            migrationBuilder.RenameIndex(
                name: "IX_AssinaturaDB_AlunoId",
                table: "Assinatura",
                newName: "IX_Assinatura_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assinatura",
                table: "Assinatura",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assinatura_Aluno_AlunoId",
                table: "Assinatura",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
