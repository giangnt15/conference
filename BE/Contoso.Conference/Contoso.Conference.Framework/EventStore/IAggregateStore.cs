using Contoso.Conference.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Framework.EventStore
{
    public interface IAggregateStore
    {
        Task<bool> ExistsAsync<TId, T>(TId id) where T : AggregateRoot<TId>;
        Task<T> LoadAsync<TId, T>(TId id) where T : AggregateRoot<TId>, new();
        Task SaveAsync<TId, T>(T aggregate) where T : AggregateRoot<TId>;
    }
}
