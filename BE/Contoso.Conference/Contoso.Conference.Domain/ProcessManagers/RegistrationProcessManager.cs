using Contoso.Conference.Domain.Base;
using Contoso.Conference.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.ProcessManagers
{
    public class RegistrationProcessManager : IProcessManager
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ConferenceId { get; private set; }
        public Guid ReservationId { get; private set; }
        public bool Completed { get; private set; }
        public ProcessState State { get; private set; }

        public enum ProcessState
        {
            NotStarted = 0,
            AwaitingReservationConfirmation = 1,
            ReservationConfirmationReceived = 2,
            PaymentConfirmationRecieved = 3,
        }

        public void Handle(OrderEvents.OrderPlaced e)
        {

        }
    }
}
