
using Confluent.Kafka;
using Contoso.Conference.Domain.Events;

namespace Contoso.Conference.Transport.Kafka
{
    public class KafkaEventBus : IEventBus
    {
        private readonly IProducer<Guid, string> _producer;

        public KafkaEventBus(IServiceProvider serviceProvider)
        {
            _producer = serviceProvider.GetService(typeof(IProducer<Guid, string>)) as IProducer<Guid, string>;
        }

        public async Task PublishAsync(Envelop<IEvent> envelop)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync(IEnumerable<IEvent> events)
        {
            throw new NotImplementedException();
        }
    }
}
