using YakShop.Core.Entities;

namespace YakShop.Core.Interfaces.Repository
{
    public interface IYakShopRepository
    {
        Task Load(List<Yak> yaks);
        Task<List<Yak>> GetHerd();
        Task ResetStock(decimal totalMilk, int totalSkins);
        Task<(int Skins, decimal Milk)> GetStock();
        Task UpdateStock(int skins, decimal milk);
        Task UpdateHerd(List<Yak> herd);
    }
}
