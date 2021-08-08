using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Protocol.Nesneler.Concrete.NesnelerProtocolEnums;

namespace Washer.Entity.Concrete
{
    public class Event
    {
        public WasherEventType EventType { get; set; }
        public DateTime EventDateTime { get; set; }
    }
}
