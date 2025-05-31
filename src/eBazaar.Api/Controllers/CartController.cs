using eBazaar.Api.Interfaces;
using eBazaar.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBazaar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetUserCart(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest(new { message = "Invalid user ID" });

            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> AddToCart(Cart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (cart.Quantity <= 0)
                return BadRequest(new { message = "Quantity must be greater than 0" });

            try
            {
                var addedItem = await _cartRepository.AddToCartAsync(cart);
                return CreatedAtAction(nameof(GetCartItem),
                    new { userId = cart.UserId, productId = cart.ProductId },
                    addedItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}/items/{productId}")]
        public async Task<ActionResult<Cart>> GetCartItem(Guid userId, Guid productId)
        {
            if (userId == Guid.Empty)
                return BadRequest(new { message = "Invalid user ID" });

            if (productId == Guid.Empty)
                return BadRequest(new { message = "Invalid product ID" });

            var cartItem = await _cartRepository.GetCartItemAsync(userId, productId);
            if (cartItem == null)
                return NotFound(new { message = "Cart item not found" });

            return Ok(cartItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(Cart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (cart.Quantity <= 0)
                return BadRequest(new { message = "Quantity must be greater than 0" });

            try
            {
                await _cartRepository.UpdateCartItemAsync(cart);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> DeleteCartItem(Guid userId, Guid productId)
        {
            if (userId == Guid.Empty)
                return BadRequest(new { message = "Invalid user ID" });

            if (productId == Guid.Empty)
                return BadRequest(new { message = "Invalid product ID" });

            try
            {
                await _cartRepository.DeleteCartItemAsync(userId, productId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest(new { message = "Invalid user ID" });

            try
            {
                await _cartRepository.ClearCartAsync(userId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}