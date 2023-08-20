using Microsoft.EntityFrameworkCore;

namespace xUnitTestExample.Web.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Pens" }, new Category { Id = 2, Name = "Wheels" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
