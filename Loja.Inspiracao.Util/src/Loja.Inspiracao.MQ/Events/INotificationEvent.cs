#nullable disable

using System.Threading.Tasks;

namespace Loja.Inspiracao.MQ.Events
{
    public interface INotificationEvent
    {
        void AddEvent(IEvent evento);

        void RemoveEvent(IEvent eventItem);

        void ClearEvent();

        Task<bool> PublishEvents();
    }
}
