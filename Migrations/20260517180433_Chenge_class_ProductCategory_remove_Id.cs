using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Chenge_class_ProductCategory_remove_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productsPicture_products_ProductId",
                table: "productsPicture");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "productCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_productsPicture_products_ProductId",
                table: "productsPicture",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productsPicture_products_ProductId",
                table: "productsPicture");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "productCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_productsPicture_products_ProductId",
                table: "productsPicture",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
