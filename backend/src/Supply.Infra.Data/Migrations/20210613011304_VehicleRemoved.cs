using Microsoft.EntityFrameworkCore.Migrations;

namespace Supply.Infra.Data.Migrations
{
    public partial class VehicleRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Vehicles");
        }
    }
}
