using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xUnitTestExample.Web.Helpers;
using xUnitTestExample.Web.Models;
using xUnitTestExample.Web.Repository;

namespace xUnitTestExample.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IRepository<Product> _productRepo;

        public ProductsApiController(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepo.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{firstNumber}/{secondNumber}")]
        public IActionResult Add(int firstNumber, int secondNumber)
        {
            return Ok(new Helper().add(firstNumber, secondNumber));
        }

        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/ProductsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productRepo.UpdateAsync(product);

            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            await _productRepo.CreateAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/ProductsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepo.DeleteAsync(product);

            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            return product is null;
        }
    }
}
