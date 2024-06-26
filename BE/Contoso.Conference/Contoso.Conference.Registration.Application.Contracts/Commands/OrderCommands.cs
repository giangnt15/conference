﻿using Contoso.Conference.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Registration.Application.Contracts.Commands
{
    public static class OrderCommands
    {
        public static class V1
        {
            public class PlaceOrder : ICommand
            {
                public Guid Id { get; set; }
                public Guid ConferenceId { get; set; }
                public List<SeatQuantity> Seats { get; set; }
                public string RegistrantEmail { get; set; }
            }

            public class UpdateOrder : ICommand
            {
                public Guid Id { get; set; }
                public Guid OrderId { get; set; }
                public List<SeatQuantity> Seats { get; set; }
            }

            public class ExpireOrder : ICommand
            {
                public Guid Id { get; set; }
                public Guid OrderId { get; set; }
            }

            public class RejectOrder : ICommand
            {
                public Guid Id { get; set; }
                public Guid OrderId { get; set; }
            }

            public class MarkOrderAsReserved : ICommand
            {
                public Guid Id { get; set; }
                public Guid OrderId { get; set; }
                public List<SeatQuantity> Seats { get; set; }
            }

            public class ConfirmOrder : ICommand
            {
                public Guid Id { get; set; }
                public Guid OrderId { get; set; }
            }
        }
    }
}
