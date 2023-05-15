using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTerminalServer.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Leg_CurrentLegId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Pilot_pilotId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Leg");

            migrationBuilder.DropIndex(
                name: "IX_Flights_CurrentLegId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CurrentLegId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "pilotId",
                table: "Flights",
                newName: "PilotId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_pilotId",
                table: "Flights",
                newName: "IX_Flights_PilotId");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Pilot_PilotId",
                table: "Flights",
                column: "PilotId",
                principalTable: "Pilot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Pilot_PilotId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "PilotId",
                table: "Flights",
                newName: "pilotId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_PilotId",
                table: "Flights",
                newName: "IX_Flights_pilotId");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CurrentLegId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CrossingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leg", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CurrentLegId",
                table: "Flights",
                column: "CurrentLegId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Leg_CurrentLegId",
                table: "Flights",
                column: "CurrentLegId",
                principalTable: "Leg",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Pilot_pilotId",
                table: "Flights",
                column: "pilotId",
                principalTable: "Pilot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
