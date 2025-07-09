using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace on_the_sky.Data.Migrations
{
    public partial class RenameTravelsToTravel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passengername",
                table: "TravelsDB");

            migrationBuilder.RenameColumn(
                name: "amounttickets",
                table: "TravelsDB",
                newName: "AmountTickets");

            migrationBuilder.RenameColumn(
                name: "passengerid",
                table: "TravelsDB",
                newName: "TravelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountTickets",
                table: "TravelsDB",
                newName: "amounttickets");

            migrationBuilder.RenameColumn(
                name: "TravelId",
                table: "TravelsDB",
                newName: "passengerid");

            migrationBuilder.AddColumn<string>(
                name: "passengername",
                table: "TravelsDB",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
