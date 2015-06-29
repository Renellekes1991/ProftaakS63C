using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class KlantFilter
    {
        private string voornaam;
        private string tussenvoegsel;
        private string achternaam;
        private string woonplaats;
        private string postcode;
        private string straatnaam;
        private int huisnummer;
        private string email;
        private string land;
        private int telefoon;

        public KlantFilter()
        {
            voornaam = "";
            tussenvoegsel = "";
            achternaam = "";
            woonplaats = "";
            postcode = "";
            straatnaam = "";
            huisnummer = -1;
            email = "";
            land = "";
            telefoon = -1;
        }

        public string Voornaam
        {
            get { return voornaam; }
            set { voornaam = value; }
        }

        public string Tussenvoegsel
        {
            get { return tussenvoegsel; }
            set { tussenvoegsel = value; }
        }

        public string Achternaam
        {
            get { return achternaam; }
            set { achternaam = value; }
        }

        public string Woonplaats
        {
            get { return woonplaats; }
            set { woonplaats = value; }
        }

        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }

        public string Straatnaam
        {
            get { return straatnaam; }
            set { straatnaam = value; }
        }

        public int Huisnummer
        {
            get { return huisnummer; }
            set { huisnummer = value; }
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

        public int Telefoon
        {
            get { return telefoon; }
            set { telefoon = value; }
        }
    }
}
