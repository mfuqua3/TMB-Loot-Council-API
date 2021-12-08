using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class PreVote_Domain_Definition_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConflictOfInterestConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AllowSelfVoting = table.Column<bool>(type: "boolean", nullable: false),
                    AllowVoting = table.Column<bool>(type: "boolean", nullable: false),
                    AllowObjecting = table.Column<bool>(type: "boolean", nullable: false),
                    AllowCommenting = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConflictOfInterestConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpirationConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LockCommentsTteMinutes = table.Column<int>(type: "integer", nullable: false),
                    LockObjectionsTteMinutes = table.Column<int>(type: "integer", nullable: false),
                    LockVotesTteMinutes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpirationConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemSelectionConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SelectAll = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSelectionConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoterSelectionConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MinimumVotersPerItem = table.Column<int>(type: "integer", nullable: false),
                    MaximumVotersPerItem = table.Column<int>(type: "integer", nullable: false),
                    Randomize = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterSelectionConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoteVisibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AllowGuild = table.Column<bool>(type: "boolean", nullable: false),
                    AllowAllEligibleVoters = table.Column<bool>(type: "boolean", nullable: false),
                    VoteSubmissionRequirement = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteVisibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildRoleVoterConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoterSelectionConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    GuildRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildRoleVoterConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildRoleVoterConfigurations_GuildRoles_GuildRoleId",
                        column: x => x.GuildRoleId,
                        principalTable: "GuildRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildRoleVoterConfigurations_VoterSelectionConfigurations_V~",
                        column: x => x.VoterSelectionConfigurationId,
                        principalTable: "VoterSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuildUserVoterConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoterSelectionConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    GuildUserId = table.Column<int>(type: "integer", nullable: false),
                    Eligible = table.Column<bool>(type: "boolean", nullable: false),
                    Fixed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildUserVoterConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildUserVoterConfigurations_GuildUsers_GuildUserId",
                        column: x => x.GuildUserId,
                        principalTable: "GuildUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildUserVoterConfigurations_VoterSelectionConfigurations_V~",
                        column: x => x.VoterSelectionConfigurationId,
                        principalTable: "VoterSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransparencyConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoteVisibilityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransparencyConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransparencyConfigurations_VoteVisibilities_VoteVisibilityId",
                        column: x => x.VoteVisibilityId,
                        principalTable: "VoteVisibilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuildId = table.Column<int>(type: "integer", nullable: false),
                    ExpirationConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    ItemSelectionConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    VoterSelectionConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    ConflictOfInterestConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    TransparencyConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVotes_ConflictOfInterestConfigurations_ConflictOfInteres~",
                        column: x => x.ConflictOfInterestConfigurationId,
                        principalTable: "ConflictOfInterestConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVotes_ExpirationConfigurations_ExpirationConfigurationId",
                        column: x => x.ExpirationConfigurationId,
                        principalTable: "ExpirationConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVotes_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVotes_ItemSelectionConfigurations_ItemSelectionConfigura~",
                        column: x => x.ItemSelectionConfigurationId,
                        principalTable: "ItemSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVotes_TransparencyConfigurations_TransparencyConfigurati~",
                        column: x => x.TransparencyConfigurationId,
                        principalTable: "TransparencyConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVotes_VoterSelectionConfigurations_VoterSelectionConfigu~",
                        column: x => x.VoterSelectionConfigurationId,
                        principalTable: "VoterSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildRoleVoterConfigurations_GuildRoleId",
                table: "GuildRoleVoterConfigurations",
                column: "GuildRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildRoleVoterConfigurations_VoterSelectionConfigurationId",
                table: "GuildRoleVoterConfigurations",
                column: "VoterSelectionConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildUserVoterConfigurations_GuildUserId",
                table: "GuildUserVoterConfigurations",
                column: "GuildUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildUserVoterConfigurations_VoterSelectionConfigurationId",
                table: "GuildUserVoterConfigurations",
                column: "VoterSelectionConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_ConflictOfInterestConfigurationId",
                table: "PreVotes",
                column: "ConflictOfInterestConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_ExpirationConfigurationId",
                table: "PreVotes",
                column: "ExpirationConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_GuildId",
                table: "PreVotes",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_ItemSelectionConfigurationId",
                table: "PreVotes",
                column: "ItemSelectionConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_TransparencyConfigurationId",
                table: "PreVotes",
                column: "TransparencyConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_VoterSelectionConfigurationId",
                table: "PreVotes",
                column: "VoterSelectionConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransparencyConfigurations_VoteVisibilityId",
                table: "TransparencyConfigurations",
                column: "VoteVisibilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildRoleVoterConfigurations");

            migrationBuilder.DropTable(
                name: "GuildUserVoterConfigurations");

            migrationBuilder.DropTable(
                name: "PreVotes");

            migrationBuilder.DropTable(
                name: "ConflictOfInterestConfigurations");

            migrationBuilder.DropTable(
                name: "ExpirationConfigurations");

            migrationBuilder.DropTable(
                name: "ItemSelectionConfigurations");

            migrationBuilder.DropTable(
                name: "TransparencyConfigurations");

            migrationBuilder.DropTable(
                name: "VoterSelectionConfigurations");

            migrationBuilder.DropTable(
                name: "VoteVisibilities");
        }
    }
}
