using NSE.Catalog.API.Models;
using NSE.Catalog.API.Data.Repository;
using NSE.Catalog.API.Data;

namespace NSE.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }

    }
}
