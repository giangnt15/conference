using Contoso.Conference.Domain.Base;
using Contoso.Conference.Domain.Events;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Infrastructure.EventStore
{
    public static class EventStoreExtensions
    {
        public static List<EventData> GetEventDatas<TId>(this AggregateRoot<TId> aggregateRoot)
        {
            var events = from evt in aggregateRoot.GetEvents() 
                         select new EventData(Uuid.NewUuid(),
                                    evt.GetType().Name,
                                    Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(evt)),
                                    Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(new EventMetadata() { ClrType = evt.GetType().AssemblyQualifiedName })));
            return events.ToList();
        }

        public static IVersionedEvent GetEvent(this ResolvedEvent eventData)
        {
            var eventMetadata = System.Text.Json.JsonSerializer.Deserialize<EventMetadata>(eventData.Event.Metadata.ToArray());
            var clrType = Type.GetType(eventMetadata.ClrType);
            return (IVersionedEvent) System.Text.Json.JsonSerializer.Deserialize(eventData.Event.Data.ToArray(), clrType);
        }
    }
}
