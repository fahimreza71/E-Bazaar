namespace eBazaar.Api.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
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
