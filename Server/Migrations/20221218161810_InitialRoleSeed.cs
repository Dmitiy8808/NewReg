using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81bb488c-9730-4885-9318-4af399b8ea0e", null, "Administrator", "ADMINISTRATOR" },
                    { "88968981-ff71-4085-944d-aabdb0e38192", null, "AppAdministrator", "APPADMINISTRATOR" },
                    { "c6c88d09-4219-4da1-aa2b-5167e9c72d47", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81bb488c-9730-4885-9318-4af399b8ea0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88968981-ff71-4085-944d-aabdb0e38192");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6c88d09-4219-4da1-aa2b-5167e9c72d47");
        }
    }
}
