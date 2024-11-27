using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderID { get; set; }
        public int SupplierID { get; set; }
        [ForeignKey(nameof(SupplierID))]
        public Supplier Supplier { get; set; }  
       /* public string EmployeeID { get; set; }
        [ForeignKey(nameof(EmployeeID))]
        public AppUser User { get; set; }*/
        public DateTime PurchaseDate { get; set; }=DateTime.Now;
        public double TotalAmount { get; set; }
        public int TotalQuantity {  get; set; }
        public double Total=>TotalAmount*TotalQuantity;
        public int Status { get; set; } = 1;
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
