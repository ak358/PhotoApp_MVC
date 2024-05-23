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
                values: new object[] { new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8127), "images/bird_shimaenaga.png", new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8138) });

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8140), "images/animal_chara_radio_penguin.png", new DateTime(2024, 5, 23, 17, 28, 35, 397, DateTimeKind.Local).AddTicks(8140) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "password");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 17, 1, 15, 355, DateTimeKind.Local).AddTicks(1151), "images/e4a7662d-9cfa-44e4-b686-dd4216338b43_bird_shimaenaga.png", new DateTime(2024, 5, 23, 17, 1, 15, 355, DateTimeKind.Local).AddTicks(1164) });

            migrationBuilder.UpdateData(
                table: "PhotoPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 17, 1, 15, 355, DateTimeKind.Local).AddTicks(1166), "images/e57e5036-abf4-4431-bc97-3215f0a88c5b_animal_chara_radio_penguin.png", new DateTime(2024, 5, 23, 17, 1, 15, 355, DateTimeKind.Local).AddTicks(1167) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "adminpassword");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "userpassword");
        }
    }
}
