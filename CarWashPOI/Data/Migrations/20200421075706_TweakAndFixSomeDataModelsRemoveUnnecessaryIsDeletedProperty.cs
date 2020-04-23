using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CarWashPOI.Data.Migrations
{
    public partial class TweakAndFixSomeDataModelsRemoveUnnecessaryIsDeletedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Articles_ArticleId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Locations_LocationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ArticleId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_LocationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Articles");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Locations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ImageId",
                table: "Locations",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ImageId",
                table: "Articles",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Images_ImageId",
                table: "Articles",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Images_ImageId",
                table: "Locations",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Images_ImageId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Images_ImageId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_ImageId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ImageId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Articles");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Locations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArticleId",
                table: "Images",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_LocationId",
                table: "Images",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Articles_ArticleId",
                table: "Images",
                column: "ArticleId",
                principalTable: "Articles",
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
    }
}
