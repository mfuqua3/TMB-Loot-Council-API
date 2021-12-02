using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class Discord_Server_Navigation_Prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_DiscordServers_DiscordServerId",
                table: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_Guilds_DiscordServerId",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "DiscordServerId",
                table: "Guilds");

            migrationBuilder.CreateTable(
                name: "GuildServerAssociation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServerId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    GuildId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildServerAssociation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildServerAssociation_DiscordServers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "DiscordServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildServerAssociation_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildServerAssociation_GuildId",
                table: "GuildServerAssociation",
                column: "GuildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuildServerAssociation_ServerId",
                table: "GuildServerAssociation",
                column: "ServerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildServerAssociation");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscordServerId",
                table: "Guilds",
                type: "numeric(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_DiscordServerId",
                table: "Guilds",
                column: "DiscordServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_DiscordServers_DiscordServerId",
                table: "Guilds",
                column: "DiscordServerId",
                principalTable: "DiscordServers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
