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
    public class ProductsControllerTest
    {
        private Mock<IRepository<Product>> _mockRepo;
        private readonly ProductsController _productsController;
        private List<Product> _products;
        public ProductsControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _productsController = new ProductsController(_mockRepo.Object);
            _products = new List<Product>
            {
                new Product{ Name = "Book", Quantity = 1, Price = 10,Color="red"},
                new Product{ Name = "Computer", Quantity = 12, Price = 110,Color= "green"},
                new Product{ Name = "Mouse", Quantity = 14, Price = 50, Color = "purple"},
            };
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnView()
        {
            var result = await _productsController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Index_ActionExecute_ReturnProductList()
        {
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(_products);

            var result = await _productsController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var productList = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);

            Assert.Equal<int>(3, productList.Count());
        }
    }
}
