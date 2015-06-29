using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;

namespace ReparatieSysteem
{
    public class OracleDatabase : IDatabase
    {
        private OracleConnection connection;

        public OracleDatabase()
        {
            string host = "127.0.0.1";
            string SID = "XE";
            string password = "appel";
            string user = "PTS22";
            connection = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + host + ")(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + SID + ")));User Id=" + user + ";Password=" + password + ";");
        }

        public List<Klant> GetKlanten()
        {
            List<Klant> result = new List<Klant>();
            //try
            //{
                connection.Open();
                string query = "SELECT * FROM klant";
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Klant k;
                while (reader.Read())
                {
                    k = new Klant(
                        Convert.ToInt32(reader["klantnr"]),
                        (string)reader["voornaam"],
                        (string)reader["tussenvoegsel"],
                        (string)reader["achternaam"],
                        (string)reader["woonplaats"],
                        (string)reader["postcode"],
                        (string)reader["straat"],
                        Convert.ToInt32(reader["huisnummer"]),
                        (string)reader["email"],
                        (string)reader["land"],
                        (string)reader["telefoon"]
                        );
                    result.Add(k);
                }
                connection.Close();
                return result;
            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.ToString());
            //    return null;
            //}
            //finally
            //{
                //connection.Close();
            //}
        }

        public Klant GetKlantByKlantnr(int klantnr)
        {
            //try
            //{
                connection.Open();
                string query = "SELECT * FROM klant WHERE klantnr = " + klantnr.ToString();

                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                return new Klant(
                        Convert.ToInt32(reader["klantnr"]),
                        (string)reader["voornaam"],
                        (string)reader["tussenvoegsel"],
                        (string)reader["achternaam"],
                        (string)reader["woonplaats"],
                        (string)reader["postcode"],
                        (string)reader["straat"],
                        Convert.ToInt32(reader["huisnummer"]),
                        (string)reader["email"],
                        (string)reader["land"],
                        (string)reader["telefoon"]
                        );
                connection.Close();
            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.ToString());
            //    return null;
            //}
            //finally
            //{
                //connection.Close();
            //}
        }

        public List<Klant> GetKlanten(KlantFilter filter)
        {
            List<Klant> result = new List<Klant>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM klant WHERE ";
                if (filter.Voornaam != "")
                {
                    query += "voornaam LIKE '%" + filter.Voornaam + "%' AND ";
                }
                if (filter.Tussenvoegsel != "")
                {
                    query += "tussenvoegsel LIKE '%" + filter.Tussenvoegsel + "%' AND ";
                }
                if (filter.Achternaam != "")
                {
                    query += "achternaam LIKE '%" + filter.Achternaam + "%' AND ";
                }
                if (filter.Woonplaats != "")
                {
                    query += "woonplaats LIKE '%" + filter.Woonplaats + "%' AND ";
                }
                if (filter.Postcode != "")
                {
                    query += "postcode LIKE '%" + filter.Postcode + "%' AND ";
                }
                if (filter.Straatnaam != "")
                {
                    query += "straat LIKE '%" + filter.Straatnaam + "%' AND ";
                }
                if (filter.Huisnummer != -1)
                {
                    query += "huisnummer = " + filter.Huisnummer + " AND ";
                }
                if (filter.Email != "")
                {
                    query += "email LIKE '%" + filter.Email + "%' AND ";
                }
                if (filter.Land != "")
                {
                    query += "land LIKE '%" + filter.Land + "%' AND ";
                }
                if (filter.Telefoon != -1)
                {
                    query += "telefoon = " + filter.Telefoon + " AND ";
                }
                query = query.Substring(0, query.Length - 5);
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Klant k;
                while (reader.Read())
                {
                    k = new Klant(
                        Convert.ToInt32(reader["klantnr"]),
                        (string)reader["voornaam"],
                        (string)reader["tussenvoegsel"],
                        (string)reader["achternaam"],
                        (string)reader["woonplaats"],
                        (string)reader["postcode"],
                        (string)reader["straat"],
                        Convert.ToInt32(reader["huisnummer"]),
                        (string)reader["email"],
                        (string)reader["land"],
                        (string)reader["telefoon"]
                        );
                    result.Add(k);
                }
                return result;
            }
            catch (OracleException e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> result = new List<Ticket>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM ticket";
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Ticket t;
                while (reader.Read())
                {
                    t = new Ticket(
                        Convert.ToInt32(reader["ticketnr"]),
                        Convert.ToInt32(reader["klantnr"]),
                        Convert.ToInt32(reader["verw_oplostijd"]),
                        Convert.ToDouble(reader["verw_kosten"]),
                        (TicketStatus)Convert.ToInt32(reader["status"]),
                        (string)reader["afdeling"],
                        (string)reader["probleem"],
                        (string)reader["categorie"]
                        );
                    result.Add(t);
                }
                return result;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Ticket GetTicketByTicketnr(int ticketnr)
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM ticket WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                return new Ticket(
                        Convert.ToInt32(reader["ticketnr"]),
                        Convert.ToInt32(reader["klantnr"]),
                        Convert.ToInt32(reader["verw_oplostijd"]),
                        Convert.ToDouble(reader["verw_kosten"]),
                        (TicketStatus)Convert.ToInt32(reader["status"]),
                        (string)reader["afdeling"],
                        (string)reader["probleem"],
                        (string)reader["categorie"]
                        );;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Ticket> GetTickets(TicketFilter filter)
        {
            return null;
        }

        public List<Ticket> GetTicketsByKlant(int klantnr)
        {
            List<Ticket> result = new List<Ticket>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM ticket WHERE klantnr = " + klantnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Ticket t;
                while (reader.Read())
                {
                    t = new Ticket(
                        Convert.ToInt32(reader["ticketnr"]),
                        Convert.ToInt32(reader["klantnr"]),
                        Convert.ToInt32(reader["verw_oplostijd"]),
                        Convert.ToDouble(reader["verw_kosten"]),
                        (TicketStatus)Convert.ToInt32(reader["status"]),
                        (string)reader["afdeling"],
                        (string)reader["probleem"],
                        (string)reader["categorie"]
                        );
                    result.Add(t);
                }
                return result;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Registratie> GetRegistratiesByTicket(int ticketnr)
        {
            List<Registratie> result = new List<Registratie>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM ticketregistratie WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Registratie r = new Registratie();
                while (reader.Read())
                {
                    r.Mednr = Convert.ToInt32(reader["mednr"]);
                    r.Datum = Convert.ToDateTime(reader["datum"]);
                    r.Vermelding = (string)reader["melding"];
                    result.Add(r);
                }
                return result;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<ReparatieRegistratie> GetReparatieRegistratiesByTicket(int ticketnr)
        {
            List<ReparatieRegistratie> result = new List<ReparatieRegistratie>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM reparatieregistratie WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                ReparatieRegistratie r = new ReparatieRegistratie();
                while (reader.Read())
                {
                    r.Mednr = Convert.ToInt32(reader["mednr"]);
                    r.Datum = Convert.ToDateTime(reader["datum"]);
                    r.Vermelding = (string)reader["melding"];
                    r.AantalUren = Convert.ToInt32(reader["uren"]);
                    r.VervangenOnderdelen.Clear();

                    string vervangenOnderdelenQuery = "SELECT * FROM verv_onderd WHERE rrnr = '" + (string)reader["rrnr"] + "'";
                    command = new OracleCommand(vervangenOnderdelenQuery, connection);
                    OracleDataReader vervangenOnderdelenReader = command.ExecuteReader();

                    VervangenOnderdeel vo;
                    while (vervangenOnderdelenReader.Read())
                    {
                        vo = new VervangenOnderdeel(
                            (string)vervangenOnderdelenReader["productnaam"],
                            Convert.ToDouble(vervangenOnderdelenReader["kosten"])
                            );
                        r.VervangenOnderdelen.Add(vo);
                    }

                    result.Add(r);
                }
                return result;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Product> GetProductsByKlant(int klantnr)
        {
            List<Product> result = new List<Product>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM product";
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Product p;
                while (reader.Read())
                {
                    p = new Product(
                        Convert.ToInt32(reader["serienummer"]),
                        Convert.ToInt32(reader["klantnr"]),
                        Convert.ToInt32(reader["garantieduur"]),
                        (string)reader["omschrijving"],
                        Convert.ToDateTime(reader["aankoop_datum"])
                        );
                    result.Add(p);
                }
                return result;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Afdeling> GetAfdelingen()
        {
            List<Afdeling> result = new List<Afdeling>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM afdeling";
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Afdeling a;
                while (reader.Read())
                {
                    a = new Afdeling(
                        (string)reader["afd_afk"],
                        (string)reader["afd_naam"],
                        (string)reader["afd_omschrijving"]
                        );
                    result.Add(a);
                }
                return result;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<TicketCategorie> GetCategorieen()
        {
            List<TicketCategorie> result = new List<TicketCategorie>();
            try
            {
                connection.Open();
                string query = "SELECT naam, omschrijving FROM categorie";
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                TicketCategorie c;
                while (reader.Read())
                {
                    c = new TicketCategorie(
                        (string)reader["naam"],
                        (string)reader["omschrijving"]
                        );
                    result.Add(c);
                }
                return result;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Niet mogelijk om categorieen binnen te halen: " + e.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Klant SaveKlant(string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer, string email, string telefoon, string land)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO klant (klantnr, voornaam, achternaam, tussenvoegsel,  woonplaats, straat, huisnummer, postcode, email, telefoon, land) " +
                    "VALUES (klantseq.nextval, '" + voornaam + "', '" + achternaam + "', '" + tussenvoegsel + "', '" + woonplaats + "', '" + straatnaam + "', " + huisnummer.ToString() + ", '" + postcode + "', '" + email + "', '" + telefoon + "', '" + land + "')";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT klantnr FROM klant WHERE voornaam = '" + voornaam + "'";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                return new Klant(
                    Convert.ToInt32(reader["klantnr"]),
                    voornaam,
                    tussenvoegsel,
                    achternaam,
                    woonplaats,
                    postcode,
                    straatnaam,
                    huisnummer,
                    email,
                    land,
                    telefoon
                    );
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Ticket SaveTicket(int klantnr, int productnr, double verwachteKosten, int verwachteReparatieTijd, TicketStatus status, Registratie registratie, string probleem, string afdelingsAfkorting, string categorie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO ticket (ticketnr, status, probleem, afdeling, verw_oplostijd, verw_kosten, klantnr,serienummer) VALUES " +
                    "(ticketseq.nextval, " + ((int)status).ToString() + ", '" + probleem + "', '" + afdelingsAfkorting + "', " + verwachteReparatieTijd.ToString() + ", " + verwachteKosten.ToString() + ", " + klantnr.ToString() + ", " + productnr + ")";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(ticketnr) FROM ticket";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();

                return new Ticket(
                    Convert.ToInt32(reader["ticketnr"]),
                    klantnr,
                    verwachteReparatieTijd,
                    verwachteKosten,
                    status,
                    afdelingsAfkorting,
                    probleem,
                    categorie
                    );
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Ticket SaveTicket(int klantnr, double verwachteKosten, int verwachteReparatieTijd, TicketStatus status, Registratie registratie, string probleem, string afdelingsAfkorting, string categorie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO ticket (ticketnr, status, probleem, afdeling, verw_oplostijd, verw_kosten, klantnr) VALUES " +
                    "(ticketseq.nextval, " + ((int)status).ToString() + ", '" + probleem + "', '" + afdelingsAfkorting + "', " + verwachteReparatieTijd.ToString() + ", " + verwachteKosten.ToString() + ", " + klantnr.ToString() + ")";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(ticketnr) FROM ticket";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();

                return new Ticket(
                    Convert.ToInt32(reader["ticketnr"]),
                    klantnr,
                    verwachteReparatieTijd,
                    verwachteKosten,
                    status,
                    afdelingsAfkorting,
                    probleem,
                    categorie
                    );
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool WijzigTicketstatus(int ticketnr, TicketStatus status)
        {
            try
            {
                connection.Open();
                string query = "UPDATE ticket SET status = " + ((int)status).ToString() + " WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool SaveRegistratie(int ticketnr, Registratie registratie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO ticketregistratie (mednr, ticketnr, datum, melding) VALUES (" +
                    registratie.Mednr + ", " + ticketnr + ", to_timestamp('" + registratie.Datum.Year.ToString() + "-" + registratie.Datum.Month + "-" + registratie.Datum.Day + " " + registratie.Datum.Hour + ":" + registratie.Datum.Minute + ":" + registratie.Datum.Second + "',' 'YYYY-MM-DD HH24:MI:SS'), '" + registratie.Vermelding + "')";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool SaveReparatieRegistratie(int ticketnr, ReparatieRegistratie registratie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO reparatieregistratie (mednr, ticketnr, datum, melding, uren) VALUES (" +
                    registratie.Mednr + ", " + ticketnr + ", to_timestamp('" + registratie.Datum.Year.ToString() + "-" + registratie.Datum.Month + "-" + registratie.Datum.Day + " " + registratie.Datum.Hour + ":" + registratie.Datum.Minute + ":" + registratie.Datum.Second + "',' 'YYYY-MM-DD HH24:MI:SS'), '" + registratie.Vermelding + "', " + registratie.AantalUren.ToString() + ")";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(rrnr) FROM reparatieregistratie";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                string rrnr = (string)reader["rrnr"];

                foreach (VervangenOnderdeel vo in registratie.VervangenOnderdelen)
                {
                    string vervangenOnderdelenQuery = "INSERT INTO verv_onderd (productnaam, kosten, rrnr) VALUES ('" + vo.Productnaam + "', " + vo.Kosten.ToString() + ", " + rrnr + ")";
                    command = new OracleCommand(vervangenOnderdelenQuery, connection);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public Medewerker SaveMedewerker(string naam, string password, string telefoon, string filliaal, MedewerkerFunctie functie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO medewerker (mednr, naam, functie, afd, password, telnr) VALUES (medewerkerseq.nextval, " +
                    "'" + naam + "', " + ((int)functie).ToString() + ", '" + filliaal + "', '" + password + "', '" + telefoon + "')";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(mednr) FROM medewerker";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();

                return new Medewerker(
                    Convert.ToInt32(reader["mednr"]),
                    naam,
                    password,
                    telefoon,
                    filliaal,
                    functie
                    );
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Product SaveProduct(int klantnr, int garantieTijd, string omschrijving, DateTime verkoopDatum)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO product (omschrijving, aankoop_datum, garantieduur, klantnr) VALUES "
                    + "('" + omschrijving +
                    "', to_timestamp('" + verkoopDatum.Year.ToString() + "-" + verkoopDatum.Month + "-" + verkoopDatum.Day + " " + verkoopDatum.Hour + ":" + verkoopDatum.Minute + ":" + verkoopDatum.Second + "',' 'YYYY-MM-DD HH24:MI:SS'), " +
                    garantieTijd.ToString() +
                    ", " + klantnr.ToString() + ")";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(productnr) FROM product";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();

                return new Product(
                    Convert.ToInt32(reader["productnr"]),
                    klantnr, 
                    garantieTijd, 
                    omschrijving, 
                    verkoopDatum
                    );
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Medewerker LogIn(int gebruikersnaam, string pw)
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM medewerker WHERE mednr = " + gebruikersnaam.ToString() + " AND password = " + pw;
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Medewerker(
                        Convert.ToInt32(reader["mednr"]),
                        (string)reader["naam"],
                        pw,
                        (string)reader["telnr"],
                        (string)reader["afd"],
                        (MedewerkerFunctie)Convert.ToInt32(reader["functie"])
                        );
                }
                //gebruikersnaam + PW kloppen niet
                return null;
                
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RemoveKlant(int klantnr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM klant WHERE klantnr = " + klantnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RemoveTicket(int ticketnr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM ticket WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RemoveMedewerker(int mednr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM medewerker WHERE mednr = " + mednr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RemoveProduct(int productnr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM product WHERE serienr = " + productnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
