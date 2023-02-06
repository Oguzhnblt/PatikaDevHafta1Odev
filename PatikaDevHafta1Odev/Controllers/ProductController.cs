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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id) // ID'ye Göre Ürün Getirme
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product) // Ürün Ekleme
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productRepository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product) // ID'ye Göre Ürün Güncelleme 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.Id = id;
            await _productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)  // ID'ye Göre Ürün Silme 
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Id = id;
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
