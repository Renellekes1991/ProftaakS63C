using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public interface IDatabase
    {
        List<Klant> GetKlanten();
        Klant GetKlantByKlantnr(int klantnr);
        List<Klant> GetKlanten(KlantFilter filter);
        List<Ticket> GetTickets();
        Ticket GetTicketByTicketnr(int ticketnr);
        List<Ticket> GetTickets(TicketFilter filter);
        List<Ticket> GetTicketsByKlant(int klantnr);
        List<Registratie> GetRegistratiesByTicket(int ticketnr);
        List<ReparatieRegistratie> GetReparatieRegistratiesByTicket(int ticketnr);
        List<Product> GetProductsByKlant(int klantnr);
        List<Afdeling> GetAfdelingen();

        Klant SaveKlant(string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer, string email, string telefoon, string land);
        Ticket SaveTicket(int klantnr, int productnr, double verwachteKosten, int verwachteReparatieTijd, TicketStatus status, Registratie registratie, string probleem, string afdelingsAfkorting, string categorie);
        Ticket SaveTicket(int klantnr, double verwachteKosten, int verwachteReparatieTijd, TicketStatus status, Registratie registratie, string probleem, string afdelingsAfkorting, string categorie);
        bool WijzigTicketstatus(int ticketnr, TicketStatus status);

        bool SaveRegistratie(int ticketnr, Registratie registratie);
        bool SaveReparatieRegistratie(int ticketnr, ReparatieRegistratie registratie);

        Medewerker SaveMedewerker(string naam, string password, string telefoon, string filliaal, MedewerkerFunctie functie);
        Product SaveProduct(int klanntr, int garantieTijd, string omschrijving, DateTime verkoopDatum);

        Medewerker LogIn(int gebruikersnaam, string pw);

        bool RemoveKlant(int klantnr);
        bool RemoveTicket(int ticketnr);
        bool RemoveMedewerker(int mednr);
        bool RemoveProduct(int productnr);
    }
}
