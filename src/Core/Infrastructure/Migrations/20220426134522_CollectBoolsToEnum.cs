using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAT154Oblig4.Infrastructure.Migrations
{
    public partial class CollectBoolsToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckedIn",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckedOut",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CheckedIn",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CheckedOut",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
