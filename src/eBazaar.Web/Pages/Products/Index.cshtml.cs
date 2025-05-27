using eBazaar.Api.Models;
using eBazaar.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eBazaar.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public IndexModel(ProductService productService, CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 12;

        public IEnumerable<Product> Products { get; private set; } = Array.Empty<Product>();
        public int TotalPages { get; private set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Products = await _productService.SearchProductsAsync(SearchQuery, CurrentPage, PageSize);
            }
            else
            {
                Products = await _productService.GetProductsAsync(CurrentPage, PageSize);
            }

            var totalCount = await _productService.GetTotalCountAsync();
            //TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
            TotalPages = 5;
        }
    }
}
