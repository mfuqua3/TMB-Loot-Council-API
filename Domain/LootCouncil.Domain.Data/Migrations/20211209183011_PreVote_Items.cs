using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class PreVote_Items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreVoteItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreVoteCharacters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreVoteItemId = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteCharacters_PreVoteItems_PreVoteItemId",
                        column: x => x.PreVoteItemId,
                        principalTable: "PreVoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreVoteVoters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreVoteId = table.Column<int>(type: "integer", nullable: false),
                    GuildUserId = table.Column<int>(type: "integer", nullable: false),
                    PreVoteItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteVoters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteVoters_GuildUsers_GuildUserId",
                        column: x => x.GuildUserId,
                        principalTable: "GuildUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteVoters_PreVoteItems_PreVoteItemId",
                        column: x => x.PreVoteItemId,
                        principalTable: "PreVoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteVoters_PreVotes_PreVoteId",
                        column: x => x.PreVoteId,
                        principalTable: "PreVotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreVoteItemComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreVoteItemId = table.Column<int>(type: "integer", nullable: false),
                    PreVoteVoterId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteItemComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteItemComments_PreVoteItems_PreVoteItemId",
                        column: x => x.PreVoteItemId,
                        principalTable: "PreVoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteItemComments_PreVoteVoters_PreVoteVoterId",
                        column: x => x.PreVoteVoterId,
                        principalTable: "PreVoteVoters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreVoteItemObjections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreVoteVoterId = table.Column<int>(type: "integer", nullable: false),
                    PreVoteItemId = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteItemObjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteItemObjections_PreVoteItems_PreVoteItemId",
                        column: x => x.PreVoteItemId,
                        principalTable: "PreVoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteItemObjections_PreVoteVoters_PreVoteVoterId",
                        column: x => x.PreVoteVoterId,
                        principalTable: "PreVoteVoters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreVoteItemVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PreVoteItemId = table.Column<int>(type: "integer", nullable: false),
                    PreVoteCharacterId = table.Column<int>(type: "integer", nullable: false),
                    PreVoteVoterId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteItemVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteItemVotes_PreVoteCharacters_PreVoteCharacterId",
                        column: x => x.PreVoteCharacterId,
                        principalTable: "PreVoteCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteItemVotes_PreVoteItems_PreVoteItemId",
                        column: x => x.PreVoteItemId,
                        principalTable: "PreVoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteItemVotes_PreVoteVoters_PreVoteVoterId",
                        column: x => x.PreVoteVoterId,
                        principalTable: "PreVoteVoters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreVoteItemObjectionResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreVoteItemObjectionId = table.Column<int>(type: "integer", nullable: false),
                    PreVoteVoterId = table.Column<int>(type: "integer", nullable: false),
                    ResponseRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Response = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteItemObjectionResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteItemObjectionResponses_PreVoteItemObjections_PreVote~",
                        column: x => x.PreVoteItemObjectionId,
                        principalTable: "PreVoteItemObjections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteItemObjectionResponses_PreVoteVoters_PreVoteVoterId",
                        column: x => x.PreVoteVoterId,
                        principalTable: "PreVoteVoters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteCharacters_CharacterId",
                table: "PreVoteCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteCharacters_PreVoteItemId",
                table: "PreVoteCharacters",
                column: "PreVoteItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemComments_PreVoteItemId",
                table: "PreVoteItemComments",
                column: "PreVoteItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemComments_PreVoteVoterId",
                table: "PreVoteItemComments",
                column: "PreVoteVoterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemObjectionResponses_PreVoteItemObjectionId",
                table: "PreVoteItemObjectionResponses",
                column: "PreVoteItemObjectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemObjectionResponses_PreVoteVoterId",
                table: "PreVoteItemObjectionResponses",
                column: "PreVoteVoterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemObjections_PreVoteItemId",
                table: "PreVoteItemObjections",
                column: "PreVoteItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemObjections_PreVoteVoterId",
                table: "PreVoteItemObjections",
                column: "PreVoteVoterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItems_ItemId",
                table: "PreVoteItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemVotes_PreVoteCharacterId",
                table: "PreVoteItemVotes",
                column: "PreVoteCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemVotes_PreVoteItemId",
                table: "PreVoteItemVotes",
                column: "PreVoteItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteItemVotes_PreVoteVoterId",
                table: "PreVoteItemVotes",
                column: "PreVoteVoterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteVoters_GuildUserId",
                table: "PreVoteVoters",
                column: "GuildUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteVoters_PreVoteId",
                table: "PreVoteVoters",
                column: "PreVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteVoters_PreVoteItemId",
                table: "PreVoteVoters",
                column: "PreVoteItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreVoteItemComments");

            migrationBuilder.DropTable(
                name: "PreVoteItemObjectionResponses");

            migrationBuilder.DropTable(
                name: "PreVoteItemVotes");

            migrationBuilder.DropTable(
                name: "PreVoteItemObjections");

            migrationBuilder.DropTable(
                name: "PreVoteCharacters");

            migrationBuilder.DropTable(
                name: "PreVoteVoters");

            migrationBuilder.DropTable(
                name: "PreVoteItems");
        }
    }
}
