using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStoreClothing.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DiscountId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discounts_Discounts_DiscountId1",
                        column: x => x.DiscountId1,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId");
                });

            migrationBuilder.CreateTable(
                name: "DiscountDetails",
                columns: table => new
                {
                    DiscountDetailId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    ProductDetailId = table.Column<int>(type: "int", nullable: false),
                    PriceDiscount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountDetails", x => x.DiscountDetailId);
                    table.ForeignKey(
                        name: "FK_DiscountDetails_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountDetails_ProductDetails_DiscountDetailId",
                        column: x => x.DiscountDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "ProductDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountDetails_DiscountId",
                table: "DiscountDetails",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountId1",
                table: "Discounts",
                column: "DiscountId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountDetails");

            migrationBuilder.DropTable(
                name: "Discounts");
        }
    }
}
