using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xUnitTestExample.Web.Models;

namespace xUnitTestExample.WebTest.ProductTest
{
    public class ProductsWithCategoryControllerTest
    {
        protected DbContextOptions<DataContext> _dbContextOptions { get; private set; }

        public void SetContextOptions(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
            SeedData();
        }

        public void SeedData()
        {
            using var context = new DataContext(_dbContextOptions);

            // Ayağa kalkınca Db siler
            context.Database.EnsureDeleted();
            // Yeniden oluşturur.
            context.Database.EnsureCreated();

            context.Categories.Add(new Category { Name = "Electronics" });
            context.Categories.Add(new Category { Name = "Glassware" });

            context.Products.Add(new Product { CategoryId = 1, Name = "Pens", Price = 10, Quantity = 10, Color = "Red" });
            context.Products.Add(new Product { CategoryId = 2, Name = "Computers", Price = 250, Quantity = 3, Color = "Gray" });
            context.Products.Add(new Product { CategoryId = 1, Name = "Easer", Price = 5, Quantity = 15, Color = "Green" });
            context.Products.Add(new Product { CategoryId = 2, Name = "Mouses", Price = 15, Quantity = 100, Color = "Gray" });

            context.SaveChanges();
        }
    }
}
