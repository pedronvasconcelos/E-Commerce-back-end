namespace NSE.BFF.Orders.Models
{
    public class ItemProductDTO //represents Items in catalog
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public  int QuantityStock { get; set;}
    
    }
}
