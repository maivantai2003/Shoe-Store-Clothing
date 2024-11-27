using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionID { get; set; }
        public float DiscountRate { get; set; }
        public string Conditions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; } = 1;
    }
}
