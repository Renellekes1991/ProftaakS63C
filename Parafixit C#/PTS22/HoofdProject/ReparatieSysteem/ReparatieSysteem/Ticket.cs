using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public class Ticket
    {
        private long ticketnr;
        private long klantnr;

        //in dagen
        private int verwachteReparatieTijd;
        private double verwachteKosten;
        private TicketStatus status;

        private string afdelingsAfkorting;
        private string probleem;

        private string categorieNaam;

        public Ticket(long ticketnr, long klantnr, int verwachteReparatieTijd, double verwachteKosten, TicketStatus status, string afdelingsAfkorting, string probleem, string categorieNaam)
        {
            this.ticketnr = ticketnr;
            this.klantnr = klantnr;
            this.verwachteReparatieTijd = verwachteReparatieTijd;
            this.verwachteKosten = verwachteKosten;
            this.status = status;
            this.afdelingsAfkorting = afdelingsAfkorting;
            this.probleem = probleem;
            this.categorieNaam = categorieNaam;
        }

        public long Ticketnr
        {
            get { return ticketnr; }
        }

        public long Klantnr
        {
            get { return klantnr; }
            set { klantnr = value; }
        }

        public int VerwachteReparatieTijd
        {
            get { return verwachteReparatieTijd; }
            set { verwachteReparatieTijd = value; }
        }

        public double VerwachteKosten
        {
            get { return verwachteKosten; }
            set { verwachteKosten = value; }
        }

        public TicketStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public string AfdelingsAfkorting
        {
            get { return afdelingsAfkorting; }
            set { afdelingsAfkorting = value; }
        }

        public string Probleem
        {
            get { return probleem; }
            set { probleem = value; }
        }

        public string Categorie
        {
            get { return categorieNaam; }
            set { categorieNaam = value; }
        }
    }
}
