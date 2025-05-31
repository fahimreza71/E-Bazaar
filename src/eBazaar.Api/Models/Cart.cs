using System.ComponentModel.DataAnnotations;

namespace eBazaar.Api.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        public Product? Product { get; set; }

        public decimal GetTotal()
        {
            if (Product == null)
                return 0;

            return Product.GetCurrentPrice() * Quantity;
        }
    }
}