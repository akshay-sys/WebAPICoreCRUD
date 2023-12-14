using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoreCURD.Models
{
    public class Products
    {
        [Key]
        public int productId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string color { get; set; }
        public decimal unitPrice { get; set; }
        public int availableQuantity { get; set; }
    }
}
