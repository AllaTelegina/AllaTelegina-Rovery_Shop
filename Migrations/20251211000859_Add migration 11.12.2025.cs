using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Addmigration11122025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categorys_products_ProductId",
                table: "categorys");

            migrationBuilder.DropIndex(
                name: "IX_categorys_ProductId",
                table: "categorys");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "categorys");

            migrationBuilder.AddColumn<string>(
                name: "BriefDiscription",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullDiscription",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IsCategoryId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Lastprise",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prise",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_IsCategoryId",
                table: "products",
                column: "IsCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categorys_IsCategoryId",
                table: "products",
                column: "IsCategoryId",
                principalTable: "categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categorys_IsCategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_IsCategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "BriefDiscription",
                table: "products");

            migrationBuilder.DropColumn(
                name: "FullDiscription",
                table: "products");

            migrationBuilder.DropColumn(
                name: "IsCategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Lastprise",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Prise",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "categorys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_categorys_ProductId",
                table: "categorys",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_categorys_products_ProductId",
                table: "categorys",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id");
        }
    }
}
