using YakShop.API.DTOs.Herd;
using YakShop.API.DTOs.Order;
using YakShop.API.DTOs.Yak;
using YakShop.Core.Commands;

namespace YakShop.API.Extensions
{
    public static class DTOExtensions
    {
        public static YakCommand ToCommand(this YakCreateDTO dto)
        {
            return new YakCommand
            {
                Name = dto.Name,
                Age = dto.Age,
                Sex = dto.Sex
            };
        }

        public static HerdCommand ToCommand(this HerdCreateDTO dto)
        {
            return new HerdCommand
            {
                Herd = dto.Herd.Select(y => y.ToCommand()).ToList()
            };
        }

        public static OrderCommand ToCommand(this OrderDTO dto)
        {
            return new OrderCommand
            {
                Customer = dto.Customer,
                Order = new Items { Milk = dto.Order.Milk, Skins = dto.Order.Skins }
            };
        }
    }
}
