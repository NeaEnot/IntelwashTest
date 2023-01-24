using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }
        public List<SaleData> SalesData { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
