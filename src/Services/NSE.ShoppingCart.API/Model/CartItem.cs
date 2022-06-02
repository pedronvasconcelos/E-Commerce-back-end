using FluentValidation;

namespace NSE.ShoppingCart.API.Model
{
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public Guid CartId { get; set; }

        public CartCustomer CartCustomer { get; set; }

        internal void LinkToCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal decimal CalculateValue()
        {
            return Quantity * Price;
        }

        internal void AddUnits(int units)
        {
            Quantity += units;
        }

        internal void UpdateUnits(int units)
        {
            Quantity = units;
        }

        internal bool IsValid()
        {
            return new ItemCartValidator().Validate(this).IsValid;
        }
        public class ItemCartValidator : AbstractValidator<CartItem>
        {
            public ItemCartValidator()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Product ID is required");
                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("Name is required");
                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage(item => $"Minimum quantity for the item {item.Name} is 1");
                RuleFor(c => c.Quantity)
                    .LessThan(5)
                    .WithMessage(item => $"Maximum quantity for the item {item.Name} is {CartCustomer.MaxItemsQuantity}");
                RuleFor(c => c.Price)
                    .GreaterThan(0)
                    .WithMessage(item => $"{item.Name} price must be greater than 0");
            }
        }
    }


}
