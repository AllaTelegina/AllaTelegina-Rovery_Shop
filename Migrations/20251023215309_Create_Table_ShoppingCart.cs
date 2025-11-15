using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_ShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "roverMechs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "shoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRoveryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shoppingCarts_userRoverys_UserRoveryId",
                        column: x => x.UserRoveryId,
                        principalTable: "userRoverys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_roverMechs_ShoppingCartId",
                table: "roverMechs",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCarts_UserRoveryId",
                table: "shoppingCarts",
                column: "UserRoveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_roverMechs_shoppingCarts_ShoppingCartId",
                table: "roverMechs",
                column: "ShoppingCartId",
                principalTable: "shoppingCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roverMechs_shoppingCarts_ShoppingCartId",
                table: "roverMechs");

            migrationBuilder.DropTable(
                name: "shoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_roverMechs_ShoppingCartId",
                table: "roverMechs");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "roverMechs");
        }
    }
}
