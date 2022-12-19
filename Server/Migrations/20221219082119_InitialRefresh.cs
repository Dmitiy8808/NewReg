using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialRefresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69db4a0c-c4fb-40f5-bb9c-2ef95376abbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ca5a57b-24bb-4ec8-8e98-8de516453d73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f051e2c-07e2-4bc7-a1ad-f7e4b31c0dd6");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "67371b1f-6c3b-4b11-aacb-80339892a3d0", null, "AppAdministrator", "APPADMINISTRATOR" },
                    { "8f7f5340-3891-4adb-a71c-ada748f5286e", null, "Administrator", "ADMINISTRATOR" },
                    { "c9e1bb83-1f19-45ca-bca4-6b988923ca4a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67371b1f-6c3b-4b11-aacb-80339892a3d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f7f5340-3891-4adb-a71c-ada748f5286e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9e1bb83-1f19-45ca-bca4-6b988923ca4a");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69db4a0c-c4fb-40f5-bb9c-2ef95376abbb", null, "AppAdministrator", "APPADMINISTRATOR" },
                    { "8ca5a57b-24bb-4ec8-8e98-8de516453d73", null, "Administrator", "ADMINISTRATOR" },
                    { "8f051e2c-07e2-4bc7-a1ad-f7e4b31c0dd6", null, "User", "USER" }
                });
        }
    }
}
