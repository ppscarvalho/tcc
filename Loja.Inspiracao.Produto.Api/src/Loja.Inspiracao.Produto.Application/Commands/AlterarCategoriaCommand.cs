#nullable disable

using FluentValidation;
using Loja.Inspiracao.Resources.Messagens;

namespace Loja.Inspiracao.Produto.Application.Commands
{
    public class AlterarCategoriaCommand : Command
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }

        public AlterarCategoriaCommand() { }

        public AlterarCategoriaCommand(Guid id, string description)
        {
            Id = id;
            Descricao = description;
        }
        public override bool IsValid()
        {
            ValidationResult = new AlterarCategoriaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AlterarCategoriaCommandValidation : AbstractValidator<AlterarCategoriaCommand>
    {
        public AlterarCategoriaCommandValidation()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("A descrição da categoria não foi informado");
        }
    }

}
