using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class Identity_Discord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DiscordIdentityId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiscordIdentities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordIdentities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiscordIdentityId",
                table: "AspNetUsers",
                column: "DiscordIdentityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DiscordIdentities_DiscordIdentityId",
                table: "AspNetUsers",
                column: "DiscordIdentityId",
                principalTable: "DiscordIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DiscordIdentities_DiscordIdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DiscordIdentities");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiscordIdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiscordIdentityId",
                table: "AspNetUsers");
        }
    }
}
