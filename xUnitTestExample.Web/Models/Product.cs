using Microsoft.EntityFrameworkCore;

namespace xUnitTestExample.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        //[Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
    }
}
