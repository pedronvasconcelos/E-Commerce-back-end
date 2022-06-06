using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NSE.Bff.Orders.Extensions;
using NSE.BFF.Orders.Models;
using NSE.Core.Comunication;

namespace NSE.Bff.Orders.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ShoppingCartUrl);
        }

        public async Task<CartDTO> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            HandleErrorsResponse(response);

            return await DeserializeObjectResponse<CartDTO>(response);
        }

 
        
        public async Task<ResponseResult> AddItemCart(ItemCartDTO product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PostAsync("/cart/", itemContent);

            if (!HandleErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);
            
            return ReturnOk();
        }
        public async Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartDTO cart)
        {
            var itemContent = GetContent(cart);

            var response = await _httpClient
                .PutAsync($"/cart/{cart.ProductId}", itemContent);

            if (!HandleErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();

        }


        public async Task<ResponseResult> RemoveItemCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!HandleErrorsResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
    }
}