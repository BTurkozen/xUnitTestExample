using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xUnitTestExample.Web.Controllers;
using xUnitTestExample.Web.Models;

namespace xUnitTestExample.WebTest.ProductTest
{
    public class ProductsWithCategoryControllerWithInMemory : ProductsWithCategoryControllerTest
    {
        public ProductsWithCategoryControllerWithInMemory()
        {
            SetContextOptions(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("xUnitDb").Options);
        }

        [Fact]
        public async void Create_ModelValidProduct_ReturnRTAWithProduct()
        {
            var newProduct = new Product() { Name = "Pen 7A", Price = 200, Quantity = 100, Color = "Red" };

            using var context = new DataContext(_dbContextOptions);

            var category = context.Categories.First();

            newProduct.CategoryId = category.Id;

            var controller = new ProductsWithCategoryController(context);

            var result = await controller.Create(newProduct);

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);

            // Burada ikinci context oluşturma sebebimiz. Dataları trackladiği için test yanılma ıhtımalıni aradan cıkartmak için.
            using var contextCheck = new DataContext(_dbContextOptions);

            var product = contextCheck.Products.First(p => p.Name == newProduct.Name);

            Assert.Equal(newProduct.Name, product.Name);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteCategory_ExistCategoryId_DeleteAllProducts(int categoryId)
        {
            using var context = new DataContext(_dbContextOptions);

            var category = await context.Categories.FindAsync(categoryId);

            context.Categories.Remove(category);

            await context.SaveChangesAsync();

            using var contextCheck = new DataContext(_dbContextOptions);

            var products = await contextCheck.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

            // İnMemory olduğu için ilişkisel vertabanı durumundan dolayı hata geliyor.
            Assert.Empty(products);
        }
    }
}
