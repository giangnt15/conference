using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Base
{
    public interface IProcessManager
    {
        Guid Id { get; }
        bool Completed { get; }
    }
}
