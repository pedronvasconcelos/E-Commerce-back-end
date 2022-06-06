using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NSE.Bff.Orders.Extensions;
using NSE.BFF.Orders.Models;

namespace NSE.Bff.Orders.Services
{
    public interface ICatalogService
    {
        Task<ItemProductDTO> GetProductById(Guid id);
    }

    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }

        public async Task<ItemProductDTO> GetProductById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/products/{id}");

            HandleErrorsResponse(response);

            return await DeserializeObjectResponse<ItemProductDTO>(response);
        }
    }
}