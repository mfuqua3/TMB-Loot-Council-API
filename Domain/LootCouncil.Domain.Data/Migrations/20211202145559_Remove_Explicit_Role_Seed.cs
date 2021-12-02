using Microsoft.EntityFrameworkCore.Migrations;

namespace LootCouncil.Domain.Data.Migrations
{
    public partial class Remove_Explicit_Role_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95e21d7f-1d58-47ea-b679-dd0abe56ced7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc333931-6191-4ff2-aebb-593ef9017962");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d92407fe-7b33-4489-b887-d0e081bbe694");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d92407fe-7b33-4489-b887-d0e081bbe694", "ae76c9f4-2688-4fd2-99c5-be1b0e5d198b", "Basic", "BASIC" },
                    { "cc333931-6191-4ff2-aebb-593ef9017962", "1a67dd97-cf87-4a7d-8b07-c0706a169948", "Developer", "DEVELOPER" },
                    { "95e21d7f-1d58-47ea-b679-dd0abe56ced7", "02705a45-b258-43ee-a4e5-7a81b4ebcdff", "Admin", "ADMIN" }
                });
        }
    }
}
