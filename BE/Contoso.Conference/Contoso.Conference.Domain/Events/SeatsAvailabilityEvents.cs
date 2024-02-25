using Contoso.Conference.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Events
{
    public static class SeatsAvailabilityEvents
    {
        public class SeatsReserved : VersionedEvent
        {
            public Guid ReservationId { get; set; }
            public List<SeatQuantity> Seats { get; set; }
            public List<SeatQuantity> AvailableSeatChanges { get; set; }
        }
    }
}
