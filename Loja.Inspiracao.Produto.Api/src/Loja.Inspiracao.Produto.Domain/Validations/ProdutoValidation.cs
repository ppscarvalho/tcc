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

            RuleFor(c => c.ValorCompra)
                .GreaterThan(0)
                .WithMessage("O valor de compra do produto precisa ser maior que 0 (zero).");

            RuleFor(c => c.ValorVenda)
                .GreaterThan(0)
                .WithMessage("O valor de venda do produto precisa ser maior que 0 (zero).");
        }
    }
}
