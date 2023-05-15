using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTerminalServer.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrossingTimeSeconds",
                table: "Legs",
                newName: "CrossingTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrossingTime",
                table: "Legs",
                newName: "CrossingTimeSeconds");
        }
    }
}
