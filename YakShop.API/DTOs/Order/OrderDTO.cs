using YakShop.Core.Commands;

namespace YakShop.API.DTOs.Order
{
    public class OrderDTO
    {
        public string Customer { get; set; }
        public ItemsDTO Order { get; set; }
    }

    public class ItemsDTO
    {
        public decimal Milk { get; set; }
        public int Skins { get; set; }
    }
}
