using eBazaar.Api.Interfaces;
using eBazaar.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBazaar.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 8)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100)
                return BadRequest(new { message = "Invalid page or pageSize parameters" });

            var products = await _productRepository.GetAllAsync(page, pageSize);
            var totalCount = await _productRepository.GetTotalCountAsync();

            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Invalid product ID" });

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 8)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { message = "Search query cannot be empty" });

            if (page < 1 || pageSize < 1 || pageSize > 100)
                return BadRequest(new { message = "Invalid page or pageSize parameters" });

            var products = await _productRepository.SearchAsync(query, page, pageSize);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Additional validation for discount dates
            if (product.DiscountPercentage > 0)
            {
                if (!product.DiscountStartDate.HasValue || !product.DiscountEndDate.HasValue)
                    return BadRequest(new { message = "Discount start and end dates are required when discount percentage is set" });

                if (product.DiscountStartDate > product.DiscountEndDate)
                    return BadRequest(new { message = "Discount end date must be after start date" });

                if (product.DiscountStartDate < DateTime.UtcNow)
                    return BadRequest(new { message = "Discount start date cannot be in the past" });
            }

            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            var createdProduct = await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != product.Id)
                return BadRequest(new { message = "URL ID does not match product ID" });

            // Additional validation for discount dates
            if (product.DiscountPercentage > 0)
            {
                if (!product.DiscountStartDate.HasValue || !product.DiscountEndDate.HasValue)
                    return BadRequest(new { message = "Discount start and end dates are required when discount percentage is set" });

                if (product.DiscountStartDate > product.DiscountEndDate)
                    return BadRequest(new { message = "Discount end date must be after start date" });
            }

            product.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _productRepository.UpdateAsync(product);
            }
            catch (ArgumentException)
            {
                return NotFound(new { message = "Product not found" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Invalid product ID" });

            try
            {
                await _productRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound(new { message = "Product not found" });
            }
        }
    }
}