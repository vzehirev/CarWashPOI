using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CarWashPOI.Data.Migrations
{
    public partial class ChangeWashLocationToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_WashLocations_WashLocationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_WashLocations_WashLocationId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "WashLocations");

            migrationBuilder.DropIndex(
                name: "IX_Images_WashLocationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Comments_WashLocationId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "WashLocationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "WashLocationId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LatLonId = table.Column<int>(nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    RatingId = table.Column<int>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_Coordinates_LatLonId",
                        column: x => x.LatLonId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_LocationId",
                table: "Images",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LocationId",
                table: "Comments",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId",
                table: "Locations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LatLonId",
                table: "Locations",
                column: "LatLonId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RatingId",
                table: "Locations",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Locations_LocationId",
                table: "Comments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Locations_LocationId",
                table: "Images",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Locations_LocationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Locations_LocationId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Images_LocationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Comments_LocationId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "WashLocationId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WashLocationId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WashLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatLonId = table.Column<int>(type: "int", nullable: true),
                    RatingId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WashLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WashLocations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WashLocations_Coordinates_LatLonId",
                        column: x => x.LatLonId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WashLocations_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_WashLocationId",
                table: "Images",
                column: "WashLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WashLocationId",
                table: "Comments",
                column: "WashLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WashLocations_AddressId",
                table: "WashLocations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_WashLocations_LatLonId",
                table: "WashLocations",
                column: "LatLonId");

            migrationBuilder.CreateIndex(
                name: "IX_WashLocations_RatingId",
                table: "WashLocations",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_WashLocations_WashLocationId",
                table: "Comments",
                column: "WashLocationId",
                principalTable: "WashLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_WashLocations_WashLocationId",
                table: "Images",
                column: "WashLocationId",
                principalTable: "WashLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
