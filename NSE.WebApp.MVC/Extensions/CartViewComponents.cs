using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Extensions
{
    public class CartViewComponents : ViewComponent
    {
        private readonly ICartService _cartServices;

        public CartViewComponents(ICartService carrinhoService)
        {
            _cartServices = carrinhoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _cartServices.GetCart() ?? new CartViewModel());
        }
    }
}