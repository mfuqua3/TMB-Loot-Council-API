using Microsoft.EntityFrameworkCore.Migrations;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class Guild_Entity_Configuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuildConfiguration_Guilds_GuildId",
                table: "GuildConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_GuildConfiguration_GuildUsers_OwnerId",
                table: "GuildConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_GuildConfiguration_GuildId",
                table: "GuildConfiguration");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "GuildConfiguration");

            migrationBuilder.AddColumn<int>(
                name: "ConfigurationId",
                table: "Guilds",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "GuildConfiguration",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_ConfigurationId",
                table: "Guilds",
                column: "ConfigurationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GuildConfiguration_AspNetUsers_OwnerId",
                table: "GuildConfiguration",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_GuildConfiguration_ConfigurationId",
                table: "Guilds",
                column: "ConfigurationId",
                principalTable: "GuildConfiguration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuildConfiguration_AspNetUsers_OwnerId",
                table: "GuildConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_GuildConfiguration_ConfigurationId",
                table: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_Guilds_ConfigurationId",
                table: "Guilds");


            migrationBuilder.DropColumn(
                name: "ConfigurationId",
                table: "Guilds");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "GuildConfiguration",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GuildId",
                table: "GuildConfiguration",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_GuildConfiguration_GuildId",
                table: "GuildConfiguration",
                column: "GuildId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GuildConfiguration_Guilds_GuildId",
                table: "GuildConfiguration",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GuildConfiguration_GuildUsers_OwnerId",
                table: "GuildConfiguration",
                column: "OwnerId",
                principalTable: "GuildUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
