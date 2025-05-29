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
        public int PageSize { get; set; } = 8;

        public IEnumerable<Product> Products { get; private set; } = Array.Empty<Product>();
        public IEnumerable<Cart> CartItems { get; set; } = Array.Empty<Cart>();
        public int TotalPages { get; private set; }

        public async Task<IActionResult> OnPostAddProductAsync(Product product)
        {
            await _productService.CreateProductAsync(product);
            return RedirectToPage();
        }

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
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            //Cart functionality
            Guid userId = Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            CartItems = await _cartService.GetUserCartAsync(userId);
        }
    }
}
