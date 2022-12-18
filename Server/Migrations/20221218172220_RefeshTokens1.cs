using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class RefeshTokens1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b4d3a27-61d3-4a44-b82a-faccec1b8bd1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49575e48-0ee0-4536-a82c-9236c4a53b55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65223553-e956-4784-a5a6-811f55676aa6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77425029-cf1f-412c-8af1-9a19f56c4d05", null, "AppAdministrator", "APPADMINISTRATOR" },
                    { "7fe0569a-3656-4a3e-b2f8-0f533b217e79", null, "Administrator", "ADMINISTRATOR" },
                    { "9dfbf7c1-34ce-40ba-9a71-0c7b1b7c1538", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77425029-cf1f-412c-8af1-9a19f56c4d05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fe0569a-3656-4a3e-b2f8-0f533b217e79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dfbf7c1-34ce-40ba-9a71-0c7b1b7c1538");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b4d3a27-61d3-4a44-b82a-faccec1b8bd1", null, "Administrator", "ADMINISTRATOR" },
                    { "49575e48-0ee0-4536-a82c-9236c4a53b55", null, "AppAdministrator", "APPADMINISTRATOR" },
                    { "65223553-e956-4784-a5a6-811f55676aa6", null, "User", "USER" }
                });
        }
    }
}
