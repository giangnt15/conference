using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Registration.Application.Contracts.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
