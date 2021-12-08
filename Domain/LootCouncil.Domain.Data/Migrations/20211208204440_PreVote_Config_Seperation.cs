using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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

            migrationBuilder.CreateTable(
                name: "PreVoteConfiguration",
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
                    table.PrimaryKey("PK_PreVoteConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreVoteConfiguration_ConflictOfInterestConfigurations_Confl~",
                        column: x => x.ConflictOfInterestConfigurationId,
                        principalTable: "ConflictOfInterestConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfiguration_ExpirationConfigurations_ExpirationCon~",
                        column: x => x.ExpirationConfigurationId,
                        principalTable: "ExpirationConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfiguration_ItemSelectionConfigurations_ItemSelect~",
                        column: x => x.ItemSelectionConfigurationId,
                        principalTable: "ItemSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfiguration_TransparencyConfigurations_Transparenc~",
                        column: x => x.TransparencyConfigurationId,
                        principalTable: "TransparencyConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreVoteConfiguration_VoterSelectionConfigurations_VoterSele~",
                        column: x => x.VoterSelectionConfigurationId,
                        principalTable: "VoterSelectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PreVoteConfiguration",
                columns: new[] { "Id", "ConflictOfInterestConfigurationId", "ExpirationConfigurationId", "ItemSelectionConfigurationId", "TransparencyConfigurationId", "VoterSelectionConfigurationId" },
                values: new object[] { 1, 1, 1, 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfiguration_ConflictOfInterestConfigurationId",
                table: "PreVoteConfiguration",
                column: "ConflictOfInterestConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfiguration_ExpirationConfigurationId",
                table: "PreVoteConfiguration",
                column: "ExpirationConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfiguration_ItemSelectionConfigurationId",
                table: "PreVoteConfiguration",
                column: "ItemSelectionConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfiguration_TransparencyConfigurationId",
                table: "PreVoteConfiguration",
                column: "TransparencyConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreVoteConfiguration_VoterSelectionConfigurationId",
                table: "PreVoteConfiguration",
                column: "VoterSelectionConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreVotes_PreVoteConfiguration_PreVoteConfigurationId",
                table: "PreVotes",
                column: "PreVoteConfigurationId",
                principalTable: "PreVoteConfiguration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreVotes_PreVoteConfiguration_PreVoteConfigurationId",
                table: "PreVotes");

            migrationBuilder.DropTable(
                name: "PreVoteConfiguration");

            migrationBuilder.RenameColumn(
                name: "PreVoteConfigurationId",
                table: "PreVotes",
                newName: "VoterSelectionConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_PreVotes_PreVoteConfigurationId",
                table: "PreVotes",
                newName: "IX_PreVotes_VoterSelectionConfigurationId");

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
