using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace on_the_sky.Data.Migrations
{
    public partial class fixit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlacesDB",
                columns: table => new
                {
                    countryid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacesDB", x => x.countryid);
                });

            migrationBuilder.CreateTable(
                name: "UsersDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightDB",
                columns: table => new
                {
                    flightid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flighttime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    countryID = table.Column<int>(type: "int", nullable: false),
                    Maximum = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightDB", x => x.flightid);
                    table.ForeignKey(
                        name: "FK_FlightDB_PlacesDB_countryID",
                        column: x => x.countryID,
                        principalTable: "PlacesDB",
                        principalColumn: "countryid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelsDB",
                columns: table => new
                {
                    passengerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    passengername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amounttickets = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelsDB", x => x.passengerid);
                    table.ForeignKey(
                        name: "FK_TravelsDB_FlightDB_FlightId",
                        column: x => x.FlightId,
                        principalTable: "FlightDB",
                        principalColumn: "flightid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelsDB_UsersDB_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightDB_countryID",
                table: "FlightDB",
                column: "countryID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelsDB_FlightId",
                table: "TravelsDB",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelsDB_UserId",
                table: "TravelsDB",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelsDB");

            migrationBuilder.DropTable(
                name: "FlightDB");

            migrationBuilder.DropTable(
                name: "UsersDB");

            migrationBuilder.DropTable(
                name: "PlacesDB");
        }
    }
}
