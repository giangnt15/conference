using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Events
{
    public interface IEvent
    {
        Guid SourceId { get; set; }
    }
}
