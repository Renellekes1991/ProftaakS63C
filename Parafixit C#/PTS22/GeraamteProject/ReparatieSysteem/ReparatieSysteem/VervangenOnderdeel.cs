using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class VervangenOnderdeel
    {
        private string productnaam;
        private double kosten;

        public VervangenOnderdeel(string productnaam, double kosten)
        {
            this.productnaam = productnaam;
            this.kosten = kosten;
        }

        public string Productnaam
        {
            get { return productnaam; }
            set { productnaam = value; }
        }

        public double Kosten
        {
            get { return kosten; }
            set { kosten = value; }
        }
    }
}
