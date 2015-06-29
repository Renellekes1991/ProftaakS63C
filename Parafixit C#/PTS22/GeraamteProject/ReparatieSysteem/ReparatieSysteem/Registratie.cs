using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class Registratie
    {
        private long mednr;

        //datum van toevoeging van registratie
        private DateTime datum;

        //vermelding (opmerking) bij de registratie
        private string vermelding;

        public Registratie()
        {
            mednr = -1;
            vermelding = "";
        }

        public long Mednr
        {
            get { return mednr; }
            set { mednr = value; }
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public string Vermelding
        {
            get { return vermelding; }
            set { vermelding = value; }
        }
    }
}
