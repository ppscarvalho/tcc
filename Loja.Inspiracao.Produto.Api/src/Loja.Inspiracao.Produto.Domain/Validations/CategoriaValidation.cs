using FluentValidation;
using Loja.Inspiracao.Produto.Domain.Entities;

namespace Loja.Inspiracao.Produto.Domain.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("O id da categria não foi informado.");

            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("A descrição da categoria não foi informada.");
        }
    }
}
