using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class TicketFilter
    {
        private long ticketnr;

        public TicketFilter()
        {
            ticketnr = -1;
        }

        public long Ticketnr
        {
            get { return ticketnr; }
            set { ticketnr = value; }
        }
    }
}
