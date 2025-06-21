namespace YakShop.Core.Commands
{
    public class OrderCommand
    {
        public string Customer {  get; set; }  
        public Items Order { get; set; }
    }

    public class Items
    {
        public decimal Milk { get; set; }
        public int Skins { get; set; }
    }
}
