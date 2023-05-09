#nullable disable
using Loja.Inspiracao.Produto.Domain.Validations;
using Loja.Inspiracao.Resources.DomainObject;
using System.ComponentModel.DataAnnotations;

namespace Loja.Inspiracao.Produto.Domain.Entities
{
    public sealed class Produto : Entity, IAggregateRoot
    {
        public Guid CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal ValorCompra { get; private set; }
        public decimal ValorVenda { get; private set; }
        public decimal MargemLucro { get; private set; }
        public int Estoque { get; private set; }
        public bool Ativo { get; private set; }
        public Guid FornecedorId { get; private set; }

        public Categoria Categoria { get; private set; }
        public Fornecedor Fornecedor { get; private set; }

        public Produto() { }

        public Produto(Guid id, Guid categoriaId, string nome, string descricao, decimal valorCompra, decimal valorVenda, decimal margemLucro, Guid fornecedorId)
        {
            Id = id;
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            ValorCompra = valorCompra;
            ValorVenda = valorVenda;
            MargemLucro = margemLucro;
            FornecedorId = fornecedorId;
            IsValid();
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void UpdateCategory(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void StockReplacement(int stock)
        {
            Estoque += stock;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}