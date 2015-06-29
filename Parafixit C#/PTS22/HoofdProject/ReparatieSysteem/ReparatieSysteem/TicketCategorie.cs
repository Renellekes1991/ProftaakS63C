using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class TicketCategorie
    {
        private string naam;
        private string omschrijving;

        public TicketCategorie(string naam, string omschrijving)
        {
            this.naam = naam;
            this.omschrijving = omschrijving;
        }

        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }

        public string Omschrijving
        {
            get { return omschrijving; }
            set { omschrijving = value; }
        }
    }
}
