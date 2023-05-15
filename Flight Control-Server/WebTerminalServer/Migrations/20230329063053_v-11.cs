using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTerminalServer.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsOccupied",
                table: "Legs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOccupied",
                table: "Legs");

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
    }
}
