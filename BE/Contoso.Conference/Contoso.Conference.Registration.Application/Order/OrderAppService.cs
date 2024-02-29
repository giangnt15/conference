using Contoso.Conference.Domain;
using Contoso.Conference.Framework.EventStore;
using Contoso.Conference.Registration.Application.Contracts.Commands;
using Contoso.Conference.Registration.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Registration.Application.Order
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IAggregateStore _aggregateStore;
        public OrderAppService(IServiceProvider serviceProvider)
        {
            _aggregateStore = serviceProvider.GetService(typeof(IAggregateStore)) as IAggregateStore;
        }
        public async Task HandleAsync(OrderCommands.V1.PlaceOrder command)
        {
            var orderId = Guid.NewGuid();
            var order = new Domain.Order(orderId, command.ConferenceId,
                command.Seats.ConvertAll(x=>new OrderItem(x.SeatId, x.Quantity)),
                command.RegistrantEmail);
            await _aggregateStore.SaveAsync<Guid, Domain.Order>(order);
        }

        public async Task HandleAsync(OrderCommands.V1.UpdateOrder command)
        {
            var orderExisted = await _aggregateStore.ExistsAsync<Guid, Domain.Order>(command.OrderId);
            if (!orderExisted)
            {
                throw new InvalidOperationException($"Order {command.OrderId} is not existed!");
            }
            var order = await _aggregateStore.LoadAsync<Guid, Domain.Order>(command.OrderId);
            order.UpdateOrder(command.Seats.ConvertAll(x => new OrderItem(x.SeatId, x.Quantity)));
            await _aggregateStore.SaveAsync<Guid, Domain.Order>(order);
        }
    }
}
