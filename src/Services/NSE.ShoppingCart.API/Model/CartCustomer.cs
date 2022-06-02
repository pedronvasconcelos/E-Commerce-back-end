using FluentValidation;
using FluentValidation.Results;

namespace NSE.ShoppingCart.API.Model
{
    public class CartCustomer
    {

        public const int MaxItemsQuantity = 20;
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public ValidationResult ValidationResult { get; set; }
        public CartCustomer(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }
        
        public CartCustomer()
        {
            
        }
        
        internal void SetCartTotalPrice()
        {
            TotalPrice = Items.Sum(p => p.CalculateValue());
        }

        internal bool CartItemExists(CartItem cartItem)
        {
            return Items.Any(prop => prop.ProductId == cartItem.ProductId);
        }
        
        internal CartItem GetByProductId(Guid productId)
        {
            return Items.FirstOrDefault(prop => prop.ProductId == productId);
        }

        internal void AddItem (CartItem item)
        {
            
            item.LinkToCart(Id);

            if (CartItemExists(item))
            {
                var existingItem = GetByProductId(item.ProductId);
                    existingItem.AddUnits(item.Quantity);

                    item = existingItem;
                    Items.Remove(existingItem);
             }
        
                Items.Add(item);
                SetCartTotalPrice();
         }

        internal void UpdateItem (CartItem item)
        {
            item.LinkToCart(Id);

            var existingItem = GetByProductId(item.ProductId);

            Items.Remove(existingItem);
            Items.Add(item);

            SetCartTotalPrice();   
        }

        internal void UpdateUnits(CartItem item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            Items.Remove(GetByProductId(item.ProductId));
            SetCartTotalPrice();
        }

        internal bool IsValid()
        {
            var errors = Items.SelectMany(i => new CartItem.ItemCartValidator().Validate(i).Errors).ToList();
            errors.AddRange(new CartCustomerValidator().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }

        public class CartCustomerValidator : AbstractValidator<CartCustomer>
        {
            public CartCustomerValidator()
            {
                RuleFor(c => c.CustomerId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("The customer needs an ID");

                RuleFor(c => c.Items.Count)
                    .GreaterThan(0)
                    .WithMessage("The cart has no items");
                RuleFor(c => c.TotalPrice)
                    .GreaterThan(0)
                    .WithMessage("The total price of the cart must be greater than zero");
            }
        }
    }

    
}
