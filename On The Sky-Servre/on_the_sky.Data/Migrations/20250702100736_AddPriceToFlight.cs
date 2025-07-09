using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace on_the_sky.Data.Migrations
{
    public partial class AddPriceToFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "FlightDB",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "FlightDB");
        }
    }
}
