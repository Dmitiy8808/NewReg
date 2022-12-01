using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Inn = table.Column<string>(type: "text", nullable: true),
                    Kpp = table.Column<string>(type: "text", nullable: true),
                    Ogrn = table.Column<string>(type: "text", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    LocationAddressId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AddressInfo_LocationAddressId",
                        column: x => x.LocationAddressId,
                        principalTable: "AddressInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationAddressId",
                table: "Companies",
                column: "LocationAddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
