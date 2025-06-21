using YakShop.Core.Enumerations;

namespace YakShop.Core.Entities
{
    public class Stock
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public StockType Type { get; set; }
        public decimal Quantity { get; set; }

        public Stock() { }

        public Stock(StockType type, decimal quantity)
        {
            Type = type;
            Quantity = quantity;
        }
    }
}
