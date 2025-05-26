using eBazaar.Api.Models;

namespace eBazaar.Api.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetUserCartAsync(Guid userId);
        Task<Cart?> GetCartItemAsync(Guid userId, Guid productId);
        Task<Cart> AddToCartAsync(Cart cart);
        Task<Cart> UpdateCartItemAsync(Cart cart);
        Task DeleteCartItemAsync(Guid userId, Guid productId);
        Task ClearCartAsync(Guid userId);
    }
}
