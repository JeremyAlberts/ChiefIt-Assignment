using YakShop.Core.Enumerations;

namespace YakShop.API.DTOs.Yak
{
    public class YakCreateDTO
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public Sex Sex {  get; set; }      
    }
}
