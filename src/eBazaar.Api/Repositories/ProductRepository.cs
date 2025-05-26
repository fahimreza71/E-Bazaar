using eBazaar.Api.Data;
using eBazaar.Api.Interfaces;
using eBazaar.Api.Models;

namespace eBazaar.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        Task<Product> IProductRepository.AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        Task IProductRepository.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Product>> IProductRepository.GetAllAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<Product?> IProductRepository.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<int> IProductRepository.GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Product>> IProductRepository.SearchAsync(string searchTerm, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<Product> IProductRepository.UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
