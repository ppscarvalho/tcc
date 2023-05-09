#nullable disable

using Loja.Inspiracao.Produto.Domain.Validations;
using Loja.Inspiracao.Resources.DomainObject;
using System.ComponentModel.DataAnnotations;

namespace Loja.Inspiracao.Produto.Domain.Entities
{
    public sealed class Categoria : Entity, IAggregateRoot
    {
        public string Descricao { get; set; }

        // EF Relation
        public ICollection<Produto> Produto { get; set; }

        public Categoria() { }

        public Categoria(Guid id, string description)
        {
            Id = id;
            Descricao = description;
            IsValid();
        }

        public override bool IsValid()
        {
            ValidationResult = new CategoriaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}