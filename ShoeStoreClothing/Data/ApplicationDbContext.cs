using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Models;
namespace ShoeStoreClothing.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Brand> Brands { get; set; }   
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }  
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }    
        public DbSet<ProductIMEI> ProductIMEIs { get; set; }  
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Blog> Blogs { get; set; } 
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Replies> Replies { get; set; } 
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountDetail> DiscountDetails { get; set; }
    }
}
