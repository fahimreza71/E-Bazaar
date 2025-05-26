using eBazaar.Api.Data;
using eBazaar.Api.Interfaces;
using eBazaar.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace eBazaar.Api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetUserCartAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Cart?> GetCartItemAsync(Guid userId, Guid productId)
        {
            return await _context.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }

        public async Task<Cart> AddToCartAsync(Cart cart)
        {
            var existingItem = await GetCartItemAsync(cart.UserId, cart.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cart.Quantity;
                await _context.SaveChangesAsync();
                return existingItem;
            }

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> UpdateCartItemAsync(Cart cart)
        {
            var existingItem = await _context.Carts.FindAsync(cart.Id);
            if (existingItem == null)
                throw new ArgumentException("Cart item not found");

            existingItem.Quantity = cart.Quantity;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task DeleteCartItemAsync(Guid userId, Guid productId)
        {
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
