using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class OrdersBffService : Service, IOrdersBffService
    {
        private readonly HttpClient _httpClient;

        public OrdersBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.OrdersBffUrl);
        }

        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/orders/cart/");

            HandleErrorsResponse(response);

            return await DeserializeResponse<CartViewModel>(response);
        }

        public async Task<int> GetQuantityCart()
        {
            var response = await _httpClient.GetAsync("/orders/cart-quantity");
            HandleErrorsResponse(response);

            return await DeserializeResponse<int>(response);
        }

        public async Task<ResponseResult> AddItemCart(ItemCartViewModel produto)
        {
            var itemContent = GetContent(produto);

            var response = await _httpClient.PostAsync("/orders/cart/items/", itemContent);

            if(!HandleErrorsResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItemCart(Guid produtoId, ItemCartViewModel produto)
        {
            var itemContent = GetContent(produto);

            var response = await _httpClient.PutAsync($"/orders/cart/items/{produto.ProductId}", itemContent);

            if (!HandleErrorsResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveItemCart(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/orders/cart/items/{produtoId}");

            if (!HandleErrorsResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnOk();
        }

  
    }
}