using Microsoft.EntityFrameworkCore;

namespace xUnitTestExample.Web.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
