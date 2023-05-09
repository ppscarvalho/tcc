#nullable disable

using FluentValidation;
using FluentValidation.Results;
using Loja.Inspiracao.Produto.Application.ViewModels;
using Loja.Inspiracao.Resources.Messagens;

namespace Loja.Inspiracao.Produto.Application.Commands
{
    public class AdicionarProdutoCommand : Command
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal MargemLucro { get; set; }
        public int Estoque { get; set; }

        public AdicionarProdutoCommand() { }

        public AdicionarProdutoCommand(ProdutoViewModel produtoViewModel)
        {
            CategoriaId = produtoViewModel.CategoriaId;
            Nome = produtoViewModel.Nome;
            Descricao = produtoViewModel.Descricao;
            ValorCompra = produtoViewModel.ValorCompra;
            ValorVenda = produtoViewModel.ValorVenda;
            MargemLucro = produtoViewModel.MargemLucro;
        }

        public override bool IsValid()
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

            RuleFor(c => c.ValorCompra)
                .GreaterThan(0)
                .WithMessage("O valor de compra do produto precisa ser maior que 0 (zero).");

            RuleFor(c => c.ValorVenda)
                .GreaterThan(0)
                .WithMessage("O valor de venda do produto precisa ser maior que 0 (zero).");
        }
    }
}
