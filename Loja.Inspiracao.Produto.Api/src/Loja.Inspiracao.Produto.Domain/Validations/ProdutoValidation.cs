using FluentValidation;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;

namespace Loja.Inspiracao.Produto.Domain.Validations
{
    public class ProdutoValidation : AbstractValidator<LojaInspiracao.Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido.");

            RuleFor(c => c.CategoriaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da categoria inválido.");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado.");

            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("A descrição do produto não foi informada.");

            RuleFor(c => c.Preco)
                .GreaterThan(0)
                .WithMessage("O valor do produto precisa ser maior que 0 (zero).");
        }
    }
}
