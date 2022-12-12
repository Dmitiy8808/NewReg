using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class LeaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LiaderId",
                table: "Requests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    LegalDocument = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_LiaderId",
                table: "Requests",
                column: "LiaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Leaders_LiaderId",
                table: "Requests",
                column: "LiaderId",
                principalTable: "Leaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Leaders_LiaderId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "Leaders");

            migrationBuilder.DropIndex(
                name: "IX_Requests_LiaderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LiaderId",
                table: "Requests");
        }
    }
}
