using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using NSE.Customers.API.Application.Events;
using NSE.Customers.API.Models;

namespace NSE.Customers.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler,
            IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository; 
        }

        public async Task<ValidationResult> Handle(RegisterCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(message.Id ,message.Name, message.Email, message.Cpf);

            var existingCustomer = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if (existingCustomer != null)
            {
                AddError("Este CPF já está em uso");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new RegisteredCustomerEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await PersistData(_customerRepository.UnitOfWork);

        }
    }
}
