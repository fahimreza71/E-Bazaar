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
            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> AddToCart(Cart cart)
        {
            var addedItem = await _cartRepository.AddToCartAsync(cart);
            return CreatedAtAction(nameof(GetCartItem), new { userId = cart.UserId, productId = cart.ProductId }, addedItem);
        }

        [HttpGet("{userId}/items/{productId}")]
        public async Task<ActionResult<Cart>> GetCartItem(Guid userId, Guid productId)
        {
            var cartItem = await _cartRepository.GetCartItemAsync(userId, productId);
            if (cartItem == null)
                return NotFound();

            return Ok(cartItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(Cart cart)
        {
            try
            {
                await _cartRepository.UpdateCartItemAsync(cart);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> DeleteCartItem(Guid userId, Guid productId)
        {
            await _cartRepository.DeleteCartItemAsync(userId, productId);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(Guid userId)
        {
            await _cartRepository.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
