using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceSportsMan.Migrations
{
    public partial class AddOrganizationToSportsManTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "SportsMen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "SportsMen");
        }
    }
}
