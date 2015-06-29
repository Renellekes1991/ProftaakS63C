using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace ReparatieSysteem
{
    public class ReparatieSysteem : IReparatieSysteem
    {
        private IDatabase database;
        private IMailServer mailServer;

        //constructor
        public ReparatieSysteem()
        {
            database = new OracleDatabase();
            mailServer = new MailServer();
        }

        //returnt false als de gebruikersnaam en/of pw niet kloppen
        //returnt true als het wel klopt, en deze gebruiker wordt nu ingelogd
        public Medewerker LogIn(int gebruikersnaam, string pw)
        {
            return database.LogIn(gebruikersnaam, pw);
        }

        //als er een gebruiker is ingelogd, wordt deze nu uitgelogd
        public void LogUit()
        {

        }

        //returnt false als de gegevens niet kloppen
        //returnt true als de gegevens wel kloppen en de klant is toegevoegd
        public Klant VoegKlantToe(string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer, string email, string telefoon, string land)
        {
            return database.SaveKlant(voornaam, tussenvoegsel, achternaam, woonplaats, postcode, straatnaam, huisnummer, email, telefoon, land);
        }

        //returnt false als de klant niet bestaat
        //returnt true als de klant wel bestaat en deze wordt verwijderd
        public bool VerwijderKlant(int klantnr)
        {
            return database.RemoveKlant(klantnr);
        }

        //voegt een ticket toe met als klant de eigenaar van het meegegeven product, en met als product het meegegeven product
        public Ticket VoegTicketToe(int klantnr, int productnr, double verwachteKosten, int verwachteReparatieTijd, string afdelingsAfkorting, string probleem, string categorie)
        {
            Registratie registratie = new Registratie();
            registratie.Vermelding = "ticket aangemaakt";
            registratie.Datum = DateTime.Now;

            return database.SaveTicket(klantnr, productnr, verwachteKosten, verwachteReparatieTijd, TicketStatus.status1, registratie, probleem, afdelingsAfkorting, categorie);
        }

        //Voegt een ticket toe met als klant de meegegeven klant. Deze ticket heeft geen product
        public Ticket VoegTicketToe(int klantnr, double verwachteKosten, int verwachteReparatieTijd, string afdelingsAfkorting, string probleem, string categorie)
        {
            Registratie registratie = new Registratie();
            registratie.Vermelding = "ticket aangemaakt";
            registratie.Datum = DateTime.Now;

            return database.SaveTicket(klantnr, verwachteKosten, verwachteReparatieTijd, TicketStatus.status1, registratie, probleem, afdelingsAfkorting, categorie);
        }

        public bool WijzigTicketstatus(int ticketnr, TicketStatus status)
        {
            return database.WijzigTicketstatus(ticketnr, status);
        }

        public bool VerwijderTicket(int ticketnr)
        {
            return database.RemoveTicket(ticketnr);
        }

        public bool VoegRegistratieToe(int ticketnr, Registratie registratie)
        {
            return database.SaveRegistratie(ticketnr, registratie);
        }

        public bool VoegReparatieRegistratieToe(int ticketnr, ReparatieRegistratie registratie)
        {
            return database.SaveReparatieRegistratie(ticketnr, registratie);
        }

        //Voegt een product toe
        public Product VoegProductToe(int klantnr, int garantieTijd, string omschrijving)
        {
            return database.SaveProduct(klantnr, garantieTijd, omschrijving, DateTime.Now);
        }

        public bool VerwijderProduct(int productnr)
        {
            return database.RemoveProduct(productnr);
        }

        public List<Klant> GetKlanten()
        {
            return database.GetKlanten();
        }

        public Klant GetKlantByKlantnr(int klantnr)
        {
            return database.GetKlantByKlantnr(klantnr);
        }

        //Geeft een lijst van klanten terug die voldoen aan de meegegeven filter
        public List<Klant> ZoekKlanten(KlantFilter filter)
        {
            return database.GetKlanten(filter);
        }

        public List<Ticket> GetTickets()
        {
            return database.GetTickets();
        }

        public Ticket GetTicketByTicketnr(int ticketnr)
        {
            return database.GetTicketByTicketnr(ticketnr);
        }

        //Geeft een lijst van tickets terug die voldoen aan de meegegeven filter
        public List<Ticket> ZoekTickets(TicketFilter filter)
        {
            return database.GetTickets(filter);
        }

        public List<Registratie> GetRegistratiesByTicket(int ticketnr)
        {
            return database.GetRegistratiesByTicket(ticketnr);
        }

        public List<ReparatieRegistratie> GetReparatieRegistratiesByTicket(int ticketnr)
        {
            return database.GetReparatieRegistratiesByTicket(ticketnr);
        }

        public List<Afdeling> GetAfdelingen()
        {
            return database.GetAfdelingen();
        }

        public List<Product> GetProductenByKlant(int klantnr)
        {
            return database.GetProductsByKlant(klantnr);
        }

        public Medewerker VoegMedewerkerToe(string naam, string password, string telefoon, string filliaal, MedewerkerFunctie functie)
        {
            return database.SaveMedewerker(naam, password, telefoon, filliaal, functie);
        }

        public bool VerwijderMedewerker(int mednr)
        {
            return database.RemoveMedewerker(mednr);
        }
    }
}
