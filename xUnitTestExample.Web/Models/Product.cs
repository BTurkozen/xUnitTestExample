using Microsoft.EntityFrameworkCore;

namespace xUnitTestExample.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string Color { get; set; }
    }
}
