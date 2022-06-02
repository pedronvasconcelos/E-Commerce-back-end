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
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(ICartService carrinhoService,
                                  ICatalogService catalogoService)
        {
            _cartService = carrinhoService;
            _catalogService = catalogoService;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCart());
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AddItemCart(ItemProductViewModel ItemProduct)
        {
            var product = await _catalogService.GetById(ItemProduct.ProductId);

            ValidateItemCart(product, ItemProduct.Quantity);
            if (!OperationValid()) return View("Index", await _cartService.GetCart());
            
            ItemProduct.Name = product.Name;
            ItemProduct.Price = product.Price;
            ItemProduct.Image = product.Image;

            var response = await _cartService.AddItemCart(ItemProduct);

            if (ResponseHasErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, int quantity)
        {
            var product = await _catalogService.GetById(productId);

            ValidateItemCart(product, quantity);
            if (!OperationValid()) return View("Index", await _cartService.GetCart());

            var itemProduct = new ItemProductViewModel { ProductId = productId, Quantity = quantity };
            var response = await _cartService.UpdateItemCart(productId, itemProduct);

            if (ResponseHasErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoveItemCart(Guid produtoId)
        {
            var produto = await _catalogService.GetById(produtoId);

            if (produto == null)
            {
                AddErrorValidation("Produto inexistente!");
                return View("Index", await _cartService.GetCart());
            }

            var resposta = await _cartService.RemoveItemCart(produtoId);

            if (ResponseHasErrors(resposta)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        private void ValidateItemCart(ProductViewModel product, int quantity)
        {
            if (product == null) AddErrorValidation("Produto inexistente!");
            if (quantity < 1) AddErrorValidation($"Escolha ao menos uma unidade do produto {product.Name}");
            if (quantity > product.QuantityStock) AddErrorValidation($"O produto {product.Name} possui {product.QuantityStock } unidades em estoque, você selecionou {quantity}");
        }
    }
}