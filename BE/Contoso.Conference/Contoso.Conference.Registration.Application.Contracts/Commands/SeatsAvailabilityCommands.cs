using Contoso.Conference.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Registration.Application.Contracts.Commands
{
    public static class SeatsAvailabilityCommands
    {
        public static class V1
        {
            public class AddSeat : ICommand
            {
                public Guid Id { get; set; }
                public Guid ConferenceId { get; set; }
                public List<SeatQuantity> Seats { get; set; }
            }

            public class RemoveSeat : ICommand
            {
                public Guid Id { get; set; }
                public Guid ConferenceId { get; set; }
                public List<SeatQuantity> Seats { get; set; }
            }

            public class CommitReservation : ICommand
            {
                public Guid Id { get; set; }
                public Guid ConferenceId { get; set; }
                public Guid ReservationId { get; set; }
            }

            public class CancelReservation : ICommand
            {
                public Guid Id { get; set; }
                public Guid ConferenceId { get; set; }
                public Guid ReservationId { get; set; }
            }

            public class MakeReservation : ICommand
            {
                public Guid Id { get; set; }
                public Guid ConferenceId { get; set; }
                public Guid ReservationId { get; set; }
                public List<SeatQuantity> Seats { get; set; }
            }
        }
    }
}
