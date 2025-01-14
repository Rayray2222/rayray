using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShop.Migrations
{
    /// <inheritdoc />
    public partial class AddedPurchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.CreateTable(
                name: "purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicycleId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchases_Bicycles_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchases_BicycleId",
                table: "purchases",
                column: "BicycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "Id", "Brand", "Color", "ImageFileName", "Model", "Price", "Type", "Year" },
                values: new object[,]
                {
                    { 6, "Giant", "Silver", "giant-escape-silver.jpg", "Escape 3", 400.00m, "Hybrid", 2023 },
                    { 7, "Specialized", "Black", "specialized-sirrus-black.jpg", "Sirrus X 3.0", 700.00m, "Hybrid", 2022 },
                    { 8, "Cannondale", "Orange", "cannondale-topstone-orange.jpg", "Topstone Carbon Lefty 3", 3000.00m, "Gravel", 2023 },
                    { 9, "Trek", "Blue", "trek-domane-blue.jpg", "Domane SL 5", 2500.00m, "Road", 2023 },
                    { 10, "Giant", "Yellow", "giant-talon-yellow.jpg", "Talon 29 3", 700.00m, "Mountain", 2022 }
                });
        }
    }
}
