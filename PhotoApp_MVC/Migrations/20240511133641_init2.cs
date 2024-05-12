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
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PhotoPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "PhotoPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "imageURL" },
                values: new object[] { "Description 1", "imageURL 1" });

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "imageURL" },
                values: new object[] { "Description 2", "imageURL 2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PhotoPosts");

            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "PhotoPosts");
        }
    }
}
