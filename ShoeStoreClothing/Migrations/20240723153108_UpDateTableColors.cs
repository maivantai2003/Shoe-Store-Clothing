using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStoreClothing.Migrations
{
    /// <inheritdoc />
    public partial class UpDateTableColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Color_ColorID",
                table: "ProductDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Color",
                table: "Color");

            migrationBuilder.RenameTable(
                name: "Color",
                newName: "Colors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "ColorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Colors_ColorID",
                table: "ProductDetails",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Colors_ColorID",
                table: "ProductDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Color");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Color",
                table: "Color",
                column: "ColorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Color_ColorID",
                table: "ProductDetails",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
