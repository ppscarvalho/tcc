#nullable disable

using FluentValidation.Results;
using Loja.Inspiracao.Core.Util;
using MediatR;

namespace Loja.Inspiracao.Core.Messagens
{
    public abstract class Command : Message, IRequest<DefaultResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
