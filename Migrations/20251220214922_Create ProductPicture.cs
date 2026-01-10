using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productsPicture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Putch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeEditFile = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productsPicture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productsPicture_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productsPicture_ProductId",
                table: "productsPicture",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productsPicture");
        }
    }
}
