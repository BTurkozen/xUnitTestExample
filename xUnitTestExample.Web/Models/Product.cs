using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace xUnitTestExample.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Precision(18, 2)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Color { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
