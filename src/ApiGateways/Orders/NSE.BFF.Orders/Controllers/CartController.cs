using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Orders.Services;
using NSE.BFF.Orders.Models;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Bff.Orders.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(
            ICartService cartService,
            ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("orders/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.GetCart());
        }

        [HttpGet]
        [Route("orders/cart-quantity")]
        public async Task<int> GetQuantityCart()
        {
            var quantity = await _cartService.GetCart();
            return quantity?.Items.Sum(i => i.Quantity) ?? 0;
        }

        [HttpPost]
        [Route("orders/cart/items")]
        public async Task<IActionResult> AddItemCart(ItemCartDTO item)
        {
            var product = await _catalogService.GetProductById(item.ProductId);

            if (!OperationValid()) return CustomResponse();

            item.Name = product.Name;
            item.Price = product.Price;
            item.Image = product.Image;

            var response = await _cartService.AddItemCart(item);
            
            return CustomResponse(response);
        }

        [HttpPut]
        [Route("orders/cart/items/{productId}")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, ItemCartDTO itemProduct)
        {

            var product = await _catalogService.GetProductById(productId);

            await ValidateItemCart(product, itemProduct.Quantity);
            
            if (!OperationValid()) return CustomResponse();

            var response = await _cartService.UpdateItemCart(productId, itemProduct);
            
            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("orders/cart/items/{productId}")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var product = await _catalogService.GetProductById(productId);
            
            if (product == null)
            {
                AddNotification("Product not found");
                return CustomResponse();
            }
            var response = await _cartService.RemoveItemCart(productId);
            
            return CustomResponse(response);
        }

        private async Task ValidateItemCart(ItemProductDTO product, int quantity)
        {
            if (product == null) AddNotification("Product does not exist");
            if (quantity < 1) AddNotification($"Select at least one unit of {product.Name}");

            var cart = await _cartService.GetCart();
            var itemCart = cart.Items.FirstOrDefault(i => i.ProductId == product.Id);

            if (itemCart != null && itemCart.Quantity + quantity > product.QuantityStock)
            {
                AddNotification($"The quantity of {product.Name} in your cart is {product.QuantityStock}, you have selected {quantity}.");
                return;
            }
            if (quantity > product.QuantityStock) AddNotification($"{product.Name} has only {product.QuantityStock} units in stock.");
        }
         
        
        
    }
}
