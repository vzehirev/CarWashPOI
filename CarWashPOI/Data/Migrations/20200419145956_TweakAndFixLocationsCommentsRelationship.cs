using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWashPOI.Data.Migrations
{
    public partial class TweakAndFixLocationsCommentsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Locations_LocationId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Locations_LocationId",
                table: "Comments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Locations_LocationId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Locations_LocationId",
                table: "Comments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
