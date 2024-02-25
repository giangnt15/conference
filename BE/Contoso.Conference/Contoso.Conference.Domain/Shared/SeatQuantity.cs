using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Shared
{
    public class SeatQuantity
    {
        public SeatQuantity(Guid seatId, int seatQuantity)
        {
            SeatId = seatId;
            Quantity = seatQuantity;
        }

        public Guid SeatId { get; internal set; }
        public int Quantity { get; internal set; }

    }
}
