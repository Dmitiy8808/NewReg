using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class RefeshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
