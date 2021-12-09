using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class Character_Soft_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Characters",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Characters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_IsDeleted",
                table: "Characters",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_IsDeleted",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Characters");
        }
    }
}
