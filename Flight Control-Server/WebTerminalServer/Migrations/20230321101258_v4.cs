using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTerminalServer.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Legs",
                newName: "NextLeg");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Legs",
                newName: "CurrentLeg");

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Legs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Legs_FlightId",
                table: "Legs",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Flights_FlightId",
                table: "Legs",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Flights_FlightId",
                table: "Legs");

            migrationBuilder.DropIndex(
                name: "IX_Legs_FlightId",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Legs");

            migrationBuilder.RenameColumn(
                name: "NextLeg",
                table: "Legs",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "CurrentLeg",
                table: "Legs",
                newName: "Capacity");
        }
    }
}
