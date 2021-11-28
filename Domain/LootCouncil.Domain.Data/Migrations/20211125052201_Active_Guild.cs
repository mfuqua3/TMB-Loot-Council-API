using Microsoft.EntityFrameworkCore.Migrations;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class Active_Guild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActiveGuildId",
                table: "AspNetUsers",
                type: "numeric(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ActiveGuildId",
                table: "AspNetUsers",
                column: "ActiveGuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Guilds_ActiveGuildId",
                table: "AspNetUsers",
                column: "ActiveGuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Guilds_ActiveGuildId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ActiveGuildId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95e21d7f-1d58-47ea-b679-dd0abe56ced7");

        }
    }
}
