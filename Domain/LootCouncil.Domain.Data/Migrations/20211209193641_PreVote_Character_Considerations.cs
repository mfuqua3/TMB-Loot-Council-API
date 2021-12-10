using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class PreVote_Character_Considerations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreVoteCharacterConsiderations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreVoteCharacterId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteCharacterConsiderations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteCharacterConsiderations_PreVoteCharacters_PreVoteCha~",
                        column: x => x.PreVoteCharacterId,
                        principalTable: "PreVoteCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteCharacterConsiderations_PreVoteCharacterId",
                table: "PreVoteCharacterConsiderations",
                column: "PreVoteCharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreVoteCharacterConsiderations");
        }
    }
}
