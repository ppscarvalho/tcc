#nullable disable
using AutoMapper;
using Loja.Inspiracao.Produto.Application.ViewModels;
using Loja.Inspiracao.Produto.Domain.Interfaces;

namespace Loja.Inspiracao.Produto.Application.Queries.Categoria
{
    public class CategoriaQueries : ICategoriaQueries
    {
        private readonly ICategoriaRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriaQueries(ICategoriaRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterTodasCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoryRepository.ObterTodasCategorias());
        }

        public async Task<CategoriaViewModel> ObterCategoriaPorId(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoryRepository.ObterCategoriaPorId(id));
        }
    }
}
