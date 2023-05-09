#nullable disable

namespace Loja.Inspiracao.Produto.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<ProdutoViewModel> ProdutosViewModel { get; set; }
    }
}
