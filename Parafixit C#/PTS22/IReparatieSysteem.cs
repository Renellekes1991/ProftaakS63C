using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem
{
    public interface IReparatieSysteem
    {
        Medewerker LogIn(string gebruikersnaam, string pw);
        void LogUit();

        Klant VoegKlantToe(string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer, string email, string telefoon, string land);
        bool VerwijderKlant(long klantnr);

        Ticket VoegTicketToe(long klantnr, long productnr, double verwachteKosten, int verwachteReparatieTijd, string afdelingsAfkorting, string probleem, string categorie);
        Ticket VoegTicketToe(long klantnr, double verwachteKosten, int verwachteReparatieTijd, string afdelingsAfkorting, string probleem, string categorie);
        bool WijzigTicketstatus(long ticketnr, TicketStatus status);
        bool VerwijderTicket(long ticketnr);

        bool VoegRegistratieToe(long ticketnr, Registratie registratie);
        bool VoegReparatieRegistratieToe(long ticketnr, ReparatieRegistratie registratie);

        Product VoegProductToe(long klantnr, int garantieTijd, string omschrijving);
        bool VerwijderProduct(long productnr);

        List<Klant> GetKlanten();
        Klant GetKlantByKlantnr(long klantnr);
        List<Klant> ZoekKlanten(KlantFilter filter);
        List<Ticket> GetTickets();
        Ticket GetTicketByTicketnr(long ticketnr);
        List<Ticket> ZoekTickets(TicketFilter filter);
        List<Registratie> GetRegistratiesByTicket(long ticketnr);
        List<ReparatieRegistratie> GetReparatieRegistratiesByTicket(long ticketnr);
        List<Product> GetProductenByKlant(long klantnr);

        List<Afdeling> GetAfdelingen();

        Medewerker VoegMedewerkerToe(string naam, string password, string telefoon, string filliaal, MedewerkerFunctie functie);
        bool VerwijderMedewerker(long mednr);
    }
}
