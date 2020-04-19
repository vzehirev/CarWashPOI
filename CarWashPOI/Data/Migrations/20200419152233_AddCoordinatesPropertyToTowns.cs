using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWashPOI.Data.Migrations
{
    public partial class AddCoordinatesPropertyToTowns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoordinatesId",
                table: "Towns",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CoordinatesId",
                table: "Towns",
                column: "CoordinatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Coordinates_CoordinatesId",
                table: "Towns",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Coordinates_CoordinatesId",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Towns_CoordinatesId",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "CoordinatesId",
                table: "Towns");
        }
    }
}
