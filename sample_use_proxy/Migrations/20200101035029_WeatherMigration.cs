using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sample_use_proxy.Migrations
{
    public partial class WeatherMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    TemperatureC = table.Column<int>(nullable: false),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 1, 22, 50, 29, 357, DateTimeKind.Local).AddTicks(2261), "Bracing", 2 },
                    { 2, new DateTime(2020, 1, 2, 22, 50, 29, 359, DateTimeKind.Local).AddTicks(2129), "Warm", 35 },
                    { 3, new DateTime(2020, 1, 3, 22, 50, 29, 359, DateTimeKind.Local).AddTicks(2160), "Cool", -5 },
                    { 4, new DateTime(2020, 1, 4, 22, 50, 29, 359, DateTimeKind.Local).AddTicks(2165), "Bracing", -17 },
                    { 5, new DateTime(2020, 1, 5, 22, 50, 29, 359, DateTimeKind.Local).AddTicks(2168), "Bracing", 31 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecasts");
        }
    }
}
