using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Buildthedatabaseofproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resultPropertyBicycles_propertyBicecles_PropertyBicycleId",
                table: "resultPropertyBicycles");

            migrationBuilder.DropForeignKey(
                name: "FK_resultPropertyBicycles_roverMechs_RoverMechId",
                table: "resultPropertyBicycles");

            migrationBuilder.DropTable(
                name: "propertyBicecles");

            migrationBuilder.DropTable(
                name: "roverMechs");

            migrationBuilder.DropTable(
                name: "shoppingCarts");

            migrationBuilder.RenameColumn(
                name: "RoverMechId",
                table: "resultPropertyBicycles",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "PropertyBicycleId",
                table: "resultPropertyBicycles",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_resultPropertyBicycles_RoverMechId",
                table: "resultPropertyBicycles",
                newName: "IX_resultPropertyBicycles_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_resultPropertyBicycles_PropertyBicycleId",
                table: "resultPropertyBicycles",
                newName: "IX_resultPropertyBicycles_CategoryId");

            migrationBuilder.CreateTable(
                name: "baskets",
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
                    table.PrimaryKey("PK_baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_baskets_userRoverys_UserRoveryId",
                        column: x => x.UserRoveryId,
                        principalTable: "userRoverys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomerRoveryShop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoverGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelRoverMech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandRoverMech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateRoverMechTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "basketProducts",
                columns: table => new
                {
                    BasketID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basketProducts", x => new { x.BasketID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_basketProducts_baskets_BasketID",
                        column: x => x.BasketID,
                        principalTable: "baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_basketProducts_products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_basketProducts_ProductID",
                table: "basketProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_baskets_UserRoveryId",
                table: "baskets",
                column: "UserRoveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_resultPropertyBicycles_categorys_CategoryId",
                table: "resultPropertyBicycles",
                column: "CategoryId",
                principalTable: "categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_resultPropertyBicycles_products_ProductId",
                table: "resultPropertyBicycles",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resultPropertyBicycles_categorys_CategoryId",
                table: "resultPropertyBicycles");

            migrationBuilder.DropForeignKey(
                name: "FK_resultPropertyBicycles_products_ProductId",
                table: "resultPropertyBicycles");

            migrationBuilder.DropTable(
                name: "basketProducts");

            migrationBuilder.DropTable(
                name: "categorys");

            migrationBuilder.DropTable(
                name: "baskets");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "resultPropertyBicycles",
                newName: "RoverMechId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "resultPropertyBicycles",
                newName: "PropertyBicycleId");

            migrationBuilder.RenameIndex(
                name: "IX_resultPropertyBicycles_ProductId",
                table: "resultPropertyBicycles",
                newName: "IX_resultPropertyBicycles_RoverMechId");

            migrationBuilder.RenameIndex(
                name: "IX_resultPropertyBicycles_CategoryId",
                table: "resultPropertyBicycles",
                newName: "IX_resultPropertyBicycles_PropertyBicycleId");

            migrationBuilder.CreateTable(
                name: "propertyBicecles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_propertyBicecles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoveryId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "roverMechs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    BrandRoverMech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateRoverMechTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModelRoverMech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomerRoveryShop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoverGender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roverMechs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roverMechs_shoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "shoppingCarts",
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
                name: "FK_resultPropertyBicycles_propertyBicecles_PropertyBicycleId",
                table: "resultPropertyBicycles",
                column: "PropertyBicycleId",
                principalTable: "propertyBicecles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_resultPropertyBicycles_roverMechs_RoverMechId",
                table: "resultPropertyBicycles",
                column: "RoverMechId",
                principalTable: "roverMechs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
