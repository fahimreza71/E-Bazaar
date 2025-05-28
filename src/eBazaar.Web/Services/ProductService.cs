using eBazaar.Api.Models;

namespace eBazaar.Web.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000/";
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int page = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/Product?page={page}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>() ?? Array.Empty<Product>();
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Product/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string query, int page = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/Product/search?query={Uri.EscapeDataString(query)}&page={page}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>() ?? Array.Empty<Product>();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Product", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>() ?? throw new InvalidOperationException("Failed to create product");
        }

        public async Task UpdateProductAsync(Guid id, Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Product/{id}", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Product/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> GetTotalCountAsync()
        {
            var response = await _httpClient.GetAsync("api/Product");
            response.EnsureSuccessStatusCode();

            if (response.Headers.TryGetValues("X-Total-Count", out var values))
            {
                if (int.TryParse(values.FirstOrDefault(), out int totalCount))
                    return totalCount;
            }
            return 0;
        }
    }
}
