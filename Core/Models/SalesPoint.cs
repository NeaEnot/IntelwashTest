using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class SalesPoint
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProvidedProduct> ProvidedProducts { get; set; }
    }
}
