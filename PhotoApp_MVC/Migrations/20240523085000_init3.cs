using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp_MVC.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 17, 50, 0, 101, DateTimeKind.Local).AddTicks(3638), new DateTime(2024, 5, 23, 17, 50, 0, 101, DateTimeKind.Local).AddTicks(3649) });

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 17, 50, 0, 101, DateTimeKind.Local).AddTicks(3651), new DateTime(2024, 5, 23, 17, 50, 0, 101, DateTimeKind.Local).AddTicks(3651) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8127), new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8138) });

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8140), new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8140) });
        }
    }
}
