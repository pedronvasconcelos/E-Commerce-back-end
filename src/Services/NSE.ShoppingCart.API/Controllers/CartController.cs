using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.ShoppingCart.API.Data;
using NSE.ShoppingCart.API.Model;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.ShoppingCart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {

        private readonly IAspNetUser _user;
        private readonly CartContext _dbContext;
        public CartController(IAspNetUser user)
        {
            _user = user;
        }

        [HttpGet("cart")]
        public async Task<CartCustomer> GetCart()
        {
            return await GetCartCustomer() ?? new CartCustomer(); //If the result is null, return a new CartCustomer
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddItemToCart(CartItem item)
        {
            var cart = await GetCartCustomer();

            if (cart == null)
            {
                HandleNewCart(item);
            }
            else
            {
                HandleExistentCart(cart, item);
            }
            ValidateCart(cart);
            if (!OperationValid()) return CustomResponse();

            await PersistToDatabase();

            return CustomResponse();
        }


        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
        {
            var cart = await GetCartCustomer();
            var itemCart = await GetItemCartValidated(productId,  cart, item);
            if (itemCart == null) return CustomResponse();

            cart.UpdateUnits(itemCart, item.Quantity);

            ValidateCart(cart);
            if (!OperationValid()) return CustomResponse();
            
            _dbContext.CartItems.Update(itemCart);
            _dbContext.CartCustomer.Update(cart);

            await PersistToDatabase();
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> DeleteCartItem(Guid productId)
        {
            var cart = await GetCartCustomer();
            var itemCart = await GetItemCartValidated(productId, cart);
            if (itemCart == null) return CustomResponse();

            ValidateCart(cart);
            if (!OperationValid()) return CustomResponse();
            
            cart.RemoveItem(itemCart);

            _dbContext.CartItems.Remove(itemCart);
            _dbContext.CartCustomer.Update(cart);

            await PersistToDatabase();
            return CustomResponse();
        }

        private async Task<CartCustomer> GetCartCustomer()
        {
            return await _dbContext.CartCustomer
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
        }

        private void HandleNewCart(CartItem item)
        {
            var cart = new CartCustomer(_user.GetUserId());
            cart.AddItem(item);

            _dbContext.CartCustomer.Add(cart);

        }

        private void HandleExistentCart(CartCustomer cart, CartItem item)
        {
            var productItemExistent = cart.CartItemExists(item);
            cart.AddItem(item);

            if (productItemExistent)
            {
                _dbContext.CartItems.Update(cart.GetByProductId(item.ProductId));
            }
            else
            {
                _dbContext.CartItems.Add(item);
            }

            _dbContext.CartCustomer.Update(cart);

        }

        private async Task<CartItem> GetItemCartValidated(Guid productId,  CartCustomer cart, CartItem item = null)
        {
            if (item != null && productId != item.ProductId)
            {
                AddNotification("ProductId does not match");
                return null;
            }
            if (cart == null)
            {
                AddNotification("Cart does not exist");
                return null;
            }

            var itemCart = await _dbContext.CartItems.FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

            if (itemCart == null || !cart.CartItemExists(itemCart))
            {
                AddNotification("The item is not in the cart");
                return null;
            }

            return itemCart;
        }

        private async Task PersistToDatabase()
        {
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0) AddNotification("Unable to persist data in the database");
        }

        private bool ValidateCart(CartCustomer cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddNotification(e.ErrorMessage));
            return false;
        }
    }
}
