using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStoreClothing.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableDiscountDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Discounts_DiscountId1",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_DiscountId1",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "DiscountId1",
                table: "Discounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId1",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountId1",
                table: "Discounts",
                column: "DiscountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Discounts_DiscountId1",
                table: "Discounts",
                column: "DiscountId1",
                principalTable: "Discounts",
                principalColumn: "DiscountId");
        }
    }
}
