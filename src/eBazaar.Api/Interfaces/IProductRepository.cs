using eBazaar.Api.Models;

namespace eBazaar.Api.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> SearchAsync(string searchTerm, int page = 1, int pageSize = 10);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task<int> GetTotalCountAsync();
    }
}
