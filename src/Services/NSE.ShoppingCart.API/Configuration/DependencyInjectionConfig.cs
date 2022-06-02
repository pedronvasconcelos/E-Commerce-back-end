using Microsoft.AspNetCore.Http;
using NSE.ShoppingCart.API.Data;
using NSE.WebAPI.Core.User;

namespace NSE.ShoppingCart.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<CartContext>();
        }
    }
}
