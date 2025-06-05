using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator
{
    static class EventHandlers
    {
        public static event EventHandler<PIResponse> GetDataEvent;

        public static void Notify(PIResponse response)
        {
            GetDataEvent.Invoke(null,response);
        }
    }
}
