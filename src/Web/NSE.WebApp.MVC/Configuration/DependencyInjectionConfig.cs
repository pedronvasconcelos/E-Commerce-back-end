using NSE.WebApp.MVC.Services;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

        }
    }
}
