using Contoso.Conference.Domain.Base;
using Contoso.Conference.Domain.Events;
using Contoso.Conference.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain
{
    public class SeatsAvailability : AggregateRoot<Guid>
    {
        private readonly Dictionary<Guid, int> _remainingSeats;
        private readonly Dictionary<Guid, List<SeatQuantity>> _pendingReservations;

        public SeatsAvailability()
        {
            _remainingSeats = [];
            _pendingReservations = [];
        }

        public SeatsAvailability(Guid id) : this()
        {
            Id = id;
        }

        public void MakeReservation(Guid reservationId, List<SeatQuantity> wantedSeats)
        {
            var wantedList = wantedSeats.ToList();
            if (wantedList.Any(x => !_remainingSeats.ContainsKey(x.SeatId)))
            {
                throw new ArgumentOutOfRangeException(nameof(wantedSeats));
            }

            var diff = new Dictionary<Guid, SeatDiff>();

            foreach (var wantedSeat in wantedList)
            {
                diff.Add(wantedSeat.SeatId, new SeatDiff()
                {
                    Remaining = _remainingSeats[wantedSeat.SeatId],
                    Wanted = wantedSeat.Quantity
                });
            }

            if (_pendingReservations.TryGetValue(reservationId, out var reservation))
            {
                foreach (var item in reservation)
                {
                    GetOrAdd(diff, item.SeatId).Existing = item.Quantity;
                }
            }

            Apply(new SeatsAvailabilityEvents.SeatsReserved()
            {
                ReservationId = reservationId,
                Seats = diff.Select(x => new SeatQuantity(x.Key, x.Value.Actual)).Where(x => x.Quantity != 0).ToList(),
                AvailableSeatChanges = diff.Select(x => new SeatQuantity(x.Key, x.Value.DiffFromLast)).ToList(),
            });
        }

        public void CommitReservation(Guid reservationId)
        {
            if (!_pendingReservations.ContainsKey(reservationId))
            {
                throw new InvalidOperationException("Reservation not existed!");
            }
            Apply(new SeatsAvailabilityEvents.SeatsReservationCommited()
            {
                ReservationId = reservationId
            });
        }

        public void CancelReservation(Guid reservationId)
        {
            if (!_pendingReservations.ContainsKey(reservationId))
            {
                throw new InvalidOperationException("Reservation not existed!");
            }
            Apply(new SeatsAvailabilityEvents.SeatReservationCanceled()
            {
                ReservationId = reservationId,
                AvailableSeatsChange = _pendingReservations[reservationId]
            });
        }

        public void AddSeats(List<SeatQuantity> seats)
        {
            Apply(new SeatsAvailabilityEvents.SeatsAdded()
            {
                Seats = seats.Where(x => _remainingSeats.ContainsKey(x.SeatId)).ToList(),
            });
        }

        public void RemoveSeats(List<SeatQuantity> seats)
        {
            Apply(new SeatsAvailabilityEvents.SeatsRemoved()
            {
                Seats = seats.Where(x => _remainingSeats.ContainsKey(x.SeatId)).ToList(),
            });
        }

        protected override void EnsureValidState()
        {
            if (_remainingSeats.Any(x => x.Value < 0))
            {
                throw new Exception("Remaining seats can not be negative");
            }
        }

        protected override void When(IVersionedEvent @event)
        {
            switch (@event)
            {
                case SeatsAvailabilityEvents.SeatsReserved e:
                    if (e.Seats.Count == 0)
                    {
                        _pendingReservations.Remove(e.ReservationId);
                    }
                    else
                    {
                        _pendingReservations[e.ReservationId] = e.Seats;
                    }
                    foreach (var item in e.Seats)
                    {
                        _remainingSeats[item.SeatId] -= item.Quantity;
                    }
                    break;
                case SeatsAvailabilityEvents.SeatsReservationCommited e:
                    _pendingReservations.Remove(e.ReservationId);
                    break;
                case SeatsAvailabilityEvents.SeatReservationCanceled e:
                    _pendingReservations.Remove(e.ReservationId);
                    foreach (var seat in e.AvailableSeatsChange)
                    {
                        _remainingSeats[seat.SeatId] += seat.Quantity;
                    }
                    break;
                case SeatsAvailabilityEvents.SeatsAdded e:
                    foreach (var seat in e.Seats)
                    {
                        _remainingSeats[seat.SeatId] += seat.Quantity;
                    }
                    break;
                case SeatsAvailabilityEvents.SeatsRemoved e:
                    foreach (var seat in e.Seats)
                    {
                        _remainingSeats[seat.SeatId] -= seat.Quantity;
                    }
                    break;
            }
        }

        private SeatDiff GetOrAdd(Dictionary<Guid, SeatDiff> diffs, Guid seatId)
        {
            if (!diffs.ContainsKey(seatId))
            {
                diffs[seatId] = new SeatDiff();
            }
            return diffs[seatId];

        }

        private class SeatDiff
        {
            public int Wanted { get; set; }
            public int Remaining { get; set; }
            public int Existing { get; set; }
            public int Actual { get => Math.Min(Wanted, Remaining + Existing); }
            public int DiffFromLast { get => Actual - Existing; }
        }
    }
}
