using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class Medewerker
    {
        private long mednr;
        private string naam;
        private string password;
        private string telefoon;
        private string filliaal;
        private MedewerkerFunctie functie;

        public Medewerker(long mednr, string naam, string password, string telefoon, string filliaal, MedewerkerFunctie functie)
        {
            this.mednr = mednr;
            this.naam = naam;
            this.password = password;
            this.telefoon = telefoon;
            this.filliaal = filliaal;
            this.functie = functie;
        }

        public long Mednr
        {
            get { return mednr; }
            set { mednr = value; }
        }

        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Telefoon
        {
            get { return telefoon; }
            set { telefoon = value; }
        }

        public string Filiaal
        {
            get { return filliaal; }
            set { filliaal = value; }
        }

        public MedewerkerFunctie Functie
        {
            get { return functie; }
            set { functie = value; }
        }
    }
}
