using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace grow.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Light",
                columns: table => new
                {
                    LightId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Requirements = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Light", x => x.LightId);
                });

            migrationBuilder.CreateTable(
                name: "PlantType",
                columns: table => new
                {
                    PlantTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantType", x => x.PlantTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Water",
                columns: table => new
                {
                    WaterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Regularity = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Water", x => x.WaterId);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    PlantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    InitialImage = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    PlantTypeId = table.Column<int>(nullable: false),
                    LightId = table.Column<int>(nullable: true),
                    WaterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_Plant_Light_LightId",
                        column: x => x.LightId,
                        principalTable: "Light",
                        principalColumn: "LightId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plant_PlantType_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantType",
                        principalColumn: "PlantTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plant_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plant_Water_WaterId",
                        column: x => x.WaterId,
                        principalTable: "Water",
                        principalColumn: "WaterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantAudit",
                columns: table => new
                {
                    PlantAuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PlantId = table.Column<int>(nullable: false),
                    WaterId = table.Column<int>(nullable: false),
                    LightId = table.Column<int>(nullable: false),
                    RequirementsChanged = table.Column<bool>(nullable: false),
                    InfestationIssue = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    UpdatedImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantAudit", x => x.PlantAuditId);
                    table.ForeignKey(
                        name: "FK_PlantAudit_Light_LightId",
                        column: x => x.LightId,
                        principalTable: "Light",
                        principalColumn: "LightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantAudit_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantAudit_Water_WaterId",
                        column: x => x.WaterId,
                        principalTable: "Water",
                        principalColumn: "WaterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plant_LightId",
                table: "Plant",
                column: "LightId");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_PlantTypeId",
                table: "Plant",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_UserId",
                table: "Plant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_WaterId",
                table: "Plant",
                column: "WaterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantAudit_LightId",
                table: "PlantAudit",
                column: "LightId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantAudit_PlantId",
                table: "PlantAudit",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantAudit_WaterId",
                table: "PlantAudit",
                column: "WaterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantAudit");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "Light");

            migrationBuilder.DropTable(
                name: "PlantType");

            migrationBuilder.DropTable(
                name: "Water");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
