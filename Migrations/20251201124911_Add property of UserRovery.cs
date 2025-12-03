using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_asp.net.Migrations
{
    /// <inheritdoc />
    public partial class AddpropertyofUserRovery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "userRoverys",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsSignature",
                table: "userRoverys",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatus",
                table: "userRoverys",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeIntrance",
                table: "userRoverys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LevelCategory",
                table: "categorys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSignature",
                table: "userRoverys");

            migrationBuilder.DropColumn(
                name: "IsStatus",
                table: "userRoverys");

            migrationBuilder.DropColumn(
                name: "TimeIntrance",
                table: "userRoverys");

            migrationBuilder.DropColumn(
                name: "LevelCategory",
                table: "categorys");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "userRoverys",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
