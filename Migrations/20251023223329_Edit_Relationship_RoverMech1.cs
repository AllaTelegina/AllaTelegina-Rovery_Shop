using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Relationship_RoverMech1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roverMechs_shoppingCarts_ShoppingCartId",
                table: "roverMechs");

            migrationBuilder.DropForeignKey(
                name: "FK_roverMechs_userRoverys_UserRoveryId",
                table: "roverMechs");

            migrationBuilder.DropIndex(
                name: "IX_roverMechs_UserRoveryId",
                table: "roverMechs");

            migrationBuilder.DropColumn(
                name: "UserRoveryId",
                table: "roverMechs");

            migrationBuilder.RenameColumn(
                name: "RoverMechTime",
                table: "roverMechs",
                newName: "CreateRoverMechTime");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "roverMechs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_roverMechs_shoppingCarts_ShoppingCartId",
                table: "roverMechs",
                column: "ShoppingCartId",
                principalTable: "shoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roverMechs_shoppingCarts_ShoppingCartId",
                table: "roverMechs");

            migrationBuilder.RenameColumn(
                name: "CreateRoverMechTime",
                table: "roverMechs",
                newName: "RoverMechTime");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "roverMechs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserRoveryId",
                table: "roverMechs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_roverMechs_UserRoveryId",
                table: "roverMechs",
                column: "UserRoveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_roverMechs_shoppingCarts_ShoppingCartId",
                table: "roverMechs",
                column: "ShoppingCartId",
                principalTable: "shoppingCarts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_roverMechs_userRoverys_UserRoveryId",
                table: "roverMechs",
                column: "UserRoveryId",
                principalTable: "userRoverys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
