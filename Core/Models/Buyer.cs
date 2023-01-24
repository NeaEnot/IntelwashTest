using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Buyer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> SalesIds { get; set; }
    }
}
