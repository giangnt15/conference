using Contoso.Conference.Domain.Events;

namespace Contoso.Conference.Transport
{
    public interface IEventBus
    {
        Task PublishAsync(Envelop<IEvent> envelop);
        Task PublishAsync(IEvent @event);
        Task PublishAsync(IEnumerable<IEvent> events);
    }
}
