using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class SaleData
    {
        [Key]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductIdAmount { get; set; }
    }
}
