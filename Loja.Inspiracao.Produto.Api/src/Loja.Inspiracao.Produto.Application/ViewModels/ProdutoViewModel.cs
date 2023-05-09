#nullable disable

namespace Loja.Inspiracao.Produto.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal MargemLucro { get; set; }
        public int Estoque { get; set; }
        public CategoriaViewModel CategoriaViewModel { get; set; }
    }
}
