using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTSL.Ovidhan.API.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                schema: "GS",
                table: "Todos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CategoryId",
                schema: "GS",
                table: "Todos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Categorys_CategoryId",
                schema: "GS",
                table: "Todos",
                column: "CategoryId",
                principalSchema: "GS",
                principalTable: "Categorys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Categorys_CategoryId",
                schema: "GS",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_CategoryId",
                schema: "GS",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "GS",
                table: "Todos");
        }
    }
}
