using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Transport
{
    public class Envelop
    {
        public static Envelop<T> Create<T>(T body)
        {
            return new Envelop<T>(body);
        }
    }

    public class Envelop<T> : Envelop
    {
        public Envelop(T body) {
            Body = body;
        }
        public T Body { get; private set; }
        public TimeSpan Delay { get; set; }
        public string MessageId { get; set; }

        public static implicit operator Envelop<T>(T body) => Envelop.Create(body);
        public static implicit operator T(Envelop<T> envelop) => envelop.Body;
    }
}
