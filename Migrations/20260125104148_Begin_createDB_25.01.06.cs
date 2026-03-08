using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Begin_createDB_250106 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "keyFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateKeyFeature = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keyFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkuNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prise = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lastprise = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateRoverMechTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BriefDiscription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullDiscription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userRoverys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true),
                    TimeIntrance = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSignature = table.Column<bool>(type: "bit", nullable: true),
                    IsStatus = table.Column<bool>(type: "bit", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bilding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreateUserRovery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: true),
                    TokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRoverys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_productCategories_categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productCategories_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productKeyFeatures",
                columns: table => new
                {
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    IdKeyFeature = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productKeyFeatures", x => new { x.IdProduct, x.IdKeyFeature });
                    table.ForeignKey(
                        name: "FK_productKeyFeatures_keyFeatures_IdKeyFeature",
                        column: x => x.IdKeyFeature,
                        principalTable: "keyFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productKeyFeatures_products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "basketProducts",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    BasketID = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_productCategories_CategoryId",
                table: "productCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productKeyFeatures_IdKeyFeature",
                table: "productKeyFeatures",
                column: "IdKeyFeature");

            migrationBuilder.CreateIndex(
                name: "IX_productsPicture_ProductId",
                table: "productsPicture",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basketProducts");

            migrationBuilder.DropTable(
                name: "productCategories");

            migrationBuilder.DropTable(
                name: "productKeyFeatures");

            migrationBuilder.DropTable(
                name: "productsPicture");

            migrationBuilder.DropTable(
                name: "baskets");

            migrationBuilder.DropTable(
                name: "categorys");

            migrationBuilder.DropTable(
                name: "keyFeatures");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "userRoverys");
        }
    }
}
