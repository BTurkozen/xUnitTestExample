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
                new Product{ Id = 19, Name = "Glass", Quantity = 14, Price = 50, Color = "white"},
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

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public async void GetProduct_IdValid_ReturnOkResult(int productId)
        {
            var product = _products.First(p => p.Id == productId);

            _mockRepo.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await _productsApiController.GetProduct(productId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(productId, returnProduct.Id);
        }

        [Theory]
        [InlineData(4)]
        public async void PutProduct_IdIsNotEqualProduct_ReturnBadRequestResult(int productId)
        {
            var product = _products.First(p => p.Id == productId);

            var result = await _productsApiController.PutProduct(5, product);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(4)]
        public async void PutProduct_ActionExecute_ReturnNoContent(int productId)
        {
            var product = _products.First(x => x.Id == productId);

            _mockRepo.Setup(s => s.UpdateAsync(product));

            var result = await _productsApiController.PutProduct(productId, product);

            _mockRepo.Verify(v => v.UpdateAsync(product), Times.Once);

            var noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.IsType<NoContentResult>(noContentResult);
        }

        [Fact]
        public async void PostProduct_ActionExecute_ReturnCreateAtAction()
        {
            Product product = _products.Last();

            _mockRepo.Setup(s => s.CreateAsync(product)).Returns(Task.CompletedTask);

            var result = await _productsApiController.PostProduct(product);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            _mockRepo.Verify(v => v.CreateAsync(product), Times.Once);

            Assert.Equal("GetProduct", createdAtActionResult.ActionName);

        }

        [Theory]
        [InlineData(0)]
        public async void DeleteProduct_IdInValid_ReturnNotFound(int productId)
        {
            Product product = null;

            _mockRepo.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await _productsApiController.DeleteProduct(productId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(4)]
        public async void DeleteProduct_ActionExecute_ReturnNoContentResult(int productId)
        {
            var product = _products.First(p => p.Id == productId);

            _mockRepo.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(product);

            _mockRepo.Setup(s => s.DeleteAsync(product));

            var noContentResult = await _productsApiController.DeleteProduct(productId);

            _mockRepo.Verify(v => v.DeleteAsync(product), Times.Once());

            Assert.IsType<NoContentResult>(noContentResult);
        }

    }
}
