using MediatR;

namespace NSE.Customers.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<RegisteredCustomerEvent>
    {
        public Task Handle(RegisteredCustomerEvent notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

    }
}
