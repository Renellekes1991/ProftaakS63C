using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class Afdeling
    {
        private string afkorting;
        private string naam;
        private string omschrijving;

        public Afdeling(string afkorting, string naam, string omschrijving)
        {
            this.afkorting = afkorting;
            this.naam = naam;
            this.omschrijving = omschrijving;
        }

        public string Afkorting
        {
            get { return afkorting; }
            set { afkorting = value; }
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
