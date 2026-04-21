using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class Chenge_ProductPicture_add_MainPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MainPhoto",
                table: "productsPicture",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "productsPicture");
        }
    }
}
