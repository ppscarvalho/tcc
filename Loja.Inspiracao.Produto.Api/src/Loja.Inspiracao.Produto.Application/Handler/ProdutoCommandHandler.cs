using AutoMapper;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.ViewModels;
using Loja.Inspiracao.Produto.Domain.Interfaces;
using Loja.Inspiracao.Resources.Communication.Mediator;
using Loja.Inspiracao.Resources.Messagens;
using Loja.Inspiracao.Resources.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Resources.Util;
using MediatR;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;
namespace Loja.Inspiracao.Produto.Application.Handler
{
    public class ProdutoCommandHandler : IRequestHandler<AdicionarProdutoCommand, DefaultResult>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;

        public ProdutoCommandHandler(IProdutoRepository productRepository, IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _produtoRepository = productRepository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<DefaultResult> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var produto = _mapper.Map<LojaInspiracao.Produto>(request);
            var entity = _mapper.Map<ProdutoViewModel>(await _produtoRepository.AdicionarProduto(produto));

            var result = await _produtoRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result };
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }
    }
}
