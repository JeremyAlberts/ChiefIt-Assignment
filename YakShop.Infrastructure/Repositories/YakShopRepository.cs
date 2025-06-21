using Microsoft.EntityFrameworkCore;
using YakShop.Core.Entities;
using YakShop.Core.Enumerations;
using YakShop.Core.Interfaces.Repository;

namespace YakShop.Infrastructure.Repositories
{
    public class YakShopRepository : IYakShopRepository
    {
        private readonly YakShopDbContext _context;

        public YakShopRepository(YakShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Yak>> GetHerd()
        {
            var herd = await _context.Yak.ToListAsync();

            return herd;
        }

        public async Task<(int Skins, decimal Milk)> GetStock()
        {
            var skinsQuantity = (int)((await _context.Stock
                .FirstOrDefaultAsync(s => s.Type == StockType.Skins))?.Quantity ?? 0m);

            var milkQuantity = (await _context.Stock
                .FirstOrDefaultAsync(s => s.Type == StockType.Milk))?.Quantity ?? 0m;

            return (skinsQuantity, milkQuantity);
        }

        public async Task Load(List<Yak> herd)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Yak");

            await _context.Yak.AddRangeAsync(herd);
            await _context.SaveChangesAsync();
        }

        public async Task ResetStock(decimal totalMilk, int totalSkins)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Stock");

            var milkStock = new Stock(StockType.Milk, totalMilk);
            var skinStock = new Stock(StockType.Skins, totalSkins);

            await _context.Stock.AddAsync(milkStock);
            await _context.Stock.AddAsync(skinStock);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateHerd(List<Yak> herd)
        {
            _context.Yak.UpdateRange(herd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStock(int skins, decimal milk)
        {
            var milkStock = _context.Stock.FirstOrDefault(s => s.Type == StockType.Milk);
            var skinStock = _context.Stock.FirstOrDefault(s => s.Type == StockType.Skins);

            if (milkStock != null)
            {
                milkStock.Quantity = milk;
            }

            if (skinStock != null)
            {
                skinStock.Quantity = skins;
            }

            await _context.SaveChangesAsync();
        }
    }
}
