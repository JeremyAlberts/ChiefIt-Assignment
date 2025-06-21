using YakShop.Core.Commands;
using YakShop.Core.Entities;
using YakShop.Core.Operation;

namespace YakShop.Core.Interfaces.Services
{
    public interface IShopService
    {
        Task<OperationResult<bool>> Load(HerdCommand command);
        Task<OperationResult<List<Yak>>> GetHerd(int days);
        Task<OperationResult<StockResult>> GetStock(int days);
        Task<OperationResult<OrderResult>> Order(int days, OrderCommand command);
    }
}
