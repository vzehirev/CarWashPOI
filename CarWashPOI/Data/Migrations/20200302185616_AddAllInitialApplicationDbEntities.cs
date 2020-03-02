using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWashPOI.Data.Migrations
{
    public partial class AddAllInitialApplicationDbEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_Rating_RatingId",
                table: "UserRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId",
                table: "UserRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "UserRating",
                newName: "UsersRatings");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_UserRating_RatingId",
                table: "UsersRatings",
                newName: "IX_UsersRatings_RatingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersRatings",
                table: "UsersRatings",
                columns: new[] { "UserId", "RatingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Town = table.Column<string>(nullable: true),
                    Neighbourhood = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Views = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WashLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<int>(nullable: false),
                    LatLonId = table.Column<int>(nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    RatingId = table.Column<int>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    WashLocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_WashLocations_WashLocationId",
                        column: x => x.WashLocationId,
                        principalTable: "WashLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArticleId = table.Column<int>(nullable: true),
                    WashLocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_WashLocations_WashLocationId",
                        column: x => x.WashLocationId,
                        principalTable: "WashLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WashLocationId",
                table: "Comments",
                column: "WashLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArticleId",
                table: "Images",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_WashLocationId",
                table: "Images",
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
                name: "FK_UsersRatings_Ratings_RatingId",
                table: "UsersRatings",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRatings_AspNetUsers_UserId",
                table: "UsersRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRatings_Ratings_RatingId",
                table: "UsersRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRatings_AspNetUsers_UserId",
                table: "UsersRatings");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "WashLocations");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersRatings",
                table: "UsersRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "UsersRatings",
                newName: "UserRating");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_UsersRatings_RatingId",
                table: "UserRating",
                newName: "IX_UserRating_RatingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating",
                columns: new[] { "UserId", "RatingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_Rating_RatingId",
                table: "UserRating",
                column: "RatingId",
                principalTable: "Rating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId",
                table: "UserRating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
