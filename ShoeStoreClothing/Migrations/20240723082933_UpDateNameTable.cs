using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStoreClothing.Migrations
{
    /// <inheritdoc />
    public partial class UpDateNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProduct_AspNetUsers_CustomerID",
                table: "FavoriteProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProduct_ProductDetail_ProductDetailID",
                table: "FavoriteProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_CustomerID",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetail_Invoice_InvoiceID",
                table: "InvoiceDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetail_ProductDetail_ProductDetailID",
                table: "InvoiceDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Material_MaterialID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_Color_ColorID",
                table: "ProductDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_Product_ProductID",
                table: "ProductDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_Size_SizeID",
                table: "ProductDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIMEI_ProductDetail_ProductDetailID",
                table: "ProductIMEI");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_AspNetUsers_EmployeeID",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierID",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetail_ProductDetail_ProductDetailID",
                table: "PurchaseOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetail_PurchaseOrder_PurchaseOrderID",
                table: "PurchaseOrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Size",
                table: "Size");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrderDetail",
                table: "PurchaseOrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrder",
                table: "PurchaseOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductIMEI",
                table: "ProductIMEI");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDetail",
                table: "ProductDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetail",
                table: "InvoiceDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteProduct",
                table: "FavoriteProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Size",
                newName: "Sizes");

            migrationBuilder.RenameTable(
                name: "PurchaseOrderDetail",
                newName: "PurchaseOrderDetails");

            migrationBuilder.RenameTable(
                name: "PurchaseOrder",
                newName: "PurchaseOrders");

            migrationBuilder.RenameTable(
                name: "ProductIMEI",
                newName: "ProductIMEIs");

            migrationBuilder.RenameTable(
                name: "ProductDetail",
                newName: "ProductDetails");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "InvoiceDetail",
                newName: "InvoiceDetails");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameTable(
                name: "FavoriteProduct",
                newName: "FavoriteProducts");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetail_PurchaseOrderID",
                table: "PurchaseOrderDetails",
                newName: "IX_PurchaseOrderDetails_PurchaseOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetail_ProductDetailID",
                table: "PurchaseOrderDetails",
                newName: "IX_PurchaseOrderDetails_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_SupplierID",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_EmployeeID",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductIMEI_ProductDetailID",
                table: "ProductIMEIs",
                newName: "IX_ProductIMEIs_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetail_SizeID",
                table: "ProductDetails",
                newName: "IX_ProductDetails_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetail_ProductID",
                table: "ProductDetails",
                newName: "IX_ProductDetails_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetail_ColorID",
                table: "ProductDetails",
                newName: "IX_ProductDetails_ColorID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_MaterialID",
                table: "Products",
                newName: "IX_Products_MaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryID",
                table: "Products",
                newName: "IX_Products_CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandID",
                table: "Products",
                newName: "IX_Products_BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetail_ProductDetailID",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetail_InvoiceID",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_CustomerID",
                table: "Invoices",
                newName: "IX_Invoices_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProduct_ProductDetailID",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProduct_CustomerID",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "SupplierID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes",
                column: "SizeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrderDetails",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderDetailID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders",
                column: "PurchaseOrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductIMEIs",
                table: "ProductIMEIs",
                column: "IMEICode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDetails",
                table: "ProductDetails",
                column: "ProductDetailID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "MaterialID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails",
                column: "InvoiceDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteProducts",
                table: "FavoriteProducts",
                column: "FavoriteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_AspNetUsers_CustomerID",
                table: "FavoriteProducts",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_ProductDetails_ProductDetailID",
                table: "FavoriteProducts",
                column: "ProductDetailID",
                principalTable: "ProductDetails",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceID",
                table: "InvoiceDetails",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_ProductDetails_ProductDetailID",
                table: "InvoiceDetails",
                column: "ProductDetailID",
                principalTable: "ProductDetails",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_CustomerID",
                table: "Invoices",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Color_ColorID",
                table: "ProductDetails",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products_ProductID",
                table: "ProductDetails",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Sizes_SizeID",
                table: "ProductDetails",
                column: "SizeID",
                principalTable: "Sizes",
                principalColumn: "SizeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIMEIs_ProductDetails_ProductDetailID",
                table: "ProductIMEIs",
                column: "ProductDetailID",
                principalTable: "ProductDetails",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandID",
                table: "Products",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Materials_MaterialID",
                table: "Products",
                column: "MaterialID",
                principalTable: "Materials",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_ProductDetails_ProductDetailID",
                table: "PurchaseOrderDetails",
                column: "ProductDetailID",
                principalTable: "ProductDetails",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderID",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderID",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_EmployeeID",
                table: "PurchaseOrders",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierID",
                table: "PurchaseOrders",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_AspNetUsers_CustomerID",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_ProductDetails_ProductDetailID",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceID",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_ProductDetails_ProductDetailID",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_CustomerID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Color_ColorID",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products_ProductID",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Sizes_SizeID",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIMEIs_ProductDetails_ProductDetailID",
                table: "ProductIMEIs");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Materials_MaterialID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_ProductDetails_ProductDetailID",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderID",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_EmployeeID",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierID",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrderDetails",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductIMEIs",
                table: "ProductIMEIs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDetails",
                table: "ProductDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteProducts",
                table: "FavoriteProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "Sizes",
                newName: "Size");

            migrationBuilder.RenameTable(
                name: "PurchaseOrders",
                newName: "PurchaseOrder");

            migrationBuilder.RenameTable(
                name: "PurchaseOrderDetails",
                newName: "PurchaseOrderDetail");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductIMEIs",
                newName: "ProductIMEI");

            migrationBuilder.RenameTable(
                name: "ProductDetails",
                newName: "ProductDetail");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameTable(
                name: "InvoiceDetails",
                newName: "InvoiceDetail");

            migrationBuilder.RenameTable(
                name: "FavoriteProducts",
                newName: "FavoriteProduct");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_SupplierID",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_EmployeeID",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrderID",
                table: "PurchaseOrderDetail",
                newName: "IX_PurchaseOrderDetail_PurchaseOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetails_ProductDetailID",
                table: "PurchaseOrderDetail",
                newName: "IX_PurchaseOrderDetail_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_MaterialID",
                table: "Product",
                newName: "IX_Product_MaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryID",
                table: "Product",
                newName: "IX_Product_CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandID",
                table: "Product",
                newName: "IX_Product_BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductIMEIs_ProductDetailID",
                table: "ProductIMEI",
                newName: "IX_ProductIMEI_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetails_SizeID",
                table: "ProductDetail",
                newName: "IX_ProductDetail_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetails_ProductID",
                table: "ProductDetail",
                newName: "IX_ProductDetail_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetails_ColorID",
                table: "ProductDetail",
                newName: "IX_ProductDetail_ColorID");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_CustomerID",
                table: "Invoice",
                newName: "IX_Invoice_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_ProductDetailID",
                table: "InvoiceDetail",
                newName: "IX_InvoiceDetail_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InvoiceID",
                table: "InvoiceDetail",
                newName: "IX_InvoiceDetail_InvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_ProductDetailID",
                table: "FavoriteProduct",
                newName: "IX_FavoriteProduct_ProductDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_CustomerID",
                table: "FavoriteProduct",
                newName: "IX_FavoriteProduct_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "SupplierID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Size",
                table: "Size",
                column: "SizeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrder",
                table: "PurchaseOrder",
                column: "PurchaseOrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrderDetail",
                table: "PurchaseOrderDetail",
                column: "PurchaseOrderDetailID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductIMEI",
                table: "ProductIMEI",
                column: "IMEICode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDetail",
                table: "ProductDetail",
                column: "ProductDetailID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "MaterialID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "InvoiceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetail",
                table: "InvoiceDetail",
                column: "InvoiceDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteProduct",
                table: "FavoriteProduct",
                column: "FavoriteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProduct_AspNetUsers_CustomerID",
                table: "FavoriteProduct",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProduct_ProductDetail_ProductDetailID",
                table: "FavoriteProduct",
                column: "ProductDetailID",
                principalTable: "ProductDetail",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_CustomerID",
                table: "Invoice",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetail_Invoice_InvoiceID",
                table: "InvoiceDetail",
                column: "InvoiceID",
                principalTable: "Invoice",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetail_ProductDetail_ProductDetailID",
                table: "InvoiceDetail",
                column: "ProductDetailID",
                principalTable: "ProductDetail",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandID",
                table: "Product",
                column: "BrandID",
                principalTable: "Brand",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Material_MaterialID",
                table: "Product",
                column: "MaterialID",
                principalTable: "Material",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_Color_ColorID",
                table: "ProductDetail",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_Product_ProductID",
                table: "ProductDetail",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_Size_SizeID",
                table: "ProductDetail",
                column: "SizeID",
                principalTable: "Size",
                principalColumn: "SizeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIMEI_ProductDetail_ProductDetailID",
                table: "ProductIMEI",
                column: "ProductDetailID",
                principalTable: "ProductDetail",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_AspNetUsers_EmployeeID",
                table: "PurchaseOrder",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierID",
                table: "PurchaseOrder",
                column: "SupplierID",
                principalTable: "Supplier",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetail_ProductDetail_ProductDetailID",
                table: "PurchaseOrderDetail",
                column: "ProductDetailID",
                principalTable: "ProductDetail",
                principalColumn: "ProductDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetail_PurchaseOrder_PurchaseOrderID",
                table: "PurchaseOrderDetail",
                column: "PurchaseOrderID",
                principalTable: "PurchaseOrder",
                principalColumn: "PurchaseOrderID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
