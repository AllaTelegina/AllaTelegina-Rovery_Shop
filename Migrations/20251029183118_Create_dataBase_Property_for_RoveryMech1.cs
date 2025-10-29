using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Create_dataBase_Property_for_RoveryMech1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "resultPropertyBicycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoverMechId = table.Column<int>(type: "int", nullable: false),
                    PropertyBicycleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resultPropertyBicycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resultPropertyBicycles_propertyBicecles_PropertyBicycleId",
                        column: x => x.PropertyBicycleId,
                        principalTable: "propertyBicecles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_resultPropertyBicycles_roverMechs_RoverMechId",
                        column: x => x.RoverMechId,
                        principalTable: "roverMechs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_resultPropertyBicycles_PropertyBicycleId",
                table: "resultPropertyBicycles",
                column: "PropertyBicycleId");

            migrationBuilder.CreateIndex(
                name: "IX_resultPropertyBicycles_RoverMechId",
                table: "resultPropertyBicycles",
                column: "RoverMechId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resultPropertyBicycles");

            migrationBuilder.DropTable(
                name: "propertyBicecles");
        }
    }
}
