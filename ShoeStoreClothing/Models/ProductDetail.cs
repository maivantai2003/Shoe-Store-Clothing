using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShoeStoreClothing.Models
{
    public class ProductDetail
    {
        [Key]
        public int ProductDetailID { get; set; }
        public int ProductID { get; set; }
        [ForeignKey(nameof(ProductID))]   
        public Product Product { get; set; }    
        public int ColorID { get; set; }
        [ForeignKey(nameof(ColorID))]
        public Color Color { get; set; }    
        public int SizeID { get; set; }
        [ForeignKey(nameof(SizeID))]    
        public Size Size { get; set; }  
        public string Image { get; set; }
        public int StockQuantity { get; set; }
        public double ProductPrice { get; set; }
        public double PriceSale { get; set; } = 0d;
        public int Status { get; set; } = 1;
        public ICollection<FavoriteProduct>? favoriteProducts { get; set; }  
        public ICollection<ProductIMEI>? ProductIMEIs { get; set; }
        public ICollection<InvoiceDetail>? invoiceDetails { get; set; }
        public ICollection<PurchaseOrderDetail>? PurchaseOrderDetails { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
    }
}
