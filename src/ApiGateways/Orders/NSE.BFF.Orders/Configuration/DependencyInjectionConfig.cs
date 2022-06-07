using NSE.Bff.Orders.Extensions;
using NSE.Bff.Orders.Services;
using NSE.WebAPI.Core;
using NSE.WebAPI.Core.User;
using Polly;

namespace NSE.Bff.Orders.Configuration.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();


            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitToTry())
                .AddTransientHttpErrorPolicy(prop => prop.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            
            services.AddHttpClient<ICartService, CartService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitToTry())
                .AddTransientHttpErrorPolicy(prop => prop.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitToTry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        }
    }
}