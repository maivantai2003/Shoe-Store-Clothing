using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStoreClothing.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_EmployeeID",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_EmployeeID",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderName",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "PurchaseOrderDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "PurchaseOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderName",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "PurchaseOrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_EmployeeID",
                table: "PurchaseOrders",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_EmployeeID",
                table: "PurchaseOrders",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
