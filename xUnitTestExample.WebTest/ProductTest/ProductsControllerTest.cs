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
            _mockRepo = new Mock<IRepository<Product>>(MockBehavior.Loose);
            _productsController = new ProductsController(_mockRepo.Object);
            _products = new List<Product>
            {
                new Product{ Id = 4, Name = "Book", Quantity = 1, Price = 10,Color="red"},
                new Product{ Id = 5, Name = "Computer", Quantity = 12, Price = 110,Color= "green"},
                new Product{ Id = 6, Name = "Mouse", Quantity = 14, Price = 50, Color = "purple"},
            };
        }

        #region Index Action
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
        #endregion

        #region Detail Action
        [Fact]
        public async void Detail_IdIsNull_ReturnRedirectToIndexAction()
        {
            var result = await _productsController.Details(null);

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async void Detail_IdInvalid_ReturnNotFound()
        {
            Product product = null;
            _mockRepo.Setup(s => s.GetByIdAsync(0)).ReturnsAsync(product);

            var result = await _productsController.Details(0);

            var redirect = Assert.IsType<NotFoundResult>(result);

            Assert.Equal<int>(404, redirect.StatusCode);
        }

        [Theory]
        [InlineData(4)]
        public async void Detail_ValidId_ReturnProduct(int productId)
        {
            Product product = _products.First(p => p.Id == productId);

            _mockRepo.Setup(s => s.GetByIdAsync(product.Id)).ReturnsAsync(product);

            var result = await _productsController.Details(productId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.Model);

            Assert.Equal(productId, resultProduct.Id);

            Assert.Equal(product.Name, resultProduct.Name);
        }
        #endregion

        #region Create Action
        [Fact]
        public void Create_ActionExecute_ReturnViewResult()
        {
            var result = _productsController.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_InvalidModelState_ReturnViewResult()
        {
            _productsController.ModelState.AddModelError("Name", "Name is required");

            var result = await _productsController.Create(_products.First());

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<Product>(viewResult.Model);
        }

        [Fact]
        public async void Create_ValidModelState_ReturnRedirectToActionResult()
        {
            // Mocklama işleminde mocklanacakmı mocklanmayacakmı bunun kararını 
            // MockBehavior.Strict => İlgili dependencylerin hepsini mocklanması gerekmektedir.
            // MockBehavior.Loose(varsayılandır) => Methodla işiniz yoksa mocklanmasına gerekmemektedir.
            // İçerisindeki methodla ilgili bir işlem kontrol etmemiz gerekmiyorsa mocklanmasına gerekyoktur.

            var result = await _productsController.Create(_products.First());

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), redirect.ActionName);
        }
        #endregion

    }
}
