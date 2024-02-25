using Contoso.Conference.Domain.Base;
using Contoso.Conference.Domain.Events;
using Contoso.Conference.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Contoso.Conference.Domain.Events.OrderEvents;

namespace Contoso.Conference.Domain
{
    public class Order : AggregateRoot<Guid>
    {
        public Guid ConferenceId { get; internal set; }
        public List<OrderItem> Items { get; internal set; }
        public bool IsConfirmed { get; internal set; }
        public EmailValueObject RegistrantEmail { get; internal set; }

        public Order(Guid id, Guid conferenceId, List<OrderItem> items, string registrantEmail) {
            Apply(new OrderPlaced()
            {
                SourceId = id,
                ConferenceId = conferenceId,
                Items = items,
                RegistrantEmail = registrantEmail
            });
        }

        protected override void EnsureValidState()
        {

        }

        public void UpdateOrder(List<OrderItem> items)
        {
            if (IsConfirmed)
            {
                throw new InvalidOperationException("Can not update a confirmed order!");
            }
            Apply(new OrderUpdated()
            {
                Items = items
            });
        }

        public void ExpireOrder()
        {
            Apply(new OrderExpired());
        }

        public void RejectOrder()
        {
            if (IsConfirmed)
            {
                throw new InvalidOperationException("Can not update a confirmed order!");
            }
            Apply(new OrderRejected());
        }

        public void ConfirmOrder()
        {
            Apply(new OrderConfirmed());
        }


        protected override void When(IVersionedEvent @event)
        {
            switch (@event)
            {
                case OrderPlaced e:
                    Id= e.SourceId;
                    RegistrantEmail = e.RegistrantEmail;
                    ConferenceId = e.ConferenceId;
                    Items = e.Items;
                    break;
                case OrderUpdated e:
                    Items = e.Items;
                    break;
                case OrderConfirmed e:
                    break;
                case OrderRejected e:
                    break;
                case OrderExpired e:
                    break;
            }
        }
    }
}
