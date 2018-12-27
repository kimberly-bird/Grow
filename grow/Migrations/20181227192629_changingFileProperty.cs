using Microsoft.EntityFrameworkCore.Migrations;

namespace grow.Migrations
{
    public partial class changingFileProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedImage",
                table: "PlantAudit");

            migrationBuilder.DropColumn(
                name: "InitialImage",
                table: "Plant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdatedImage",
                table: "PlantAudit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InitialImage",
                table: "Plant",
                nullable: true);
        }
    }
}
