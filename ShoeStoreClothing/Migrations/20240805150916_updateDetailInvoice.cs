using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStoreClothing.Migrations
{
    /// <inheritdoc />
    public partial class updateDetailInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionID",
                table: "Invoices");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "InvoiceDetails");

            migrationBuilder.AddColumn<int>(
                name: "PromotionID",
                table: "Invoices",
                type: "int",
                nullable: true);
        }
    }
}
