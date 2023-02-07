using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatikaDevHafta1Odev.Entities;
using PatikaDevHafta1Odev.Repository;

namespace PatikaDevHafta1Odev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newProduct = await _productRepository.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            product.Id = id;
            await _productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialProduct(int id, [FromBody] JsonPatchDocument<Product> product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            product.ApplyTo(existingProduct);

            await _productRepository.UpdateProduct(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteProduct(product);
            return NoContent();
        }


        // LİSTELEME VE SIRALAMA İŞLEVLERİ 

        [HttpGet("list")]
        public async Task<IActionResult> GetProducts([FromQuery] string name, [FromQuery] string sortOrder)
        {
            var products = await _productRepository.GetProducts();
            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.ToUpper().Contains(name.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return Ok(products);
        }



    }
}