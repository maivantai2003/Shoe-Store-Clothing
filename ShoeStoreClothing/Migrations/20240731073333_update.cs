using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStoreClothing.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "PurchaseOrderDetails");

            migrationBuilder.AddColumn<double>(
                name: "PriceSale",
                table: "ProductDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceSale",
                table: "ProductDetails");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "PurchaseOrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
