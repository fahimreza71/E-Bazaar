namespace eBazaar.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

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
