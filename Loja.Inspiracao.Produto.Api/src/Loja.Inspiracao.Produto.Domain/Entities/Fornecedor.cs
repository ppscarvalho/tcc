#nullable disable

namespace Loja.Inspiracao.Produto.Domain.Entities
{
    public class Fornecedor
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }

        // EF Relation
        public ICollection<Produto> Produto { get; set; }
    }
}