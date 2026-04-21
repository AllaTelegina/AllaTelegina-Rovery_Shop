using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class AppendfildIdNameinUserRovery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdName",
                table: "userRoverys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdName",
                table: "userRoverys");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "products");
        }
    }
}
