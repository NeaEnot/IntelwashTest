using Core.Models;
using System.Collections.Generic;
using System;

namespace SaleApi.Models
{
    public class SaleModel
    {
        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }
        public List<SaleData> SalesData { get; set; }
    }
}
