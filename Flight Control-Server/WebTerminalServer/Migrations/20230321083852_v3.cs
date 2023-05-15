using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTerminalServer.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Pilot_PilotId",
                table: "Flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pilot",
                table: "Pilot");

            migrationBuilder.RenameTable(
                name: "Pilot",
                newName: "Pilots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pilots",
                table: "Pilots",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CrossingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegId = table.Column<int>(type: "int", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    In = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Out = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loggers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Loggers_Legs_LegId",
                        column: x => x.LegId,
                        principalTable: "Legs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loggers_FlightId",
                table: "Loggers",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Loggers_LegId",
                table: "Loggers",
                column: "LegId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Pilots_PilotId",
                table: "Flights",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Pilots_PilotId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Loggers");

            migrationBuilder.DropTable(
                name: "Legs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pilots",
                table: "Pilots");

            migrationBuilder.RenameTable(
                name: "Pilots",
                newName: "Pilot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pilot",
                table: "Pilot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Pilot_PilotId",
                table: "Flights",
                column: "PilotId",
                principalTable: "Pilot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
