using Microsoft.EntityFrameworkCore.Migrations;

namespace grow.Migrations
{
    public partial class addPlantsToWater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WaterId",
                table: "Plant",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plant_WaterId",
                table: "Plant",
                column: "WaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_Water_WaterId",
                table: "Plant",
                column: "WaterId",
                principalTable: "Water",
                principalColumn: "WaterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plant_Water_WaterId",
                table: "Plant");

            migrationBuilder.DropIndex(
                name: "IX_Plant_WaterId",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "WaterId",
                table: "Plant");
        }
    }
}
