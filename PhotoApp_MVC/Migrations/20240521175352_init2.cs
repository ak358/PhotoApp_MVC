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
            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 22, 2, 53, 52, 151, DateTimeKind.Local).AddTicks(8926), "~/images/e4a7662d-9cfa-44e4-b686-dd4216338b43_bird_shimaenaga.png", new DateTime(2024, 5, 22, 2, 53, 52, 151, DateTimeKind.Local).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 22, 2, 53, 52, 151, DateTimeKind.Local).AddTicks(8941), "~/images/e57e5036-abf4-4431-bc97-3215f0a88c5b_animal_chara_radio_penguin.png", new DateTime(2024, 5, 22, 2, 53, 52, 151, DateTimeKind.Local).AddTicks(8941) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 22, 2, 24, 9, 556, DateTimeKind.Local).AddTicks(7982), "images/e4a7662d-9cfa-44e4-b686-dd4216338b43_bird_shimaenaga.png", new DateTime(2024, 5, 22, 2, 24, 9, 556, DateTimeKind.Local).AddTicks(7992) });

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 22, 2, 24, 9, 556, DateTimeKind.Local).AddTicks(7993), "images/e57e5036-abf4-4431-bc97-3215f0a88c5b_animal_chara_radio_penguin.png", new DateTime(2024, 5, 22, 2, 24, 9, 556, DateTimeKind.Local).AddTicks(7994) });
        }
    }
}
