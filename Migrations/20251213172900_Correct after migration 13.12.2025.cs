using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Correctaftermigration13122025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categorys_IsCategoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "IsCategoryId",
                table: "products",
                newName: "IsCategory");

            migrationBuilder.RenameIndex(
                name: "IX_products_IsCategoryId",
                table: "products",
                newName: "IX_products_IsCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categorys_IsCategory",
                table: "products",
                column: "IsCategory",
                principalTable: "categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categorys_IsCategory",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "IsCategory",
                table: "products",
                newName: "IsCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_IsCategory",
                table: "products",
                newName: "IX_products_IsCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categorys_IsCategoryId",
                table: "products",
                column: "IsCategoryId",
                principalTable: "categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
