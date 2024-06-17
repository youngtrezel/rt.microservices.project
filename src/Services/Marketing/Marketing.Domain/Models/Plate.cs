namespace Marketing.Domain.Models
{
    public class Plate
    {
        public Guid Id { get; set; }

        public string? Registration { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public string? Letters { get; set; }

        public int Numbers { get; set; }

        public bool Reserved { get; set; } = false;

        public bool Sold { get; set; } = false;

        public DateTime? DateSold { get; set; } = null;

        public decimal PriceSoldFor { get; set; }
    }
}
