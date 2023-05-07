using Loja.Inspiracao.Core.Messagens;
using Loja.Inspiracao.Core.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Core.Util;

namespace Loja.Inspiracao.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T events) where T : Event;
        Task<DefaultResult> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
