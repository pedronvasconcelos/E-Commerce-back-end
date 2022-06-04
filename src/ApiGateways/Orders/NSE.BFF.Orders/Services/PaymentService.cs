using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NSE.Bff.Orders.Extensions;

namespace NSE.Bff.Orders.Services
{
    public interface IPagamentoService
    {
    }

    public class PaymentService : Service, IPagamentoService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PaymentUrl);
        }
    }
}