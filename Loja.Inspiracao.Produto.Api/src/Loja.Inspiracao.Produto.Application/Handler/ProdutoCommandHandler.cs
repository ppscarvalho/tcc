using AutoMapper;
using Loja.Inspiracao.Core.Util;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Domain.Interfaces;
using MediatR;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;

namespace Loja.Inspiracao.Produto.Application.Handler
{
    public class ProdutoCommandHandler : IRequestHandler<AdicionarProdutoCommand, DefaultResult>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoCommandHandler(IProdutoRepository productRepository, IMapper mapper)
        {
            _produtoRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<DefaultResult> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (!request.IsValid())
                return new DefaultResult { Result = "Error", Success = false };

            var product = _mapper.Map<LojaInspiracao.Produto>(request);

            _produtoRepository.AdicionarProduto(product);

            return new DefaultResult { Result = "OK", Success = true };
        }
    }
}
