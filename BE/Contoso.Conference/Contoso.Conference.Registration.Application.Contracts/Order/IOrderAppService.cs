using Contoso.Conference.Registration.Application.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Registration.Application.Contracts.Order
{
    public interface IOrderAppService : 
        ICommandHandler<OrderCommands.V1.PlaceOrder>,
        ICommandHandler<OrderCommands.V1.UpdateOrder>
    {
    }
}
