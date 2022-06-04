using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ShoppingCartUrl);
        }

        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            HandleErrorsResponse(response);

            return await DeserializeResponse<CartViewModel>(response);
        }

        public async Task<ResponseResult> AddItemCart(ItemProductViewModel produto)
        {
            var itemContent = GetContent(produto);

            var response = await _httpClient.PostAsync("/cart/", itemContent);

            if(!HandleErrorsResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItemCart(Guid produtoId, ItemProductViewModel produto)
        {
            var itemContent = GetContent(produto);

            var response = await _httpClient.PutAsync($"/cart/{produto.ProductId}", itemContent);

            if (!HandleErrorsResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveItemCart(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{produtoId}");

            if (!HandleErrorsResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnOk();
        }

        Task<ResponseResult> ICartService.AddItemCart(ItemProductViewModel product)
        {
            throw new NotImplementedException();
        }

        Task<ResponseResult> ICartService.UpdateItemCart(Guid productId, ItemProductViewModel product)
        {
            throw new NotImplementedException();
        }

        Task<ResponseResult> ICartService.RemoveItemCart(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}