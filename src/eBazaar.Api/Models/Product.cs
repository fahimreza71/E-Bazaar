using System.ComponentModel.DataAnnotations;

namespace eBazaar.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between $0.01 and $999,999.99")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, 999999, ErrorMessage = "Stock quantity must be between 0 and 999,999")]
        public int StockQuantity { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100")]
        public decimal? DiscountPercentage { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DiscountStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DiscountEndDate { get; set; }

        [Url(ErrorMessage = "Please provide a valid URL for the image")]
        public string ImageUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [StringLength(200, ErrorMessage = "Slug cannot be longer than 200 characters")]
        public string Slug { get; set; } = string.Empty;

        public decimal GetCurrentPrice()
        {
            if (!DiscountPercentage.HasValue || !DiscountStartDate.HasValue || !DiscountEndDate.HasValue)
                return Price;

            var now = DateTime.UtcNow;
            if (now >= DiscountStartDate && now <= DiscountEndDate)
            {
                var discountAmount = Price * (DiscountPercentage.Value / 100);
                return Price - discountAmount;
            }

            return Price;
        }
    }
}