using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Events
{
    public static class OrderEvents
    {
        public class OrderPlaced : VersionedEvent
        {
            public Guid ConferenceId { get; set; }
            public string RegistrantEmail { get; set; }
            public string RegistrantFirstName { get; set; }
            public string RegistrantLastName { get; set; }
            public string AccessCode { get; set; }
            public List<OrderItem> Items { get; set; }
        }

        public class OrderUpdated : VersionedEvent
        {
            public List<OrderItem> Items { get; set; }
        }

        public class OrderConfirmed : VersionedEvent
        {
        }

        public class OrderRejected : VersionedEvent
        {
        }

        public class OrderExpired : VersionedEvent
        {
        }
    }
}
