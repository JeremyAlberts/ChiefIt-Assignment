using YakShop.Core.Commands;
using YakShop.Core.Constants;
using YakShop.Core.Entities;
using YakShop.Core.Interfaces.Repository;
using YakShop.Core.Interfaces.Services;
using YakShop.Core.Operation;

namespace YakShop.Core.Services
{
    public class ShopService : IShopService
    {
        private readonly IYakShopRepository _yakShopRepository;

        public ShopService(IYakShopRepository yakShopRepository)
        {
            _yakShopRepository = yakShopRepository;
        }

        public async Task<OperationResult<bool>> Load(HerdCommand command)
        {
            try
            {
                List<Yak> yaks = command.Herd
                .Select(cmd => new Yak(cmd.Name, cmd.Age, cmd.Sex))
                .ToList();

                await _yakShopRepository.Load(yaks);
                await ResetStock(yaks);

                return OperationResult<bool>.Success(true, OperationStatus.ResetContent);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex, OperationStatus.Exception);
            }
        }

        public async Task<OperationResult<List<Yak>>> GetHerd(int days)
        {
            try
            {
                await AdvanceDays(days);

                var herd = await _yakShopRepository.GetHerd();

                return OperationResult<List<Yak>>.Success(herd, OperationStatus.Ok);
            }
            catch (Exception ex)
            {
                return OperationResult<List<Yak>>.Failure(ex, OperationStatus.Exception);
            }
        }

        public async Task<OperationResult<StockResult>> GetStock(int days)
        {
            try
            {
                await AdvanceDays(days);

                var (skins, milk) = await _yakShopRepository.GetStock();

                var stockResult = new StockResult
                {
                    Milk = milk,
                    Skins = skins
                };

                return OperationResult<StockResult>.Success(stockResult, OperationStatus.Ok);
            }
            catch (Exception ex)
            {
                return OperationResult<StockResult>.Failure(ex, OperationStatus.Exception);
            }
        }

        public async Task<OperationResult<OrderResult>> Order(int days, OrderCommand command)
        {
            try
            {
                await AdvanceDays(days);
                var (skins, milk) = await _yakShopRepository.GetStock();

                var deliverableSkins = command.Order.Skins <= skins ? command.Order.Skins : (int?)null;
                var deliverableMilk = command.Order.Milk <= milk ? command.Order.Milk : (decimal?)null;

                var updatedSkins = skins - (deliverableSkins ?? 0);
                var updatedMilk = milk - (deliverableMilk ?? 0);

                await _yakShopRepository.UpdateStock(updatedSkins, updatedMilk);

                var result = new OrderResult
                {
                    Skins = command.Order.Skins > skins ? null : command.Order.Skins,
                    Milk = command.Order.Milk > milk ? null : command.Order.Milk,
                };

                if (result.Skins == null || result.Milk == null)
                {
                    return OperationResult<OrderResult>.Success(result, OperationStatus.Partial);
                }

                return OperationResult<OrderResult>.Success(result, OperationStatus.Created);
            }
            catch (Exception ex)
            {
                return OperationResult<OrderResult>.Failure(ex, OperationStatus.Exception);
            }
        }

        private async Task ResetStock(List<Yak> yaks)
        {
            decimal totalMilk = 0;
            int totalSkins = 0;

            foreach (var yak in yaks)
            {
                totalMilk += yak.Milk();
                if (yak.Age > YakConstants.MinimumShaveAge)
                    totalSkins += 1;
            }

            await _yakShopRepository.ResetStock(totalMilk, totalSkins);
        }

        private async Task AdvanceDays(int days)
        {
            if (days <= 0)
                return;

            var (skins, milk) = await _yakShopRepository.GetStock();
            var herd = await _yakShopRepository.GetHerd();

            for (int i = 0; i < days; i++)
            {
                var herdWithDaysAdded = AddDay(herd);

                foreach (var yak in herd)
                {
                    skins += yak.Shave();
                    milk += yak.Milk();
                }
            }

            await _yakShopRepository.UpdateStock(skins, milk);
            await _yakShopRepository.UpdateHerd(herd);

            return;
        }

        private List<Yak> AddDay(List<Yak> herd)
        {
            foreach (var yak in herd)
            {
                var newAge = yak.Age + YakConstants.DayInYear;

                yak.SetAge(newAge);
            }

            return herd;
        }
    }
}
