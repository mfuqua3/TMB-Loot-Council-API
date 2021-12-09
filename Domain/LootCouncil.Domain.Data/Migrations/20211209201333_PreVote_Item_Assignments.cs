using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class PreVote_Item_Assignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreVoteVoters_PreVoteItems_PreVoteItemId",
                table: "PreVoteVoters");

            migrationBuilder.DropIndex(
                name: "IX_PreVoteVoters_PreVoteItemId",
                table: "PreVoteVoters");

            migrationBuilder.DropColumn(
                name: "PreVoteItemId",
                table: "PreVoteVoters");

            migrationBuilder.CreateTable(
                name: "PreVoteItemAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreVoteVoterId = table.Column<int>(type: "integer", nullable: false),
                    PreVoteItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteItemAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteItemAssignments_PreVoteItems_PreVoteItemId",
                        column: x => x.PreVoteItemId,
                        principalTable: "PreVoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteItemAssignments_PreVoteVoters_PreVoteVoterId",
                        column: x => x.PreVoteVoterId,
                        principalTable: "PreVoteVoters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemAssignments_PreVoteItemId",
                table: "PreVoteItemAssignments",
                column: "PreVoteItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemAssignments_PreVoteVoterId",
                table: "PreVoteItemAssignments",
                column: "PreVoteVoterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreVoteItemAssignments");

            migrationBuilder.AddColumn<int>(
                name: "PreVoteItemId",
                table: "PreVoteVoters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteVoters_PreVoteItemId",
                table: "PreVoteVoters",
                column: "PreVoteItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreVoteVoters_PreVoteItems_PreVoteItemId",
                table: "PreVoteVoters",
                column: "PreVoteItemId",
                principalTable: "PreVoteItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
