using FluentValidation;
using FluentValidation.Results;
using Loja.Inspiracao.Core.Messagens;

namespace Loja.Inspiracao.Produto.Application.Commands
{
    public class AdicionarCategoriaCommand : Command
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }

        public AdicionarCategoriaCommand(Guid id, string description)
        {
            Id = id;
            Descricao = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarCategoriaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarCategoriaCommandValidation : AbstractValidator<AdicionarCategoriaCommand>
    {
        public AdicionarCategoriaCommandValidation()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("A descrição da categoria não foi informado");
        }
    }
}
