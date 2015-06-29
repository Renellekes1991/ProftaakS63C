using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public interface IReparatieSysteem
    {
        Medewerker LogIn(int gebruikersnaam, string pw);
        void LogUit();

        Klant VoegKlantToe(string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer, string email, string telefoon, string land);
        bool VerwijderKlant(int klantnr);

        Ticket VoegTicketToe(int klantnr, int productnr, double verwachteKosten, int verwachteReparatieTijd, string afdelingsAfkorting, string probleem, string categorie);
        Ticket VoegTicketToe(int klantnr, double verwachteKosten, int verwachteReparatieTijd, string afdelingsAfkorting, string probleem, string categorie);
        bool WijzigTicketstatus(int ticketnr, TicketStatus status);
        bool VerwijderTicket(int ticketnr);

        bool VoegRegistratieToe(int ticketnr, Registratie registratie);
        bool VoegReparatieRegistratieToe(int ticketnr, ReparatieRegistratie registratie);

        Product VoegProductToe(int klantnr, int garantieTijd, string omschrijving);
        bool VerwijderProduct(int productnr);

        List<Klant> GetKlanten();
        Klant GetKlantByKlantnr(int klantnr);
        List<Klant> ZoekKlanten(KlantFilter filter);
        List<Ticket> GetTickets();
        Ticket GetTicketByTicketnr(int ticketnr);
        List<Ticket> ZoekTickets(TicketFilter filter);
        List<Registratie> GetRegistratiesByTicket(int ticketnr);
        List<ReparatieRegistratie> GetReparatieRegistratiesByTicket(int ticketnr);
        List<Product> GetProductenByKlant(int klantnr);

        List<Afdeling> GetAfdelingen();

        Medewerker VoegMedewerkerToe(string naam, string password, string telefoon, string filliaal, MedewerkerFunctie functie);
        bool VerwijderMedewerker(int mednr);
    }
}
