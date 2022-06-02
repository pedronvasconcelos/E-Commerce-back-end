using FluentValidation.Results;
using MediatR;
using NSE.Core.Mediator;
using NSE.Customers.API.Application.Commands;
using NSE.Customers.API.Application.Events;
using NSE.Customers.API.Data;
using NSE.Customers.API.Data.Repository;
using NSE.Customers.API.Models;
using System.Reflection;

namespace NSE.Customers.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        
            services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            
            services.AddScoped<INotificationHandler<RegisteredCustomerEvent>, CustomerEventHandler>();
            

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersContext>();

        }
    }


        
}