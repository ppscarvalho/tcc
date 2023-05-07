using Loja.Inspiracao.Core.Messagens;
using Loja.Inspiracao.Core.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Core.Util;
using MediatR;

namespace Loja.Inspiracao.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<DefaultResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task PublishEvent<T>(T events) where T : Event
        {
            await _mediator.Publish(events);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}
