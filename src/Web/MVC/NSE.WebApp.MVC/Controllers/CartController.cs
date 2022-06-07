using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IOrdersBffService _ordersService;
        
        public CartController(IOrdersBffService ordersService,
                                  ICatalogService catalogoService)
        {
           _ordersService  = ordersService;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _ordersService.GetCart()); 
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AddItemCart(ItemCartViewModel ItemProduct)
        {
            var response = await _ordersService.AddItemCart(ItemProduct);

            if (ResponseHasErrors(response)) return View("Index", await _ordersService.GetCart());
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, int quantity)
        {
            var item = new ItemCartViewModel { ProductId = productId, Quantity = quantity };

            var response = await _ordersService.UpdateItemCart(productId, item);

            if (ResponseHasErrors(response)) return View("Index", await _ordersService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoveItemCart(Guid produtoId)
        {
            var response = await _ordersService.RemoveItemCart(produtoId);

            if (ResponseHasErrors(response)) return View("Index", await _ordersService.GetCart());
            return RedirectToAction("Index");
        }

    }
}