using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class PreVote_Config_Seperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreVotes_ConflictOfInterestConfigurations_ConflictOfInteres~",
                table: "PreVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PreVotes_ExpirationConfigurations_ExpirationConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PreVotes_ItemSelectionConfigurations_ItemSelectionConfigura~",
                table: "PreVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PreVotes_TransparencyConfigurations_TransparencyConfigurati~",
                table: "PreVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PreVotes_VoterSelectionConfigurations_VoterSelectionConfigu~",
                table: "PreVotes");

            migrationBuilder.DropIndex(
                name: "IX_PreVotes_ConflictOfInterestConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropIndex(
                name: "IX_PreVotes_ExpirationConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropIndex(
                name: "IX_PreVotes_ItemSelectionConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropIndex(
                name: "IX_PreVotes_TransparencyConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropColumn(
                name: "ConflictOfInterestConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropColumn(
                name: "ExpirationConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropColumn(
                name: "ItemSelectionConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropColumn(
                name: "TransparencyConfigurationId",
                table: "PreVotes");

            migrationBuilder.RenameColumn(
                name: "VoterSelectionConfigurationId",
                table: "PreVotes",
                newName: "PreVoteConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_PreVotes_VoterSelectionConfigurationId",
                table: "PreVotes",
                newName: "IX_PreVotes_PreVoteConfigurationId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "PreVotes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "PreVotes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Imports",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Imports",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Guilds",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Guilds",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "GuildConfiguration",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "GuildConfiguration",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationTime",
                table: "ExpirationConfigurations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "DiscordIdentity",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "DiscordIdentity",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "CharacterItems",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CharacterItems",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "CharacterItems",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateTable(
                name: "PreVoteConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpirationConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    ItemSelectionConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    VoterSelectionConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    ConflictOfInterestConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    TransparencyConfigurationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreVoteConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteConfigurations_ConflictOfInterestConfigurations_Conf~",
                        column: x => x.ConflictOfInterestConfigurationId,
                        principalTable: "ConflictOfInterestConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfigurations_ExpirationConfigurations_ExpirationCo~",
                        column: x => x.ExpirationConfigurationId,
                        principalTable: "ExpirationConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfigurations_ItemSelectionConfigurations_ItemSelec~",
                        column: x => x.ItemSelectionConfigurationId,
                        principalTable: "ItemSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfigurations_TransparencyConfigurations_Transparen~",
                        column: x => x.TransparencyConfigurationId,
                        principalTable: "TransparencyConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfigurations_VoterSelectionConfigurations_VoterSel~",
                        column: x => x.VoterSelectionConfigurationId,
                        principalTable: "VoterSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PreVoteConfigurations",
                columns: new[] { "Id", "ConflictOfInterestConfigurationId", "ExpirationConfigurationId", "ItemSelectionConfigurationId", "TransparencyConfigurationId", "VoterSelectionConfigurationId" },
                values: new object[] { 1, 1, 1, 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfigurations_ConflictOfInterestConfigurationId",
                table: "PreVoteConfigurations",
                column: "ConflictOfInterestConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfigurations_ExpirationConfigurationId",
                table: "PreVoteConfigurations",
                column: "ExpirationConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfigurations_ItemSelectionConfigurationId",
                table: "PreVoteConfigurations",
                column: "ItemSelectionConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfigurations_TransparencyConfigurationId",
                table: "PreVoteConfigurations",
                column: "TransparencyConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfigurations_VoterSelectionConfigurationId",
                table: "PreVoteConfigurations",
                column: "VoterSelectionConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreVotes_PreVoteConfigurations_PreVoteConfigurationId",
                table: "PreVotes",
                column: "PreVoteConfigurationId",
                principalTable: "PreVoteConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreVotes_PreVoteConfigurations_PreVoteConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropTable(
                name: "PreVoteConfigurations");

            migrationBuilder.RenameColumn(
                name: "PreVoteConfigurationId",
                table: "PreVotes",
                newName: "VoterSelectionConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_PreVotes_PreVoteConfigurationId",
                table: "PreVotes",
                newName: "IX_PreVotes_VoterSelectionConfigurationId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "PreVotes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "PreVotes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "ConflictOfInterestConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ExpirationConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ItemSelectionConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TransparencyConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Imports",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Imports",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Guilds",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Guilds",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "GuildConfiguration",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "GuildConfiguration",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationTime",
                table: "ExpirationConfigurations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "DiscordIdentity",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "DiscordIdentity",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "CharacterItems",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CharacterItems",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "CharacterItems",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_ConflictOfInterestConfigurationId",
                table: "PreVotes",
                column: "ConflictOfInterestConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_ExpirationConfigurationId",
                table: "PreVotes",
                column: "ExpirationConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_ItemSelectionConfigurationId",
                table: "PreVotes",
                column: "ItemSelectionConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVotes_TransparencyConfigurationId",
                table: "PreVotes",
                column: "TransparencyConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreVotes_ConflictOfInterestConfigurations_ConflictOfInteres~",
                table: "PreVotes",
                column: "ConflictOfInterestConfigurationId",
                principalTable: "ConflictOfInterestConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreVotes_ExpirationConfigurations_ExpirationConfigurationId",
                table: "PreVotes",
                column: "ExpirationConfigurationId",
                principalTable: "ExpirationConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreVotes_ItemSelectionConfigurations_ItemSelectionConfigura~",
                table: "PreVotes",
                column: "ItemSelectionConfigurationId",
                principalTable: "ItemSelectionConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreVotes_TransparencyConfigurations_TransparencyConfigurati~",
                table: "PreVotes",
                column: "TransparencyConfigurationId",
                principalTable: "TransparencyConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreVotes_VoterSelectionConfigurations_VoterSelectionConfigu~",
                table: "PreVotes",
                column: "VoterSelectionConfigurationId",
                principalTable: "VoterSelectionConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
