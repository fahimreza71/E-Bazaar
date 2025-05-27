using eBazaar.Api.Models;

namespace eBazaar.Web.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CartService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000/";
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<IEnumerable<Cart>> GetUserCartAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/Cart/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Cart>>() ?? Array.Empty<Cart>();
        }

        public async Task<Cart> AddToCartAsync(Cart cart)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Cart", cart);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Cart>() ?? throw new InvalidOperationException("Failed to add item to cart");
        }

        public async Task<Cart?> GetCartItemAsync(Guid userId, Guid productId)
        {
            var response = await _httpClient.GetAsync($"api/Cart/{userId}/items/{productId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Cart>();
        }

        public async Task UpdateCartItemAsync(Cart cart)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Cart", cart);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCartItemAsync(Guid userId, Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"api/Cart/{userId}/items/{productId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var response = await _httpClient.DeleteAsync($"api/Cart/{userId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
