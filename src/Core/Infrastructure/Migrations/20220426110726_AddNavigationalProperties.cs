using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAT154Oblig4.Infrastructure.Migrations
{
    public partial class AddNavigationalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceTasks");
        }
    }
}
