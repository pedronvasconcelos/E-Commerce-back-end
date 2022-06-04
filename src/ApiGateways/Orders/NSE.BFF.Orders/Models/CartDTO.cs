namespace NSE.BFF.Orders.Models
{
    public class CartDTO
    {
        public decimal TotalValue { get; set; }

        public List<ItemCartDTO> Items { get; set; } = new List<ItemCartDTO>();
        
        public decimal Discount { get; set; }
        
    }
}
