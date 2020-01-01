using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sample_use_proxy.Migrations
{
    public partial class WeatherMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    TemperatureC = table.Column<int>(nullable: false),
                    DescriptionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecasts_Description_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Description",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Description",
                columns: new[] { "Id", "Summary" },
                values: new object[,]
                {
                    { 1, "Freezing" },
                    { 2, "Bracing" },
                    { 3, "Chilly" },
                    { 4, "Cool" },
                    { 5, "Mild" },
                    { 6, "Warm" },
                    { 7, "Balmy" },
                    { 8, "Hot" },
                    { 9, "Sweltering" },
                    { 10, "Scorching" }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "DescriptionId", "TemperatureC" },
                values: new object[,]
                {
                    { 2, new DateTime(2020, 1, 3, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8436), 4, 8 },
                    { 4, new DateTime(2020, 1, 5, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8466), 7, 31 },
                    { 5, new DateTime(2020, 1, 6, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8468), 7, 10 },
                    { 1, new DateTime(2020, 1, 2, 0, 58, 13, 496, DateTimeKind.Local).AddTicks(9811), 8, -17 },
                    { 3, new DateTime(2020, 1, 4, 0, 58, 13, 498, DateTimeKind.Local).AddTicks(8461), 8, 39 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_DescriptionId",
                table: "Forecasts",
                column: "DescriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecasts");

            migrationBuilder.DropTable(
                name: "Description");
        }
    }
}
