using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWashPOI.Data.Migrations
{
    public partial class ChangeLatLonToCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Coordinates_LatLonId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_LatLonId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LatLonId",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "CoordinatesId",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CoordinatesId",
                table: "Locations",
                column: "CoordinatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Coordinates_CoordinatesId",
                table: "Locations",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Coordinates_CoordinatesId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CoordinatesId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CoordinatesId",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "LatLonId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LatLonId",
                table: "Locations",
                column: "LatLonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Coordinates_LatLonId",
                table: "Locations",
                column: "LatLonId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
