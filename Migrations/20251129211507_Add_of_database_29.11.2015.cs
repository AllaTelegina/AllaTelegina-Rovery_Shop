using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Add_of_database_29112015 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateRoverMechTime",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "NomerRoveryShop",
                table: "products",
                newName: "SkuNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categorys_products_ProductId",
                table: "categorys");

            migrationBuilder.DropIndex(
                name: "IX_categorys_ProductId",
                table: "categorys");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "categorys");

            migrationBuilder.RenameColumn(
                name: "SkuNumber",
                table: "products",
                newName: "NomerRoveryShop");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateRoverMechTime",
                table: "products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
