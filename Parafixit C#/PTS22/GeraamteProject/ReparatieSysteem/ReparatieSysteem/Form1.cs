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
            HELPDESK,
            WINKEL,
            REPARATIE,
            ADMIN
        }

        private Toestand toestand;
        private ReparatieSysteem reparatieSysteem;
        private List<Klant> klantenLijst;


        public Form1()
        {            
            InitializeComponent();
            gbLogIn.Location = new Point(340, 200);
            gbAlles.Location = new Point(5, 50);
            reparatieSysteem = new ReparatieSysteem();
            toestand = Toestand.INLOGGEN;
            Showtoestand();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (tbGebruiksnaam.Text == "admin")
            {
                toestand = Toestand.ADMIN;
            }
            if (tbGebruiksnaam.Text == "helpdesk")
            {
                toestand = Toestand.HELPDESK;
            }                       
            Showtoestand();
        }       

        // Selecteer in zoekresultaten een item (klant) en de klantgegevens worden verder aangevuld.
        // Verder worden de listboxen Tickets en Producten behorende bij de geselecteerde klant aangevuld met
        // tickets en producten bij de desbetreffende klant geregistreert.
        private void liZoekResultaten_SelectedIndexChanged(object sender, EventArgs e)
        {
            Klant k = klantenLijst[liZoekresultatenKlant.SelectedIndex];
            try
            {
                tbVoornaamKlant.Text = k.Voornaam;
                tbTussenvoegselKlant.Text = k.Tussenvoegsel;
                tbAchternaamKlant.Text = k.Achternaam;
                tbStraatKlant.Text = k.Straatnaam;
                tbHuisnummerKlant.Text = Convert.ToString(k.Huisnummer);
                tbPostcodeKlant.Text = k.Postcode;
                tbPlaatsKlant.Text = k.Woonplaats;
                tbLandKlant.Text = k.Land;
                tbTelefoonnummerKlant.Text = k.Telefoon;
                tbEmailKlant.Text = k.Email;
            }
            catch { MessageBox.Show("Niet alle klantgegevens zijn correct bekend", "Zoek Klant", MessageBoxButtons.OK); return; }
            
            liTickets.Items.Clear();
            Producten.Items.Clear();

            try
            {
                List<Product> productenLijst = reparatieSysteem.GetProductenByKlant(Convert.ToInt32(k.Nr));
                foreach (Product p in productenLijst)
                {
                    Producten.Items.Add(p);
                }
                List<Ticket> ticketLijst = reparatieSysteem.GetTicketsByKlantnr(Convert.ToInt32(k.Nr));
                foreach (Ticket t in ticketLijst)
                {
                    liTickets.Items.Add(t);
                }
            }
            catch { MessageBox.Show("Niet alle Tickets en Producten \nkunnen geladen worden bij het Klantnummer.", "Zoek Klant", MessageBoxButtons.OK); }
            
            
        }

        public void Showtoestand()
        {
            gbAlles.Visible = false;
            gbLogIn.Visible = false;
            lbKlantnummerSnel.Visible = true;
            lbTicketnummerSnel.Visible = true;
            tbKlantnummerSnel.Visible = true;
            tbTicketnummerSnel.Visible = true;
            btZoekenSnel.Visible = true;
            btUitloggen.Visible = true;
            //tbKlant.TabPages.Remove(TabAdministrator);
            //tbKlant.TabPages.Remove(TabKlant);
            //tbKlant.TabPages.Remove(TabReparatie);
            //tbKlant.TabPages.Remove(TabTicketEnProduct);

            if (toestand == Toestand.INLOGGEN)
            {
                btUitloggen.Visible = false;
                lbKlantnummerSnel.Visible = false;
                lbTicketnummerSnel.Visible = false;
                tbKlantnummerSnel.Visible = false;
                tbTicketnummerSnel.Visible = false;
                btZoekenSnel.Visible = false;
                gbLogIn.Visible = true;
            }
            if (toestand == Toestand.ADMIN)
            {
                gbAlles.Visible = true;
            }
            if (toestand == Toestand.HELPDESK)
            {
                gbAlles.Visible = true;                
                tbKlant.TabPages.Remove(TabAdministrator);
            }
        }

        //private void Button_MouseEnter(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    button.BackColor = Color.Goldenrod;
        //}

        //private void Button_MouseLeave(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    button.BackColor = Color.Transparent;
        //}

        private void btTicketToevoegenKlant_Click(object sender, EventArgs e)
        {
            tbKlant.SelectedTab = TabProductRegistratie;
            
        }

        private void btProductToevoegenKlant_Click(object sender, EventArgs e)
        {
            tbKlant.SelectedTab = TabProductRegistratie;
        }

        private void btUitloggen_Click(object sender, EventArgs e)
        {
            toestand = Toestand.INLOGGEN;
            Application.Restart();
            Showtoestand();
        }

        private void btZoekenKlant_Click(object sender, EventArgs e)
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

            liZoekresultatenKlant.Items.Clear();
            klantenLijst = reparatieSysteem.ZoekKlanten(klantFilter);
            if (klantenLijst != null)
            {
                
                if (klantenLijst.Count == 0)
                {
                    MessageBox.Show("Geen klanten voldoen \naan de zoekcriteria.", "Zoek Klant", MessageBoxButtons.OK);
                }
                else
                {
                    
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

       
     
        
    }
}
