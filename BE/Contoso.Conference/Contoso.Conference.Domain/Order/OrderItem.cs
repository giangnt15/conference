using Contoso.Conference.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain
{
    public class OrderItem : Entity<Guid>
    {
        public OrderItem(Guid seatId, SeatQuantityValueObject seatQuantity) { 
            SeatId = seatId;
            Quantity = seatQuantity;
        }

        public Guid SeatId { get; internal set; } 
        public SeatQuantityValueObject Quantity { get; internal set; }

        protected override void EnsureValidState()
        {
           
        }
    }
}
