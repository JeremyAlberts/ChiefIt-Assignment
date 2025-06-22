using Microsoft.AspNetCore.Mvc;
using YakShop.API.DTOs.Herd;
using YakShop.API.DTOs.Order;
using YakShop.API.Extensions;
using YakShop.Core.Interfaces.Services;

namespace YakShop.API.Controllers
{
    [ApiController]
    [Route("yak-shop")]
    public class YakShopController : BaseApiController
    {
        private readonly IShopService _shopService;

        public YakShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost("load")]
        public async Task<IActionResult> Load(HerdCreateDTO herd)
        {
            var command = herd.ToCommand();
            var result = await _shopService.Load(command);

            return ToActionResult(result);
        }

        [HttpGet("stock/{days}")]
        public async Task<IActionResult> Stock(int days)
        {
            var result = await _shopService.GetStock(days);

            return ToActionResult(result);
        }

        [HttpGet("herd/{days}")]
        public async Task<IActionResult> Herd(int days)
        {
            var result = await _shopService.GetHerd(days);
            return ToActionResult(result);
        }

        [HttpPost("order/{days}")]
        public async Task<IActionResult> Order(int days, OrderDTO order)
        {
            var command = order.ToCommand();
            var result = await _shopService.Order(days, command);
    
            return ToActionResult(result);
        }
    }
}
