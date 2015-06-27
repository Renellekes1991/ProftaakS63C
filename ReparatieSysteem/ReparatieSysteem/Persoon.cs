using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class Persoon
    {
        private int nr;
        private string voornaam;
        private string tussenvoegsel;
        private string achternaam;
        private string woonplaats;
        private string postcode;
        private string straatnaam;
        private int huisnummer;

        public Persoon(int nr, string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer)
        {
            this.nr = nr;
            this.voornaam = voornaam;
            this.tussenvoegsel = tussenvoegsel;
            this.achternaam = achternaam;
            this.woonplaats = woonplaats;
            this.postcode = postcode;
            this.straatnaam = straatnaam;
            this.huisnummer = huisnummer;
        }

        public int Nr
        {
            get { return nr; }
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
    }
}
