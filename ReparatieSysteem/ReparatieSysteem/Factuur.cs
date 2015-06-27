using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    class Factuur
    {
        private Ticket ticket;
        private double prijs;

        public Factuur(Ticket ticket)
        {
            this.ticket = ticket;
            prijs = 0;
        }

        public Ticket Ticket
        {
            get { return ticket; }
        }

        public double Prijs
        {
            get { return prijs; }
            set { prijs = value; }
        }

        public override string ToString()
        {
            return ticket.Ticketnr.ToString() + ", " + prijs.ToString();
        }
    }
}
