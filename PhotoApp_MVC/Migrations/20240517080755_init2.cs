using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp_MVC.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "imageURL",
                table: "PhotoPosts",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PhotoPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PhotoPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Title", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 17, 8, 7, 54, 495, DateTimeKind.Utc).AddTicks(9795), "This is a sample description", "sample-url.jpg", "Sample Post", new DateTime(2024, 5, 17, 8, 7, 54, 495, DateTimeKind.Utc).AddTicks(9797) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PhotoPosts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PhotoPosts");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "PhotoPosts",
                newName: "imageURL");

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title", "imageURL" },
                values: new object[] { "Description 1", "Post 1", "imageURL 1" });

            migrationBuilder.InsertData(
                table: "PhotoPosts",
                columns: new[] { "Id", "CategoryId", "Description", "Title", "UserId", "imageURL" },
                values: new object[] { 2, 2, "Description 2", "Post 2", 2, "imageURL 2" });
        }
    }
}
