using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class LeaderTableChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Leaders_LiaderId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "LiaderId",
                table: "Requests",
                newName: "LeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_LiaderId",
                table: "Requests",
                newName: "IX_Requests_LeaderId");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Leaders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Leaders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LegalDocument",
                table: "Leaders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Leaders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Leaders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Leaders_LeaderId",
                table: "Requests",
                column: "LeaderId",
                principalTable: "Leaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Leaders_LeaderId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "LeaderId",
                table: "Requests",
                newName: "LiaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_LeaderId",
                table: "Requests",
                newName: "IX_Requests_LiaderId");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Leaders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Leaders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LegalDocument",
                table: "Leaders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Leaders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Leaders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Leaders_LiaderId",
                table: "Requests",
                column: "LiaderId",
                principalTable: "Leaders",
                principalColumn: "Id");
        }
    }
}
