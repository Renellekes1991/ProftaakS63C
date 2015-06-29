using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class Product
    {
        private long productnr;
        private long klantnr;

        //de omschrijving van het product
        private string omschrijving;

        //de datum van verkoop
        private DateTime verkoopDatum;

        //de garantietijd in maanden
        private int garantieTijd;

        public Product(long productnr, long klantnr, int garantieTijd, string omschrijving, DateTime verkoopDatum)
        {
            this.productnr = productnr;
            this.klantnr = klantnr;
            this.omschrijving = omschrijving;
            this.garantieTijd = garantieTijd;
            this.verkoopDatum = verkoopDatum;
        }

        public long Productnr
        {
            get { return productnr; }
            set { productnr = value; }
        }

        public long Klantnr
        {
            get { return klantnr; }
            set { klantnr = value; }
        }

        public string Omschrijving
        {
            get { return omschrijving; }
            set { omschrijving = value; }
        }

        public DateTime VerkoopDatum
        {
            get { return verkoopDatum; }
            set { verkoopDatum = value; }
        }

        public int GarantieTijd
        {
            get { return garantieTijd; }
            set { garantieTijd = value; }
        }
    }
}
