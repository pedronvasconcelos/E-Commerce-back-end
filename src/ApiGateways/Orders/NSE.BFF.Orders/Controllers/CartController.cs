using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Orders.Services;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Bff.Orders.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogoService _catalogService;

        public CartController(
            ICartService cartService, 
            ICatalogoService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("orders/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet]
        [Route("orders/cart-quantity")]
        public async Task<IActionResult> GetQuantityCart()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("orders/cart/items")]
        public async Task<IActionResult> AddItemCart()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("orders/cart/items/{productId}")]
        public async Task<IActionResult> UpdateItemCart()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("orders/cart/items/{productId}")]
        public async Task<IActionResult> RemoveItemCart()
        {
            return CustomResponse();
        }
    }
}
