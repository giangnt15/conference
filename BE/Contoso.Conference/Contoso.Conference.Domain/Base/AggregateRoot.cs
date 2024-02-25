using Contoso.Conference.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Base
{
    public abstract class AggregateRoot<TId>
    {
        public AggregateRoot() { 
            _events = [];
        }

        public TId Id { get; protected set; }
        public int Version { get; protected set; }

        private readonly List<object> _events;

        protected abstract void When(IVersionedEvent @event);

        protected abstract void EnsureValidState();

        public void Apply(IVersionedEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event);
            @event.Version = Version + 1;
            Version = @event.Version;
            When(@event);
            EnsureValidState();
            _events.Add(@event);
        }

        public List<object> GetEvents() => _events;
        public void ClearEvents() => _events.Clear();
    }
}
