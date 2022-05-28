using FluentValidation.Results;
using MediatR;

namespace NSE.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; } //FluentValidation

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            //Metodo virtual para que seja possivel sobrescrever o metodo
            throw new NotImplementedException();
            
        }
    }
}