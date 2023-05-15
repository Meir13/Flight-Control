using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTerminalServer.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextLeg",
                table: "Legs",
                newName: "NextLegs");

            migrationBuilder.AddColumn<bool>(
                name: "IsChangeStatus",
                table: "Legs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChangeStatus",
                table: "Legs");

            migrationBuilder.RenameColumn(
                name: "NextLegs",
                table: "Legs",
                newName: "NextLeg");
        }
    }
}
