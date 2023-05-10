using AutoMapper;
using Loja.Inspiracao.MQ.Models;
using Loja.Inspiracao.MQ.Models.Categoria;
using Loja.Inspiracao.MQ.Operators;
using Newtonsoft.Json;
using WebApp.Integracao.Application.Interfaces;
using WebApp.Models;

namespace WebApp.Integracao.Application.Service
{
    public class CategoriaAppService : ICategoriaAppService
    {
        private readonly IPublish _publish;
        private readonly IMapper _mapper;

        public CategoriaAppService(IPublish publish, IMapper mapper)
        {
            _publish = publish;
            _mapper = mapper;
        }

        public async Task<CategoriaViewModel> ObterCategoriaPorId(Guid id)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = id.ToString(),
                Queue = "ObterCategoriaPorId"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseCategoriaOut>(mapIn);
            return _mapper.Map<CategoriaViewModel>(response);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterTodasCategorias()
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Queue = "ListaCategoria"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseCategoriaOut[]>(mapIn);
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(result);
        }

        public async Task<ResponseOut> SalvarCategoria(CategoriaViewModel categoriaViewModel)
        {
            var categoria = _mapper.Map<ResponseCategoriaOut>(categoriaViewModel);

            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(categoria),
                Queue = "AdicionarCategoria"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }

        public async Task<ResponseOut> AlterarCategoria(CategoriaViewModel categoriaViewModel)
        {
            var categoria = _mapper.Map<ResponseCategoriaOut>(categoriaViewModel);

            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(categoria),
                Queue = "AlterarCategoria"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }
    }
}
