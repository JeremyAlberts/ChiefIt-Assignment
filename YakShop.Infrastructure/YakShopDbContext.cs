using Microsoft.EntityFrameworkCore;
using YakShop.Core.Entities;

namespace YakShop.Infrastructure
{
    public class YakShopDbContext : DbContext
    {
        public YakShopDbContext(DbContextOptions<YakShopDbContext> options) : base(options) { }
        public DbSet<Yak> Yak { get; set; }
        public DbSet<Stock> Stock { get; set; }
    }
}
