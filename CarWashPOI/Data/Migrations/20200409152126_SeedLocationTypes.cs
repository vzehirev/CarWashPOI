using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWashPOI.Data.Migrations
{
    public partial class SeedLocationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LocationTypes",
                column: "Type",
                values: new object[]
                {
                    "На самообслужване",
                    "С персонал",
                    "Комбинирана",
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationTypes",
                keyColumn: "Type",
                keyValues: new object[]
                {
                    "На самообслужване",
                    "С персонал",
                    "Комбинирана",
                });
        }
    }
}
