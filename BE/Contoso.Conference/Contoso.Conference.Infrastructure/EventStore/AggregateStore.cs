using Contoso.Conference.Domain.Base;
using Contoso.Conference.Framework.EventStore;
using Contoso.Conference.Infrastructure.EventStore;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Infrastucture.EventStore
{
    public class AggregateStore : IAggregateStore
    {
        private readonly EventStoreClient _eventStoreClient;
        public AggregateStore(IServiceProvider serviceProvider)
        {
            _eventStoreClient = serviceProvider.GetService(typeof(EventStoreClient)) as EventStoreClient;
        }

        public async Task<bool> ExistsAsync<TId, T>(TId id) where T : AggregateRoot<TId>
        {
            var meta = await _eventStoreClient.GetStreamMetadataAsync(GetStreamName<TId, T>(id));
            return meta.MetastreamRevision != null;
        }

        public async Task<T> LoadAsync<TId, T>(TId id) where T : AggregateRoot<TId>, new()
        {
            var streamName = GetStreamName<TId, T>(id);
            var res = _eventStoreClient.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);
            var agg = new T();
            await res.ForEachAsync(e => agg.Load(e.GetEvent()));
            return agg;
        }

        public async Task SaveAsync<TId, T>(T aggregate) where T : AggregateRoot<TId>
        {
            ArgumentNullException.ThrowIfNull(aggregate, nameof(aggregate));
            var events = aggregate.GetEventDatas();
            if (events.Count == 0) return;
            await _eventStoreClient.AppendToStreamAsync(GetStreamName<TId, T>(aggregate), StreamState.Any, events);
            aggregate.ClearEvents();
        }

        private static string GetStreamName<TId, T>(TId id) where T : AggregateRoot<TId>
        {
            return $"{typeof(T).Name}_{id}";
        }

        private static string GetStreamName<TId, T>(T aggregate) where T : AggregateRoot<TId>
        {
            return $"{typeof(T).Name}_{aggregate.Id}";
        }
    }
}
