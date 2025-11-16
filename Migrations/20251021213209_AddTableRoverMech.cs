using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class AddTableRoverMech : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roverMechs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomerRoveryShop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoverGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelRoverMech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandRoverMech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoverMechTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRoveryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roverMechs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roverMechs_userRoverys_UserRoveryId",
                        column: x => x.UserRoveryId,
                        principalTable: "userRoverys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_roverMechs_UserRoveryId",
                table: "roverMechs",
                column: "UserRoveryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roverMechs");
        }
    }
}
