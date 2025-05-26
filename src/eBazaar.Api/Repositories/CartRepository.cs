using eBazaar.Api.Interfaces;
using eBazaar.Api.Models;

namespace eBazaar.Api.Repositories
{
    public class CartRepository : ICartRepository
    {
        public Task<Cart> AddToCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task ClearCartAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartItemAsync(Guid userId, Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart?> GetCartItemAsync(Guid userId, Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cart>> GetUserCartAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> UpdateCartItemAsync(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
