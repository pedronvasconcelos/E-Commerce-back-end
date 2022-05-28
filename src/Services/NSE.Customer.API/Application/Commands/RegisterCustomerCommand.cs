using FluentValidation;
using NSE.Core.Messages;

namespace NSE.Customers.API.Application.Commands
{
    public class RegisterCustomerCommand : Command
    {
        public Guid Id { get;  private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id; //fill agregate msg 
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {

        public RegisterCustomerValidation()
        {
               RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O id do cliente precisa ser fornecido");

               RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do cliente precisa ser fornecido");

               RuleFor(c => c.Email)
                .Must(IsValidEmail)
                .WithMessage("Email inválido");

               RuleFor(c => c.Cpf)
                .Must(IsValidCpf)
                .WithMessage("CPF inválido");
        }

        protected static bool IsValidCpf(string cpf)
        {
            return Core.DomainObjects.Cpf.Validate(cpf);
        }

        protected static bool IsValidEmail(string email)
        {
            return Core.DomainObjects.Email.Validate(email);
        }

    }

    

    
}
