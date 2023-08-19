﻿using Moq;
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
                new Product{ Id = 8, Name = "Computer", Quantity = 12, Price = 110,Color= "green"},
                new Product{ Id = 9, Name = "Mouse", Quantity = 14, Price = 50, Color = "purple"},
            };
        }
    }
}