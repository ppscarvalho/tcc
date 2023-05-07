#nullable disable

using FluentValidation;
using FluentValidation.Results;
using Loja.Inspiracao.Core.Util;
using Loja.Inspiracao.Produto.Application.ViewModels;
using MediatR;

namespace Loja.Inspiracao.Produto.Application.Commands
{
    public class AdicionarProdutoCommand : ProdutoViewModel, IRequest<DefaultResult>
    {
        public ValidationResult ValidationResult { get; set; }

        public AdicionarProdutoCommand(ProdutoViewModel produtoViewModel)
        {
            CategoriaId = produtoViewModel.CategoriaId;
            Nome = produtoViewModel.Nome;
            Descricao = produtoViewModel.Descricao;
            Preco = produtoViewModel.Preco;
            Imagem = produtoViewModel.Imagem;
        }

        public bool IsValid()
        {
            ValidationResult = new AdicionarProdutoCommandCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarProdutoCommandCommandValidation : AbstractValidator<AdicionarProdutoCommand>
    {
        public AdicionarProdutoCommandCommandValidation()
        {
            RuleFor(c => c.CategoriaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da categoria inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("A descrição do produto não foi informada");

            RuleFor(c => c.Preco)
                .GreaterThan(0)
                .WithMessage("O valor do produto precisa ser maior que 0");
        }
    }
}
