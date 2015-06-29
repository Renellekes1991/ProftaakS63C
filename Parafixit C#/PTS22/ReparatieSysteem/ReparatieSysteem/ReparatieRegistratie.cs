using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class ReparatieRegistratie : Registratie
    {
         //Het aantal uren gewerkt 
        private double aantalUren;

        private List<VervangenOnderdeel> vervangenOnderdelen;

        public ReparatieRegistratie()
        {
            aantalUren = -1;
            vervangenOnderdelen = new List<VervangenOnderdeel>();
        }

        public double AantalUren
        {
            get { return aantalUren; }
            set { aantalUren = value; }
        }

        public List<VervangenOnderdeel> VervangenOnderdelen
        {
            get { return vervangenOnderdelen; }
        }

        public override string ToString()
        {
            return base.ToString() + ", " + aantalUren.ToString();
        }
    }
}
