using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reg.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileModelIDchangeToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "Files",
                newName: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Files",
                newName: "TypeID");
        }
    }
}
