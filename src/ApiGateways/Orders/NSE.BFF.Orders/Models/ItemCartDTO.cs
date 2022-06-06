namespace NSE.BFF.Orders.Models
{
    public class ItemCartDTO //represents Item in shopping cart
    {
        public Guid ProductId { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }
        
    }
}
