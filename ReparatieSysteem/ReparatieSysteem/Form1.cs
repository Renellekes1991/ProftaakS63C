using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReparatieSysteem
{
    public partial class Form1 : Form
    {
        public enum Toestand
        {
            INLOGGEN,
            KLANT,
            PRODUCTREG,
            TICKETREGTICKET,
            TICKETREGREPARATIE,
            ADMINISTRATOR            
        }

        public enum Gebruiker
        {
            HELPDESK,
            WINKEL,
            REPARATIE,
            ADMIN,
            GEEN
        }

        private Gebruiker gebruiker;
        private Toestand toestand;
        private ReparatieSysteem reparatieSysteem;
        private List<Klant> klantenLijst;
        private Klant klant;

        


        public Form1()
        {            
            InitializeComponent();
            gbLogIn.Location = new Point(340, 200);
            gbAlles.Location = new Point(5, 50);
            reparatieSysteem = new ReparatieSysteem();
            klantenLijst = new List<Klant>();
            klant = null;
            toestand = Toestand.INLOGGEN;
            gebruiker = Gebruiker.GEEN;
            Showtoestand();
            
        }
        
        // 
        /// <summary>
        /// Een van de eerste methoden van het programma
        /// Aan de hand van je medewerkersnummer en je wachtwoord wordt je functie bepaald.
        /// Je functie verandert niet tijdens het programma, daarvoor moet je uitloggen.
        /// Omdat dit een van de eerste methoden is, laden we bij inloggen alvast ale bestaande categorieen en afdelingen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLogin_Click(object sender, EventArgs e)
        {

            Medewerker med = reparatieSysteem.LogIn(Convert.ToInt32(tbGebruiksnaam.Text), tbPassword.Text);
            if (med == null)
            {
                MessageBox.Show("Inloggen lukt hiermee niet! \n\nControleer gebruikersnaam en password.");
                return;
            }
            List<TicketCategorie> categorieen = reparatieSysteem.GetCategorieen();
            List<Ticket> ticketLijst = reparatieSysteem.GetTickets();
            foreach (Ticket t in ticketLijst)
            {
                if (cbGeselecteerdeAfdR.Items.Contains(t.AfdelingsAfkorting) == false)
                {
                    cbGeselecteerdeAfdR.Items.Add(t.AfdelingsAfkorting);
                }
            }

            foreach (TicketCategorie categorie in categorieen)
            {
                cbCategorieR.Items.Add(categorie.ToString());
            }

            if (med.Functie == MedewerkerFunctie.Administrator)
            {
                gebruiker = Gebruiker.ADMIN;
                toestand = Toestand.ADMINISTRATOR;
            }
            if (med.Functie == MedewerkerFunctie.Telefonist)
            {
                gebruiker = Gebruiker.HELPDESK;
                toestand = Toestand.KLANT;
            }
            if (med.Functie == MedewerkerFunctie.Balie)
            {
                gebruiker = Gebruiker.WINKEL;
                toestand = Toestand.TICKETREGTICKET;
            }
            if (med.Functie == MedewerkerFunctie.Reparateur)
            {
                gebruiker = Gebruiker.REPARATIE;
                toestand = Toestand.TICKETREGREPARATIE;
            }
            tbMedewerkerNummerTR.Text = Convert.ToString(med.Mednr);
            tbMedewerkerNummerR.Text = Convert.ToString(med.Mednr);
            Showtoestand();

        }

        
        /// <summary>
        /// Laat de gewenste tabbladen zien behorende bij de toestand. Ook aan de hand van de functie van de gebuiker
        /// worden bepaalde rechten ontleent. 
        /// </summary>
        public void Showtoestand()
        {
            gbAlles.Visible = true;
            gbLogIn.Visible = false;
            lbKlantnummerSnel.Visible = true;
            lbTicketnummerSnel.Visible = true;
            tbKlantnummerSnel.Visible = true;
            tbTicketnummerSnel.Visible = true;
            btZoekenSnel.Visible = true;
            btUitloggen.Visible = true;
            if (gebruiker != Gebruiker.ADMIN && toestand != Toestand.INLOGGEN) // alleen admin krijgt admin rechten
            {
                tbKlant.TabPages.Remove(TabAdministrator);
            }
            if (gebruiker == Gebruiker.HELPDESK) //mag geen Reparatie Registratie toevoegen (wel zien)
            {
                tbMedewerkerNummerR.Enabled = false;
                dtpDatumR.Enabled = false;
                tbOudeMeldingenRR.Enabled = false;
                tbNieuweMeldingRR.Enabled = false;
                tbVervangenOnderdeelRR.Enabled = false;
                tbVervOnderdKostenRR.Enabled = false;
                btVoegVervOnderdToeRR.Enabled = false;
                cbVervangenOnderdelenRR.Enabled = false;
                mtbGewerkteUrenR.Enabled = false;
                btVoegTicketRegToeR.Enabled = false;
                tbNieuweMeldingTR.Enabled = true;
                tabRegistratie.TabPages.Remove(tabReparatieReg);
            }
            if (gebruiker == Gebruiker.REPARATIE) // mag geen Ticket Registratie toevoegen (wel zien)
            {
                tbMedewerkerNummerTR.Enabled = false;
                dtpDatumTR.Enabled = false;
                tbOudeMeldingTR.Enabled = false;
                tbNieuweMeldingTR.Enabled = false;
                btVoegTicketRegToeTR.Enabled = false;
                tbNieuweMeldingRR.Enabled = true;
            }
            if (gebruiker == Gebruiker.ADMIN)
            {
                btWijzigKlant.Visible = true;
                tbNieuweMeldingRR.Enabled = true;
            }

            // Een switch voor de juiste tabbladen bij de toestand te laten zien.
            switch (toestand)
            {
                case Toestand.INLOGGEN:
                    gbAlles.Visible = false;
                    btUitloggen.Visible = false;
                    lbKlantnummerSnel.Visible = false;
                    lbTicketnummerSnel.Visible = false;
                    tbKlantnummerSnel.Visible = false;
                    tbTicketnummerSnel.Visible = false;
                    btZoekenSnel.Visible = false;
                    gbLogIn.Visible = true;
                    break;
                case Toestand.KLANT:
                    tbKlant.SelectedTab = TabKlant;
                    break;
                case Toestand.PRODUCTREG:
                    tbKlant.SelectedTab = TabProductRegistratie;
                    break;
                case Toestand.TICKETREGTICKET:
                    tbKlant.SelectedTab = TabTicketRegistratie;
                    tabRegistratie.SelectedTab = tabTicketReg;
                    List<Ticket> ticketlijst = reparatieSysteem.GetTickets();
                    foreach (Ticket t in ticketlijst)
                    {
                        if (comboBox1.Items.Contains(t.AfdelingsAfkorting) == false)
                        {
                            comboBox1.Items.Add(t.AfdelingsAfkorting);
                        }
                    }
                    break;
                case Toestand.TICKETREGREPARATIE:
                    tbKlant.SelectedTab = TabTicketRegistratie;
                    tabRegistratie.SelectedTab = tabReparatieReg;
                    List<Ticket> ticketlijst2 = reparatieSysteem.GetTickets();
                    foreach (Ticket t in ticketlijst2)
                    {
                        if (comboBox1.Items.Contains(t.AfdelingsAfkorting) == false)
                        {
                            comboBox1.Items.Add(t.AfdelingsAfkorting);
                        }
                    }
                    break;
                case Toestand.ADMINISTRATOR:
                    tbKlant.SelectedTab = TabAdministrator;
                    break;
                default:
                    tbKlant.SelectedTab = TabKlant;
                    MessageBox.Show("Neem contact op met uw IT specialist \n(default case voor de toestand is gebruikt.", "Bug", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        
        /// <summary>
        /// Selecteer in zoekresultaten een item (klant) en de klantgegevens worden verder aangevuld.
        /// Verder worden de listboxen Tickets en Producten behorende bij de geselecteerde klant aangevuld met
        /// tickets en producten bij de desbetreffende klant geregistreert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void liZoekResultaten_SelectedIndexChanged(object sender, EventArgs e)
        {
            klant = klantenLijst[liZoekresultatenKlant.SelectedIndex];
            try
            {
                if (klant.Voornaam != null) tbVoornaamKlant.Text = klant.Voornaam;
                if (klant.Tussenvoegsel != null) tbTussenvoegselKlant.Text = klant.Tussenvoegsel;
                tbAchternaamKlant.Text = klant.Achternaam;
                tbStraatKlant.Text = klant.Straatnaam;
                tbHuisnummerKlant.Text = Convert.ToString(klant.Huisnummer);
                tbPostcodeKlant.Text = klant.Postcode;
                tbPlaatsKlant.Text = klant.Woonplaats;
                if (klant.Land != null) tbLandKlant.Text = klant.Land;
                tbTelefoonnummerKlant.Text = klant.Telefoon;
                tbEmailKlant.Text = klant.Email;
                lbKlantnummerKlant.Visible = true;
                lbKlantnummerKlant.Text = "Klantnummer: " + Convert.ToString(klant.Nr);
            }
            catch { MessageBox.Show("Niet alle klantgegevens zijn correct bekend", "Zoek Klant", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            
            liTickets.Items.Clear();
            Producten.Items.Clear();
            cbProductSerienummersR.Items.Clear();
                List<Product> productenLijst = reparatieSysteem.GetProductenByKlant(Convert.ToInt32(klant.Nr));
                foreach (Product p in productenLijst)
                {
                    Producten.Items.Add(p);
                    cbProductSerienummersR.Items.Add(p);
                }
                List<Ticket> ticketLijst = reparatieSysteem.GetTicketsByKlantnr(Convert.ToInt32(klant.Nr));
                foreach (Ticket t in ticketLijst)
                {
                    liTickets.Items.Add(t);
                }
                
           
        }

        /// <summary>
        /// Springt naar ander tabblad, en enabled daar een aantal textboxen. Klantgegevens worden doorgepaast en
        /// een lijst van alle producten behorende bij klant wordt gemaakt zodat een geldig product gekozen kan worden
        /// waarbij de ticket geregistreerd kan worden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btTicketToevoegenKlant_Click(object sender, EventArgs e)
        {
            if (klant == null)
            {
                MessageBox.Show("Selecteer eerst een klant \nwaarbij je de ticket wilt toevoegen.", "Ticket Toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            tbProbleemR.Text = "";
            tbVerw88Kosten.Text = "";
            tbVerw88ReparatieTijd.Text = "";
            tbLocatieR.Text = "";
            cbCategorieR.SelectedItem = null;
            
            tbTicketnummerR.Enabled = false;
            tbTicketnummerR.Text = "Automatisch gegenereerd";
            tbProbleemR.Enabled = true;
            cbProductSerienummersR.Items.Clear();
            List<Product> productenlijst = reparatieSysteem.GetProductenByKlant(klant.Nr);
            foreach (Product p in productenlijst)
            {
                cbProductSerienummersR.Items.Add(p);
            }
            cbProductSerienummersR.Enabled = true;
            tbVerw88Kosten.Enabled = true;
            tbVerw88ReparatieTijd.Enabled = true;
            tbLocatieR.Enabled = true;
            cbCategorieR.Enabled = true;
            tbKlantnummerR.Text = Convert.ToString(klant.Nr);
            tbKlantnaamR.Text = klant.Achternaam + ", " + klant.Voornaam;
            tbKlantTelnrR.Text = klant.Telefoon;
            cbTicketStatus.SelectedIndex = 0;
            btWijzigTicketR.Text = "Voeg Ticket Toe";
            
            tbKlant.SelectedTab = TabTicketRegistratie;
            
        }
        
        /// <summary>
        /// Switched naar andere tab, passt klantgegevens door en laat alle producten zien die al bij klant geregistreerd zijn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btProductToevoegenKlant_Click(object sender, EventArgs e)
        {
            lantnummerTP.Text = Convert.ToString(klant.Nr);
            tbVoornaamTP.Text = klant.Voornaam;
            tbTussenvoegselTP.Text = klant.Tussenvoegsel;
            tbAchternaamTP.Text = klant.Achternaam;
            tbStraatTP.Text = klant.Straatnaam;
            tbHuisnummerTP.Text = Convert.ToString(klant.Huisnummer);
            tbPostcodeTP.Text = klant.Postcode;
            tbPlaatsTP.Text = klant.Woonplaats;
            tbLandTP.Text = klant.Land;
            tbTelefoonnummerTP.Text = Convert.ToString(klant.Telefoon);
            tbEmailTP.Text = klant.Email;
            Producten.Items.Clear();
            lbProductenTP.Items.Clear();
            List<Product> productenlijst = reparatieSysteem.GetProductenByKlant(klant.Nr);
            foreach (Product p in productenlijst)
            {
                lbProductenTP.Items.Add(p);
            }
            tbKlant.SelectedTab = TabProductRegistratie;
        }

        private void btUitloggen_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        /// <summary>
        /// Zoekt de klant op meegegeven gegevens uit textboxen.
        /// De resultaten (klanten) worden getoont in de zoekresultaten box.
        /// Als er maar 1 zoekresultaat is, wordt deze meteen geselecteerd, en worden de gegevens verder ingevuld.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btZoekenKlant_Click(object sender, EventArgs e)
        {
            KlantFilter klantFilter = new KlantFilter();
            lbKlantnummerKlant.Visible = false;
            if (tbVoornaamKlant.Text != "") klantFilter.Voornaam = tbVoornaamKlant.Text;
            if (tbTussenvoegselKlant.Text != "") klantFilter.Tussenvoegsel = tbTussenvoegselKlant.Text;
            if (tbAchternaamKlant.Text != "") klantFilter.Achternaam = tbAchternaamKlant.Text;
            if (tbStraatKlant.Text != "") klantFilter.Straatnaam = tbStraatKlant.Text;
            if (tbHuisnummerKlant.Text != "") klantFilter.Huisnummer = Convert.ToInt32(tbHuisnummerKlant.Text);
            if (tbPostcodeKlant.Text != "") klantFilter.Postcode = tbPostcodeKlant.Text;
            if (tbPlaatsKlant.Text != "") klantFilter.Woonplaats = tbPlaatsKlant.Text;
            if (tbLandKlant.Text != "") klantFilter.Land = tbLandKlant.Text;
            if (tbTelefoonnummerKlant.Text != "") klantFilter.Telefoon = Convert.ToInt32(tbTelefoonnummerKlant.Text);
            if (tbEmailKlant.Text != "") klantFilter.Email = tbEmailKlant.Text;

            liZoekresultatenKlant.Items.Clear();
            klantenLijst = reparatieSysteem.ZoekKlanten(klantFilter);
            if (klantenLijst != null)
            {
                
                if (klantenLijst.Count == 0)
                {
                    klant = null;
                    lbKlantnummerKlant.Visible = false;
                    MessageBox.Show("Geen klanten voldoen \naan de zoekcriteria.", "Zoek Klant", MessageBoxButtons.OK);
                    return;
                }
                if (klantenLijst.Count == 1) // Meteen wordt deze klant geselecteerd.
                {
                    klant = klantenLijst[0];
                    try
                    {
                        if (klant.Voornaam != null) tbVoornaamKlant.Text = klant.Voornaam;
                        if (klant.Tussenvoegsel != null) tbTussenvoegselKlant.Text = klant.Tussenvoegsel;
                        tbAchternaamKlant.Text = klant.Achternaam;
                        tbStraatKlant.Text = klant.Straatnaam;
                        tbHuisnummerKlant.Text = Convert.ToString(klant.Huisnummer);
                        tbPostcodeKlant.Text = klant.Postcode;
                        tbPlaatsKlant.Text = klant.Woonplaats;
                        if (klant.Land != null) tbLandKlant.Text = klant.Land;
                        tbTelefoonnummerKlant.Text = klant.Telefoon;
                        tbEmailKlant.Text = klant.Email;
                        lbKlantnummerKlant.Visible = true;
                        lbKlantnummerKlant.Text = "Klantnummer: " + Convert.ToString(klant.Nr);
                        liZoekresultatenKlant.Items.Add(klant.ToString());
                    }
                    catch { MessageBox.Show("Niet alle klantgegevens zijn correct bekend", "Zoek Klant", MessageBoxButtons.OK); return; }

                    liTickets.Items.Clear();
                    Producten.Items.Clear();
                    cbProductSerienummersR.Items.Clear();

                    //try
                    //{
                        List<Product> productenLijst = reparatieSysteem.GetProductenByKlant(Convert.ToInt32(klant.Nr));
                        foreach (Product p in productenLijst)
                        {
                            Producten.Items.Add(p);
                            cbProductSerienummersR.Items.Add(p);
                        }
                        List<Ticket> ticketLijst = reparatieSysteem.GetTicketsByKlantnr(Convert.ToInt32(klant.Nr));
                        foreach (Ticket t in ticketLijst)
                        {
                            liTickets.Items.Add(t);
                        }
                    //}
                    //catch { MessageBox.Show("Niet alle Tickets en Producten \nkunnen geladen worden bij het Klantnummer.", "Zoek Klant", MessageBoxButtons.OK); }
                }

                else
                {
                    klant = null;
                    lbKlantnummerKlant.Visible = false;
                    foreach (Klant k in klantenLijst)
                    {
                        liZoekresultatenKlant.Items.Add(k);
                    }
                }
            }
            else
            {
                MessageBox.Show("Er is een fout opgetreden \nmet de database.", "Zoek klant", MessageBoxButtons.OK);
            }
                
        }

        /// <summary>
        /// Checkt of alle benodigde velden niet leeg zijn; voegt daarna een klant toe aan de database.
        /// databsemethode retourneert een klant, als deze null is, is toevoegen mislukt, anders gelukt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVoegToeKlant_Click(object sender, EventArgs e)
        {
            if (tbAchternaamKlant.Text != "" && tbPlaatsKlant.Text != "" && tbPostcodeKlant.Text != "" && tbStraatKlant.Text != "" && tbHuisnummerKlant.Text != "" && tbEmailKlant.Text != "" && tbTelefoonnummerKlant.Text != "")
            {
                klant = reparatieSysteem.VoegKlantToe(tbVoornaamKlant.Text, tbTussenvoegselKlant.Text, tbAchternaamKlant.Text, tbPlaatsKlant.Text, tbPostcodeKlant.Text, tbStraatKlant.Text, Convert.ToInt32(tbHuisnummerKlant.Text), tbEmailKlant.Text, tbTelefoonnummerKlant.Text, tbLandKlant.Text);
                if (klant == null)
                {
                    MessageBox.Show("Het toevoegen van de klant is mislukt.", "Klant toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lbKlantnummerKlant.Text = Convert.ToString(klant.Nr);
                lbKlantnummerKlant.Visible = true;
                MessageBox.Show("Klant is succesvol toegevoegd.", "Klant toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Alleen admin mag gebruiker verwijderen. 
        /// Zoekt de klant met de klantfilter, als resultaat 1 klant is, dan wordt gevraagd of je em echt wilt verwijderen
        /// Zo ja, dan wordt de klant verwijderd.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVerwijderKlant_Click(object sender, EventArgs e)
        {
            if (gebruiker == Gebruiker.ADMIN)
            {
                KlantFilter klantFilter = new KlantFilter();
                if (tbVoornaamKlant.Text != "") klantFilter.Voornaam = tbVoornaamKlant.Text;
                if (tbTussenvoegselKlant.Text != "") klantFilter.Tussenvoegsel = tbTussenvoegselKlant.Text;
                if (tbAchternaamKlant.Text != "") klantFilter.Achternaam = tbAchternaamKlant.Text;
                if (tbStraatKlant.Text != "") klantFilter.Straatnaam = tbStraatKlant.Text;
                if (tbHuisnummerKlant.Text != "") klantFilter.Huisnummer = Convert.ToInt32(tbHuisnummerKlant.Text);
                if (tbPostcodeKlant.Text != "") klantFilter.Postcode = tbPostcodeKlant.Text;
                if (tbPlaatsKlant.Text != "") klantFilter.Woonplaats = tbPlaatsKlant.Text;
                if (tbLandKlant.Text != "") klantFilter.Land = tbLandKlant.Text;
                if (tbTelefoonnummerKlant.Text != "") klantFilter.Telefoon = Convert.ToInt32(tbTelefoonnummerKlant.Text);
                if (tbEmailKlant.Text != "") klantFilter.Email = tbEmailKlant.Text;
                klantenLijst = reparatieSysteem.ZoekKlanten(klantFilter);
                if (klantenLijst.Count == 1)
                {
                    klant = klantenLijst[0];

                    if (MessageBox.Show("Wilt u \n" + klant.ToString() + "\nverwijderen?.", "Klant verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        reparatieSysteem.VerwijderKlant(Convert.ToInt32(klant.Nr));
                        MessageBox.Show(klant.ToString() + "\nis succesvol verwijderd.", "Klant verwijderen", MessageBoxButtons.OK);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Meerdere klanten voldoen \naan deze zoekcriteria.", "Klant verwijderen", MessageBoxButtons.OK);
                    return;
                }
            }
        
                    
        }

        /// <summary>
        /// Zoekt Ticket of Klant bij nummer.
        /// Dan switched 'ie de toestand afhankelijk van de gebruiker.
        /// Zoeken op ticket; de klant en product bij ticket worden geladen
        /// Zoeken op klant; alle producten en tickets worden bij de klant geladen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btZoekenSnel_Click(object sender, EventArgs e)
        {
            liZoekresultatenKlant.Items.Clear();
            Producten.Items.Clear();
            liTickets.Items.Clear();
            if (tbTicketnummerSnel.Text != "")  // Zoeken op ticketnummer //////////////////////////
            {
                Ticket t;

                t = reparatieSysteem.GetTicketByTicketnr(Convert.ToInt32(tbTicketnummerSnel.Text));

                if (t == null)
                {
                    MessageBox.Show("Ticket kan niet gevonden worden.", "Ticket zoeken", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Ticketinformatie wordt ingevuld
                tbNieuweMeldingTR.Enabled = true;
                btVoegTicketRegToeTR.Enabled = true;
                cbProductSerienummersR.Items.Clear();
                tbTicketnummerR.Text = Convert.ToString(t.Ticketnr);
                tbProbleemR.Text = t.Probleem;
                Product p = reparatieSysteem.GetProductByTicket(Convert.ToInt32(t.Ticketnr));
                cbProductSerienummersR.Items.Add(p);
                cbProductSerienummersR.SelectedIndex = 0;
                tbVerw88Kosten.Text = Convert.ToString(t.VerwachteKosten);
                tbLocatieR.Text = t.AfdelingsAfkorting;
                cbCategorieR.SelectedItem = t.Categorie;
                tbVerw88ReparatieTijd.Text = t.VerwachteReparatieTijd.ToString();
                cbTicketStatus.SelectedIndex = Convert.ToInt32(t.Status);
                Klant k = reparatieSysteem.GetKlantByTicketnr(Convert.ToInt32(t.Ticketnr));
                tbKlantnaamR.Text = k.Achternaam;
                tbKlantnummerR.Text = k.Nr.ToString();
                tbKlantTelnrR.Text = k.Telefoon;
                
                // Switchs nu naar de toestand adhv gebruiker
                if (gebruiker == Gebruiker.HELPDESK || gebruiker == Gebruiker.WINKEL)
                {
                    toestand = Toestand.TICKETREGTICKET;
                    Showtoestand();
                }
                else
                {
                    toestand = Toestand.TICKETREGREPARATIE;
                    Showtoestand();
                }
            }
            if (tbKlantnummerSnel.Text != "") // Zoeken op klantnummer ///////////////////////////
            {
                
                    klant = reparatieSysteem.GetKlantByKlantnr(Convert.ToInt32(tbKlantnummerSnel.Text));
                
                if (klant == null)
                {
                    MessageBox.Show("Klant kan niet gevonden worden.", "Klant zoeken", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // klantinformatie wordt ingevuld
                lbKlantnummerKlant.Visible = true;
                lbKlantnummerKlant.Text = "Klantnummer: " + Convert.ToString(klant.Nr);
                tbVoornaamKlant.Text = klant.Voornaam;
                tbTussenvoegselKlant.Text = klant.Tussenvoegsel;
                tbAchternaamKlant.Text = klant.Achternaam;
                tbStraatKlant.Text = klant.Straatnaam;
                tbHuisnummerKlant.Text = Convert.ToString(klant.Huisnummer);
                tbPostcodeKlant.Text = klant.Postcode;
                tbPlaatsKlant.Text = klant.Woonplaats;
                tbLandKlant.Text = klant.Land;
                tbTelefoonnummerKlant.Text = klant.Telefoon;
                tbEmailKlant.Text = klant.Email;
                klantenLijst.Add(klant);
                liZoekresultatenKlant.Items.Add(klant.ToString());
                
                // lijsten worden gevuld met producten en tickets bij de gevonden klant
                Producten.Items.Clear();
                liTickets.Items.Clear();
                cbProductSerienummersR.Items.Clear();
                List<Product> productenlijst = reparatieSysteem.GetProductenByKlant(klant.Nr);
                foreach (Product p in productenlijst)
                {
                    Producten.Items.Add(p);
                    cbProductSerienummersR.Items.Add(p);
                }
                List<Ticket> ticketlijst = reparatieSysteem.GetTicketsByKlantnr(klant.Nr);
                foreach (Ticket t in ticketlijst)
                {
                    liTickets.Items.Add(t);
                }

                toestand = Toestand.KLANT;
                Showtoestand();
            }

        }

        
        /// <summary>
        /// Alleen admin mag gebruiker wijzigen
        /// er moet eerst een klant geselecteerd zijn
        /// Alle velden behalve klantnummer kunnen gewijzig worden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btWijzigKlant_Click(object sender, EventArgs e)
        {
            if (gebruiker == Gebruiker.ADMIN)
            {
                if (klant != null)
                {
                    if (tbVoornaamKlant.Text != "") klant.Voornaam = tbVoornaamKlant.Text;
                    if (tbTussenvoegselKlant.Text != "") klant.Tussenvoegsel = tbTussenvoegselKlant.Text;
                    if (tbAchternaamKlant.Text != "") klant.Achternaam = tbAchternaamKlant.Text;
                    if (tbStraatKlant.Text != "") klant.Straatnaam = tbStraatKlant.Text;
                    if (tbHuisnummerKlant.Text != "") klant.Huisnummer = Convert.ToInt32(tbHuisnummerKlant.Text);
                    if (tbPostcodeKlant.Text != "") klant.Postcode = tbPostcodeKlant.Text;
                    if (tbPlaatsKlant.Text != "") klant.Woonplaats = tbPlaatsKlant.Text;
                    if (tbLandKlant.Text != "") klant.Land = tbLandKlant.Text;
                    if (tbTelefoonnummerKlant.Text != "") klant.Telefoon = tbTelefoonnummerKlant.Text;
                    if (tbEmailKlant.Text != "") klant.Email = tbEmailKlant.Text;
                    if (reparatieSysteem.WijzigKlant(klant) == true)
                    {
                        MessageBox.Show("Klant met klantnummer " + klant.Nr.ToString() + "\nis succesvol gewijzigd", "Klant Wijzigen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Klant wijzigen is mislukt", "Klant wijzigen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
        }

        /// <summary>
        /// Maakt alle Zoekvelden van klant leeg, en maakt de variabele "klant" null.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVeldenVerwijderenKlant_Click(object sender, EventArgs e)
        {
            klant = null;
            Producten.Items.Clear();
            liTickets.Items.Clear();
            liZoekresultatenKlant.Items.Clear();
            lbKlantnummerKlant.Visible = false;
            tbVoornaamKlant.Text = "";
            tbTussenvoegselKlant.Text = "";
            tbAchternaamKlant.Text = "";
            tbStraatKlant.Text = "";
            tbHuisnummerKlant.Text = "";
            tbPostcodeKlant.Text = "";
            tbPlaatsKlant.Text = "";
            tbLandKlant.Text = "";
            tbTelefoonnummerKlant.Text = "";
            tbEmailKlant.Text = "";
        }

        /// <summary>
        /// Knop heeft verschillende functies; Ticket toevoegen, textboxen 'UNLOCKEN', en ticket wijzigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btWijzigTicketR_Click(object sender, EventArgs e)
        {
            if (btWijzigTicketR.Text == "Voeg Ticket Toe")
            {
                if (cbProductSerienummersR.SelectedItem == null)
                {
                    MessageBox.Show("Bij welk product hoort deze ticket?", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                if (tbVerw88Kosten.Text == "")
                {
                    MessageBox.Show("Vul de verwachte kosten in", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (tbVerw88ReparatieTijd.Text == "")
                {
                    MessageBox.Show("Vul de verwachte reparatietijd in", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (tbLocatieR.Text == "")
                {
                    MessageBox.Show("Vul de locatie in", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (tbProbleemR.Text == "")
                {
                    MessageBox.Show("Vul het probleem in", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (cbCategorieR.SelectedItem == null)
                {
                    MessageBox.Show("Geef de categorie aan", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Product p = (Product)cbProductSerienummersR.SelectedItem;
                Ticket t = reparatieSysteem.VoegTicketToe(klant.Nr, Convert.ToInt32(p.Productnr), Convert.ToDouble(tbVerw88Kosten.Text), Convert.ToInt32(tbVerw88ReparatieTijd.Text), tbLocatieR.Text.Substring(0, Math.Min(tbLocatieR.Text.Length, 4)), tbProbleemR.Text, Convert.ToString(cbCategorieR.SelectedItem));
                if (t == null)
                {
                    MessageBox.Show("Ticket is niet goed toegevoegd", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                tbTicketnummerR.Enabled = false;
                tbTicketnummerR.Text = Convert.ToString(t.Ticketnr);
                tbProbleemR.Enabled = false;
                cbProductSerienummersR.Enabled = false;
                tbVerw88Kosten.Enabled = false;
                tbVerw88ReparatieTijd.Enabled = false;
                tbLocatieR.Enabled = false;
                cbCategorieR.Enabled = false;
                cbTicketStatus.SelectedItem = TicketStatus.INGEVOERD;
                btWijzigTicketR.Text = "Unlock Tekstboxen Voor Ticket Wijzigen";
                MessageBox.Show("Ticket is succesvol toegevoegd", "Ticket toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (btWijzigTicketR.Text == "Unlock Tekstboxen Voor Ticket Wijzigen") // Maakt de textboxen beschikbaar voor veranderen gegevens
            {
                tbTicketnummerR.Enabled = false;
                tbProbleemR.Enabled = true;
                cbProductSerienummersR.Enabled = true;
                tbVerw88Kosten.Enabled = true;
                tbVerw88ReparatieTijd.Enabled = true;
                tbLocatieR.Enabled = true;
                cbCategorieR.Enabled = true;
                cbTicketStatus.Enabled = true;
                btWijzigTicketR.Text = "Wijzig Ticket";
                return;
            }

            if (btWijzigTicketR.Text == "Wijzig Ticket") // Wijzigt de veranderde gegevens. Alleen nummer kan niet veranderd worden
            {
                Ticket t = reparatieSysteem.GetTicketByTicketnr(Convert.ToInt32(tbTicketnummerR.Text));
                t.Probleem = tbProbleemR.Text;
                t.VerwachteKosten = Convert.ToDouble(tbVerw88Kosten.Text);
                t.VerwachteReparatieTijd = Convert.ToInt32(tbVerw88ReparatieTijd.Text);
                t.AfdelingsAfkorting = tbLocatieR.Text;
                t.Categorie = cbCategorieR.SelectedItem.ToString();
                t.Status = (TicketStatus)cbTicketStatus.SelectedIndex;
                Product p = (Product)cbProductSerienummersR.SelectedItem;
                if (reparatieSysteem.WijzigTicket(t, Convert.ToInt32(p.Productnr)) == true)
                {
                    MessageBox.Show("Ticket succesvol gewijzigd", "Ticket wijzigen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbTicketnummerR.Enabled = false;
                    tbProbleemR.Enabled = false;
                    cbProductSerienummersR.Enabled = false;
                    tbVerw88Kosten.Enabled = false;
                    tbVerw88ReparatieTijd.Enabled = false;
                    tbLocatieR.Enabled = false;
                    cbCategorieR.Enabled = false;
                    cbTicketStatus.Enabled = false;
                    btWijzigTicketR.Text = "Unlock Tekstboxen Voor Ticket Wijzigen";
                }
            }



        }

        
        /// <summary>
        /// Controleert of velden zijn ingevuld en voegt een product toe aan de klant.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btProductToevoegenProductTP_Click(object sender, EventArgs e)
        {
            int serienummer = -1;
            try
            {
                serienummer = Convert.ToInt32(tbSerienummerTP.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Voer een getal in bij SERIENUMMER", "Product toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbGarantieTP.Text == "")
            {
                MessageBox.Show("Geef een GARANTIETIJD mee", "Product toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbOmschrijvingProductTP.Text == "")
            {
                MessageBox.Show("Geef een productomschrijving mee", "Product toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Product p = reparatieSysteem.VoegProductToe(serienummer, klant.Nr, tbGarantieTP.Text, tbOmschrijvingProductTP.Text);
            if (p != null)
            {
                lbProductenTP.Items.Add(p);
                MessageBox.Show("Product is toegevoegd", "Product toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Product is niet toegevoegd", "Product toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Klik op een ticket in de ticketlijst en je sprint automatisch naar Ticket tabblad.
        /// Ticketgegevens worden geladen, en product wordt bij de ticket geladen
        /// Relevante textvoxen worden ENABLED
        /// Adhv gebruiker worden rechten bepaald voor ticketregistratie en reparatieregistratie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void liTickets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            Ticket t = (Ticket)listbox.SelectedItem;
            if (t == null) return;
            tbTicketnummerR.Text = t.Ticketnr.ToString();
            tbProbleemR.Text = t.Probleem;
            cbTicketStatus.SelectedIndex = Convert.ToInt32(t.Status);
            cbProductSerienummersR.Items.Clear();
            Product p = reparatieSysteem.GetProductByTicket(Convert.ToInt32(t.Ticketnr));
            cbProductSerienummersR.Items.Clear();
            cbProductSerienummersR.Items.Add(p);
            cbProductSerienummersR.SelectedIndex = 0;
            tbVerw88Kosten.Text = t.VerwachteKosten.ToString();
            tbVerw88ReparatieTijd.Text = t.VerwachteReparatieTijd.ToString();
            tbLocatieR.Text = t.AfdelingsAfkorting;
            cbCategorieR.SelectedItem = t.Categorie;
            cbTicketStatus.SelectedItem = t.Status.ToString();
            Klant k = reparatieSysteem.GetKlantByTicketnr(Convert.ToInt32(t.Ticketnr));
            tbKlantnummerR.Text = k.Nr.ToString();
            tbKlantnaamR.Text = k.Achternaam;
            tbKlantTelnrR.Text = k.Telefoon;
            
            // Sprint door naar de juiste toestand
            if (gebruiker == Gebruiker.HELPDESK || gebruiker == Gebruiker.WINKEL)
            {
                toestand = Toestand.TICKETREGTICKET;
                tbOudeMeldingTR.Text = "";
                List<Registratie> regLijst = reparatieSysteem.GetRegistratiesByTicket(Convert.ToInt32(t.Ticketnr));
                
                for (int i = 0; i < regLijst.Count; i++)
                {
                    tbOudeMeldingTR.Text += regLijst[i].Vermelding + "\r\n";
                }
                tbNieuweMeldingTR.Enabled = true;
                btVoegTicketRegToeTR.Enabled = true;
                Showtoestand();
                
            }
            else if (gebruiker == Gebruiker.ADMIN)
            {
                toestand = Toestand.TICKETREGREPARATIE;
                tbOudeMeldingenRR.Text = "";
                tbNieuweMeldingTR.Enabled = true;
                btVoegTicketRegToeTR.Enabled = true;
                tbOudeMeldingenRR.Enabled = false;
                tbNieuweMeldingRR.Enabled = true;
                tbVervangenOnderdeelRR.Enabled = true;
                tbVervOnderdKostenRR.Enabled = true;
                cbVervangenOnderdelenRR.Enabled = true;
                btVoegVervOnderdToeRR.Enabled = true;
                mtbGewerkteUrenR.Enabled = true;
                btVoegTicketRegToeR.Enabled = true;
                dtpDatumR.Enabled = true;
                tbNieuweMeldingRR.Enabled = true;
                
                // Laadt meldingen bij de tickets
                List<ReparatieRegistratie> repRegLijst = reparatieSysteem.GetReparatieRegistratiesByTicket(Convert.ToInt32(t.Ticketnr));
                foreach (ReparatieRegistratie repReg in repRegLijst)
                {
                    tbOudeMeldingenRR.Text += repReg.Vermelding + "\r\n";
                   
                }
                List<Registratie> regLijst = reparatieSysteem.GetRegistratiesByTicket(Convert.ToInt32(t.Ticketnr));
                for (int j = 0; j < regLijst.Count; j++)
                {
                    tbOudeMeldingTR.Text += regLijst[j].Vermelding + "\r\n";
                }
                Showtoestand();
            }
            else
            {
                toestand = Toestand.TICKETREGREPARATIE;
                tbOudeMeldingenRR.Text = "";
                tbNieuweMeldingRR.Enabled = true;
                tbVervangenOnderdeelRR.Enabled = true;
                tbVervOnderdKostenRR.Enabled = true;
                cbVervangenOnderdelenRR.Enabled = true;
                btVoegVervOnderdToeRR.Enabled = true;
                mtbGewerkteUrenR.Enabled = true;
                btVoegTicketRegToeR.Enabled = true;
                
                // Laadt meldingen bij de tickets
                List<ReparatieRegistratie> repRegLijst = reparatieSysteem.GetReparatieRegistratiesByTicket(Convert.ToInt32(t.Ticketnr));
                foreach (ReparatieRegistratie repReg in repRegLijst)
                {
                    tbOudeMeldingenRR.Text += repReg.Vermelding + "\r\n";
                }
                Showtoestand();
                
            }
        }

        
        /// <summary>
        /// Klik op een product uit de productenlijst en je sprint naar tabblad ticket.
        /// Daar kun je een ticket aanmaken voor het geselecteerde product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Producten_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            Product p = (Product)listbox.SelectedItem;
            btWijzigTicketR.Text = "Voeg Ticket Toe";
            cbProductSerienummersR.Items.Clear();
            cbProductSerienummersR.Items.Add(p);
            cbProductSerienummersR.SelectedIndex = 0;
            tbTicketnummerR.Enabled = false;
            tbTicketnummerR.Text = "Automatisch gegenereerd";
            tbProbleemR.Enabled = true;
            cbProductSerienummersR.Enabled = true;
            tbVerw88Kosten.Enabled = true;
            tbVerw88ReparatieTijd.Enabled = true;
            tbLocatieR.Enabled = true;
            cbCategorieR.Enabled = true;
            cbTicketStatus.SelectedIndex = 0;
            tbKlantnummerR.Text = klant.Nr.ToString();
            tbKlantnaamR.Text = klant.Achternaam;
            tbKlantTelnrR.Text = klant.Telefoon;
            if (gebruiker == Gebruiker.HELPDESK || gebruiker == Gebruiker.WINKEL)
            {
                toestand = Toestand.TICKETREGTICKET;
                Showtoestand();
            }
            else
            {
                toestand = Toestand.TICKETREGREPARATIE;
                Showtoestand();
            }
        }

       
        /// <summary>
        /// Klik hier voor een lijst van alle producten die weggebracht kunnen worden.
        /// Je kunt ook een locatie selecteren;m dan worden alle producten getoond ^^^^ op die locatie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btProductOphaalLijstR_Click(object sender, EventArgs e)
        {
            if (cbGeselecteerdeAfdR.SelectedItem == null)
            {
                List<Ticket> ticketlijst = reparatieSysteem.GetTickets();
                List<Ticket> ophaalTicketlijst = new List<Ticket>();
                foreach (Ticket t in ticketlijst)
                {
                    if (t.Status == TicketStatus.GEREPAREERD) 
                    {
                        ophaalTicketlijst.Add(t);
                    }
                }
                String boxstring = "";
                foreach (Ticket t in ophaalTicketlijst)
                {
                    boxstring += (t.ToString() + ", " + t.AfdelingsAfkorting) + "\n";
                }
                MessageBox.Show(boxstring, "Tickets die opgehaald kunnen worden", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                List<Ticket> ticketlijst = reparatieSysteem.GetTickets();
                List<Ticket> ophaalTicketlijst = new List<Ticket>();
                foreach (Ticket t in ticketlijst)
                {
                    if (t.Status == TicketStatus.GEREPAREERD && t.AfdelingsAfkorting == cbGeselecteerdeAfdR.SelectedItem.ToString()) 
                    {
                        ophaalTicketlijst.Add(t);
                    }
                }
                String boxstring = "";
                foreach (Ticket t in ophaalTicketlijst)
                {
                    boxstring += (t.ToString() + t.AfdelingsAfkorting) + "\n";
                }
                MessageBox.Show(boxstring, "Tickets die opgehaald kunnen worden \nop afdeling" + cbGeselecteerdeAfdR.SelectedItem.ToString(), MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

       
        /// <summary>
        /// Voor onderstaande methode
        /// Deze lijst bevat alle afdelingen waar een of meer tickets zijn.
        /// selecteer een afdeling en je krijgt een lijst met tickets op dit moment aanwezig op de locatie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTicketsOpAfdelingR.Items.Clear();
            ComboBox box = (ComboBox)sender;
            String afdeling = box.SelectedItem.ToString(); ;

            List<Ticket> ticketlijst = reparatieSysteem.GetTickets();
            foreach (Ticket t in ticketlijst)
            {
                if (t.AfdelingsAfkorting == afdeling)
                {
                    lbTicketsOpAfdelingR.Items.Add(t);
                }
            }
        }
        
              
        /// <summary>
        /// :-D is hetzelfde als een tickets selecteren uit de ticketlijst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbTicketsOpAfdelingR_SelectedIndexChanged(object sender, EventArgs e)
        {
            liTickets_SelectedIndexChanged(sender, e);
        }

        private void tbTicketnummerSnel_TextChanged(object sender, EventArgs e)
        {
            tbKlantnummerSnel.Text = "";
        }

        private void tbKlantnummerSnel_TextChanged(object sender, EventArgs e)
        {
            tbTicketnummerSnel.Text = "";
        }

        
        /// <summary>
        /// Zoekt de medewerker bij naam of bij nummer en vult daarna de velden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btZoekMedewerkerAdmin_Click(object sender, EventArgs e)
        {
            if (tbMedewerkernummerAdmin.Text == "" && tbNaamAdmin.Text == "")
            {
                MessageBox.Show("Zoek op medewerkernummer of naam", "Medewerker zoeken", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tbMedewerkernummerAdmin.Text != "")
            {
                Medewerker m = reparatieSysteem.GetMederwerkerByNummer(Convert.ToInt32(tbMedewerkernummerAdmin.Text));
                if (m == null)
                {
                    MessageBox.Show("Geen medewerkers bekent met dit nummer", "Medewerker zoeken", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    tbMedewerkernummerAdmin.Text = m.Mednr.ToString();
                    tbNaamAdmin.Text = m.Naam;
                    cbFunctieMedewerkerAdmin.SelectedIndex = Convert.ToInt32(m.Functie);
                    cbFunctieMedewerkerAdmin.SelectedItem = m.Functie.ToString();//  bepaalMedewerkerFunctieIndex(m.Functie);
                    tbAfdelingMedewerkerAdmin.Text = m.Filiaal;
                    tbPaswoordAdmin.Text = m.Password;
                    tbTelefoonnummerAdmin.Text = m.Telefoon;
                    return;
                }
            }
            else
            {
                if (tbNaamAdmin.Text != "")
                {
                    Medewerker m = reparatieSysteem.GetMedewerkerByNaam(tbNaamAdmin.Text);
                    if (m == null)
                    {
                        MessageBox.Show("Geen medewerkers bekent met deze naam", "Medewerker zoeken", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        tbMedewerkernummerAdmin.Text = m.Mednr.ToString();
                        tbNaamAdmin.Text = m.Naam;
                        cbFunctieMedewerkerAdmin.SelectedIndex = bepaalMedewerkerFunctieIndex(m.Functie);
                        tbAfdelingMedewerkerAdmin.Text = m.Filiaal;
                        tbPaswoordAdmin.Text = m.Password;
                        tbTelefoonnummerAdmin.Text = m.Telefoon;
                      
                    }
                }
            }
        

        }

        /// <summary>
        /// AHV medewerkernummer (eerst zoeken of ingeven) kun je de medewerker wijzigen op alle velden behalve nummer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBewerkMedewerkerAdmin_Click(object sender, EventArgs e)
        {
            if (tbMedewerkernummerAdmin.Text == "")
            {
                MessageBox.Show("Selecteer eerst een medewerker /n(Zoeken gaat op medewerkernummer)", "Medewerker wijzigen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Medewerker m = null;
            try
            {
                m = new Medewerker(Convert.ToInt64(tbMedewerkernummerAdmin.Text), tbNaamAdmin.Text, tbPaswoordAdmin.Text, tbTelefoonnummerAdmin.Text, tbAfdelingMedewerkerAdmin.Text, (MedewerkerFunctie)bepaalMedewerkerFunctieIndex(cbFunctieMedewerkerAdmin.Text));
            }
            catch (FormatException) { return; }
            if (m == null)
            {
                MessageBox.Show("Geen medewerkers bekent met dit nummer", "Medewerker wijzigen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (reparatieSysteem.WijzigMedewerker(m))
            {
                MessageBox.Show("Medewerker is succesvol gewijzigd", "Medewerker wijzigen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Medewerker wijzigen is niet gelukt", "Medewerker wijzigen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        /// <summary>
        /// Voegt een medewerker toe AHV meegegeven data in textboxen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVoegMedewerkerToeAdmin_Click(object sender, EventArgs e)
        {
            if (tbNaamAdmin.Text == "" || cbFunctieMedewerkerAdmin.SelectedItem == null || tbAfdelingMedewerkerAdmin.Text == "" || tbPaswoordAdmin.Text == "" || tbTelefoonnummerAdmin.Text == "")
            {
                MessageBox.Show("Alle velden behalve het nummer zijn \nverplicht voor het toevoegen van een medewerker", "Medewerker toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            Medewerker m = reparatieSysteem.VoegMedewerkerToe(tbNaamAdmin.Text, tbPaswoordAdmin.Text, tbTelefoonnummerAdmin.Text, tbAfdelingMedewerkerAdmin.Text, bepaalMedewerkerFunctieIndex(Convert.ToString(cbFunctieMedewerkerAdmin.SelectedItem)));
           
            if (m == null)
            {
                MessageBox.Show("Medewerker is niet correct ingevoerd", "Medewerker toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Medewerker is correct toegevoegd", "Medewerker toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        
        /// <summary>
        /// overbodig, je kunt casten
        /// </summary>
        /// <param name="functie"></param>
        /// <returns></returns>
        private int bepaalMedewerkerFunctieIndex(MedewerkerFunctie functie)
        {
            switch (functie)
            {
                case  MedewerkerFunctie.Reparateur:
                    return 0;
                case MedewerkerFunctie.Balie:
                    return 1;
                case MedewerkerFunctie.Telefonist:
                    return 2;
                case MedewerkerFunctie.Administrator:
                    return 3;
                default:
                    return 0;
            }
        }

        
        /// <summary>
        /// AHV functiestring, returnt ie de enumeratie index
        /// </summary>
        /// <param name="functie"></param>
        /// <returns></returns>
        private int bepaalMedewerkerFunctieIndex(string functie)
        {
            switch (functie)
            {
                case "Reparateur":
                    return 0;
                case "Winkel":
                    return 1;
                case "Helpdesk":
                    return 2;
                case "Administrator":
                    return 3;
                default:
                    return 0;
            }
        }

        
        /// <summary>
        /// Voegt een ticketregistratie toe ahv meegegeven data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVoegTicketRegToeTR_Click(object sender, EventArgs e)
        {
            if (tbTicketnummerR.Text == "")
            {
                MessageBox.Show("Selecteer een ticket waarbij \nje de registratie wilt toevoegen", "Ticketregistratie toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Ticket t = reparatieSysteem.GetTicketByTicketnr(Convert.ToInt32(tbTicketnummerR.Text));
            Registratie registratie = new Registratie();
            registratie.Mednr = Convert.ToInt64(tbMedewerkerNummerTR.Text);
            registratie.Datum = dtpDatumTR.Value;
            registratie.Vermelding = tbNieuweMeldingTR.Text;
            if (reparatieSysteem.VoegRegistratieToe(Convert.ToInt32(t.Ticketnr), registratie))
            {
                tbOudeMeldingTR.Text += registratie.Vermelding + "\r\n";
                tbNieuweMeldingTR.Text = "";
                MessageBox.Show("Ticketregistratie is correct toegevoegd", "Ticketregistratie toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ticketregistratie is mislukt", "Ticketregistratie toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// <summary>
        /// Voegt een vervangen onderdeel toe aan de lijst met vervangen onderdelen
        /// Als onderdeel van je data voor je reparatieregistratie, hieronder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVoegVervOnderdToeRR_Click(object sender, EventArgs e)
        {
            VervangenOnderdeel vo = null;
            try
            {
                vo = new VervangenOnderdeel(tbVervangenOnderdeelRR.Text, Convert.ToDouble(tbVervOnderdKostenRR.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Onjuiste gegevens", "Onderdeel toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cbVervangenOnderdelenRR.Items.Add(vo);
            tbVervangenOnderdeelRR.Text = "";
            tbVervOnderdKostenRR.Text = "";
            MessageBox.Show("Succesvol toegevoegd", "Onderdeel toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        /// <summary>
        /// Voegt een reparatieregistratie toe AHV meegegeven data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVoegTicketRegToeR_Click(object sender, EventArgs e)
        {
            ReparatieRegistratie rr = new ReparatieRegistratie();
            rr.AantalUren = Convert.ToInt32(mtbGewerkteUrenR.Text);
            rr.Datum = DateTime.Now;
            rr.Mednr = Convert.ToInt64(tbMedewerkerNummerR.Text);
            rr.Vermelding = tbNieuweMeldingRR.Text;
            
            foreach (object o in cbVervangenOnderdelenRR.Items)
            {
                rr.VervangenOnderdelen.Add((VervangenOnderdeel)o);
            }
            if (reparatieSysteem.VoegReparatieRegistratieToe(Convert.ToInt32(tbTicketnummerR.Text), rr))
            {
                MessageBox.Show("Succesvol toegevoegd", "Reparatie registratie toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Fout bij toevoegen", "Reparatie registratie toevoegen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// <summary>
        /// verwijdert medewerker ahv medewerkernummer (eerst zoeken of meegeven)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btVerwijderMedewerkerAdmin_Click(object sender, EventArgs e)
        {
            int mednr = -1;
            try
            {
                mednr = Convert.ToInt32(tbMedewerkernummerAdmin.Text);
            }
            catch (FormatException) { return; }
            if (reparatieSysteem.VerwijderMedewerker(mednr))
            {
                tbMedewerkernummerAdmin.Text = "";
                tbNaamAdmin.Text = "";
                tbAfdelingMedewerkerAdmin.Text = "";
                cbFunctieMedewerkerAdmin.SelectedIndex = -1;
                tbPaswoordAdmin.Text = "";
                tbTelefoonnummerAdmin.Text = "";
                MessageBox.Show("Succesvol verwijderd", "Medewerker verwijderen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Niet gelukt", "Medewerker verwijderen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// <summary>
        /// Vult de listbox medewerkers met alle medewerkers van PARAFIKSIT wanneer tabadministrator geselecteerd wordt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbKlant_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage != TabAdministrator) return;
            List<Medewerker> medewerkers = reparatieSysteem.GetMedewerkers();
            lbMedewerkers.Items.Clear();
            foreach (Medewerker m in medewerkers)
            {
                lbMedewerkers.Items.Add(m);
            }
            List<Ticket> tickets = reparatieSysteem.GetTickets();
            if (tickets == null) return;
            foreach (Ticket t in tickets)
            {
                if (t.Status == TicketStatus.INREPARATIE || t.Status == TicketStatus.GEREPAREERD)
                {
                    Factuur f = new Factuur(t);
                    List<ReparatieRegistratie> registraties = reparatieSysteem.GetReparatieRegistratiesByTicket(Convert.ToInt32(t.Ticketnr));
                    double prijs = 0;
                    foreach (ReparatieRegistratie rr in registraties)
                    {
                        prijs = rr.AantalUren * 100;
                        foreach (VervangenOnderdeel vo in rr.VervangenOnderdelen)
                        {
                            prijs += vo.Kosten;
                        }
                    }
                    f.Prijs = prijs;
                    lbFacturen.Items.Add(f);
                }
            }

        }

        /// <summary>
        /// Vult de informatie van de geselecteerde medewerker in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMedewerkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Medewerker m = (Medewerker)lbMedewerkers.SelectedItem;
            tbMedewerkernummerAdmin.Text = m.Mednr.ToString();
            tbNaamAdmin.Text = m.Naam;
            cbFunctieMedewerkerAdmin.SelectedIndex = (int)m.Functie;
            tbAfdelingMedewerkerAdmin.Text = m.Filiaal;
            tbPaswoordAdmin.Text = m.Password;
            tbTelefoonnummerAdmin.Text = m.Telefoon;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            //reparatieSysteem.sendMessage("Test123");
        }    
               
    }
}
