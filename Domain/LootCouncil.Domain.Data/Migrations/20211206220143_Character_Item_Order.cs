using Microsoft.EntityFrameworkCore.Migrations;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class Character_Item_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuildId",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuildUserId",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CharacterItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GuildId",
                table: "Characters",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GuildUserId",
                table: "Characters",
                column: "GuildUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Guilds_GuildId",
                table: "Characters",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_GuildUsers_GuildUserId",
                table: "Characters",
                column: "GuildUserId",
                principalTable: "GuildUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Guilds_GuildId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GuildUsers_GuildUserId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_GuildId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_GuildUserId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GuildUserId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CharacterItems");
        }
    }
}
