using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; } = 1;
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }  
    }
}
