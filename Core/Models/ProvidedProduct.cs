using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class ProvidedProduct
    {
        [Key]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
