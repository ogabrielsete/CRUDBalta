using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteBalta.Migrations
{
    /// <inheritdoc />
    public partial class AttTabelaAluno02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Aluno");

            migrationBuilder.AddColumn<string>(
                name: "ImagemAluno",
                table: "Aluno",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemAluno",
                table: "Aluno");

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Aluno",
                type: "BLOB",
                nullable: true);
        }
    }
}
