using EasyNetQ;
using FluentValidation.Results;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customers.API.Application.Commands;
using NSE.MessageBus;

namespace NSE.Customers.API.Services
{
    public class RegisterCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
                 await RegisterCustomer(request));
            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object sender, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegisterCustomer(UserRegisteredIntegrationEvent message)
        {
            var custommerCommand = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(custommerCommand);
            }

            return new ResponseMessage(success);
        }
    }
}
