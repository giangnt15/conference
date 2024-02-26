using Contoso.Conference.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Contoso.Conference.Domain.Events.OrderEvents;
using static Contoso.Conference.Domain.Events.SeatsAvailabilityEvents;

namespace Contoso.Conference.Domain.ProcessManagers
{
    public class RegistrationProcessManagerRouter :
        IEventHandler<OrderPlaced>,
        IEventHandler<SeatsReserved>
    {
        public void Handle(SeatsReserved @event)
        {
            throw new NotImplementedException();
        }

        public void Handle(OrderPlaced @event)
        {
        }
    }
}
