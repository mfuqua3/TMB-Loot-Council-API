using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class PreVote_Config_Defaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VoterSelectionConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "TransparencyConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ItemSelectionConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ExpirationConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ConflictOfInterestConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "ConflictOfInterestConfigurations",
                columns: new[] { "Id", "AllowCommenting", "AllowObjecting", "AllowSelfVoting", "AllowVoting" },
                values: new object[] { 1, true, false, false, false });

            migrationBuilder.InsertData(
                table: "ExpirationConfigurations",
                columns: new[] { "Id", "ExpirationTime", "LockCommentsTteMinutes", "LockObjectionsTteMinutes", "LockVotesTteMinutes" },
                values: new object[] { 1, new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "ItemSelectionConfigurations",
                columns: new[] { "Id", "SelectAll" },
                values: new object[] { 1, true });

            migrationBuilder.InsertData(
                table: "VoteVisibilities",
                columns: new[] { "Id", "AllowAllEligibleVoters", "AllowGuild", "VoteSubmissionRequirement" },
                values: new object[] { 1, true, false, 1 });

            migrationBuilder.InsertData(
                table: "VoterSelectionConfigurations",
                columns: new[] { "Id", "MaximumVotersPerItem", "MinimumVotersPerItem", "Randomize" },
                values: new object[] { 1, 5, 3, false });

            migrationBuilder.InsertData(
                table: "GuildRoleVoterConfigurations",
                columns: new[] { "Id", "GuildRoleId", "VoterSelectionConfigurationId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "TransparencyConfigurations",
                columns: new[] { "Id", "VoteVisibilityId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConflictOfInterestConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExpirationConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GuildRoleVoterConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemSelectionConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransparencyConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VoteVisibilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VoterSelectionConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "VoterSelectionConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "TransparencyConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ItemSelectionConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ExpirationConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ConflictOfInterestConfigurationId",
                table: "PreVotes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);
        }
    }
}
