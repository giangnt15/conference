using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain
{
    public record class SeatQuantityValueObject
    {
        public int Quantity { get; internal set; }

        private SeatQuantityValueObject(int qty) { 
            if (qty < 0)
            {
                throw new ArgumentException($"{nameof(qty)} can not be negative");
            }
            Quantity = qty;
        }

        public static SeatQuantityValueObject FromInt(int qty) => new(qty); 

        public static implicit operator int(SeatQuantityValueObject s) => s.Quantity;
        public static implicit operator SeatQuantityValueObject(int qty) => FromInt(qty);
    }
}
