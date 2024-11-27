using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public string CustomerID { get; set; }
        [ForeignKey(nameof(CustomerID))]    
        public AppUser User { get; set; }
        public DateTime InvoiceDate { get; set; }=DateTime.Now;
        public double TotalAmount { get; set; } = 0d;
        public double TotalDiscount { get; set; } = 0d;
        public string PaymentMethod { get; set; } = "COD";
        public double FinalAmount { get; set; } = 0d;
        public int Action { get; set; } = 0;
        public int Status { get; set; } = 1;
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
