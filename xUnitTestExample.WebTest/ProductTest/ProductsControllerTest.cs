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
        private readonly IMock<IRepository<Product>> _mockRepo;
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
    }
}
