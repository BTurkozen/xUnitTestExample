using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xUnitTestExample.Web.Controllers;
using xUnitTestExample.Web.Models;
using xUnitTestExample.Web.Repository;

namespace xUnitTestExample.WebTest.ProductTest
{
    public class ProductsApiControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;
        private readonly ProductsApiController _productsApiController;
        private List<Product> _products;

        public ProductsApiControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _productsApiController = new ProductsApiController(_mockRepo.Object);
            _products = new List<Product>
            {
                new Product{ Id = 4, Name = "Book", Quantity = 1, Price = 10,Color="red"},
                new Product{ Id = 5, Name = "Computer", Quantity = 12, Price = 110,Color= "green"},
                new Product{ Id = 6, Name = "Mouse", Quantity = 14, Price = 50, Color = "purple"},
            };
        }

        [Fact]
        public async void GetProducts_ActionExecute_ReturnOkResultWithProduct()
        {
            _mockRepo.Setup(s => s.GetAllAsync()).ReturnsAsync(_products);

            var result = await _productsApiController.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnProduct = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.Equal<int>(_products.Count, returnProduct.ToList().Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetProduct_IdInValid_ReturnNotFound(int productId)
        {
            Product product = null;

            _mockRepo.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await _productsApiController.GetProduct(productId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
