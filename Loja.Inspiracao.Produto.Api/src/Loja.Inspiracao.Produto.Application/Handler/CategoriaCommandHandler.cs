using AutoMapper;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.ViewModels;
using Loja.Inspiracao.Produto.Domain.Entities;
using Loja.Inspiracao.Produto.Domain.Interfaces;
using Loja.Inspiracao.Resources.Communication.Mediator;
using Loja.Inspiracao.Resources.Messagens;
using Loja.Inspiracao.Resources.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Resources.Util;
using MediatR;

namespace Loja.Inspiracao.Produto.Application.Handler
{
    public class CategoriaCommandHandler :
        IRequestHandler<AdicionarCategoriaCommand, DefaultResult>,
        IRequestHandler<AlterarCategoriaCommand, DefaultResult>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public CategoriaCommandHandler(ICategoriaRepository categoriaRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<DefaultResult> Handle(AdicionarCategoriaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var categoria = _mapper.Map<Categoria>(request);
            var entity = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.AdicionarCategoria(categoria));

            //AddEventToPublish(request, categoria);

            var result = await _categoriaRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result };
        }

        public async Task<DefaultResult> Handle(AlterarCategoriaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var categoria = _mapper.Map<Categoria>(request);
            var entity = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.AlterarCategoria(categoria));

            var result = await _categoriaRepository.UnitOfWork.Commit();

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
