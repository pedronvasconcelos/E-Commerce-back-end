using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebAPI.Core;
using NSE.WebAPI.Core.User;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using System;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();


            

            #region HttpServices

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthService, AuthService>()
                .AddPolicyHandler(PollyExtensions.WaitToTry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitToTry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IOrdersBffService, OrdersBffService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitToTry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            #endregion
        }
    }
}