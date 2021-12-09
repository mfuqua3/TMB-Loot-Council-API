using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class PreVote_Item_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreVoteId",
                table: "PreVoteItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItems_PreVoteId",
                table: "PreVoteItems",
                column: "PreVoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreVoteItems_PreVotes_PreVoteId",
                table: "PreVoteItems",
                column: "PreVoteId",
                principalTable: "PreVotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreVoteItems_PreVotes_PreVoteId",
                table: "PreVoteItems");

            migrationBuilder.DropIndex(
                name: "IX_PreVoteItems_PreVoteId",
                table: "PreVoteItems");

            migrationBuilder.DropColumn(
                name: "PreVoteId",
                table: "PreVoteItems");
        }
    }
}
