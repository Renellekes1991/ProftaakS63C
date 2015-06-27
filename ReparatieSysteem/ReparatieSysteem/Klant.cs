using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{   
    public class Klant : Persoon
    {
        private string email;
        private string land;
        private string telefoon;

        public Klant(int klantnr, string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer, string email, string land, string telefoon)
            : base(klantnr, voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer)
        {
            this.email = email;
            this.land = land;
            this.telefoon = telefoon;
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Land
        {
            get { return land; }
            set { land = value; }
        }

        public string Telefoon
        {
            get { return telefoon; }
            set { telefoon = value; }
        }

        public override string ToString()
        {
            return this.Nr + ", " + this.Voornaam + ", " + this.Tussenvoegsel + ", " + this.Achternaam + ", " + this.Woonplaats;
        }
    }
}
