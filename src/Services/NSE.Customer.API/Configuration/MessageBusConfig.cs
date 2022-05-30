using NSE.Core.Utils;
using NSE.MessageBus;
using NSE.Customers.API.Services;

namespace NSE.Customers.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisterCustomerIntegrationHandler>();
        }
    }
        
}