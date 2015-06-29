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

        /// <summary>
        /// Haalt een lijst van klanten uit de database.
        /// </summary>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout met de database is.</returns>
        public List<Klant> GetKlanten()
        {
            List<Klant> result = new List<Klant>();
            try
            {
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
                return result;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt de klant uit de database met het meegegeven klantnr.
        /// </summary>
        /// <param name="klantnr">Het klantnummer</param>
        /// <returns>Returnt de klant als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Klant GetKlantByKlantnr(int klantnr)
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM klant WHERE klantnr = " + klantnr.ToString();

                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                Klant k = new Klant(
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
                return k;
                
            }
            catch (OracleException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt een lijst met klanten uit de database die gefiltert is met de meegegeven filter.
        /// </summary>
        /// <param name="filter">De filter</param>
        /// <returns>Returnt de klant als het gelukt is. Returnt false als er een fout met de database is opgetreden.</returns>
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
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt de klant die hoort bij het meegegeven ticketnr uit de database.
        /// </summary>
        /// <param name="ticketnr">Het ticketnummer</param>
        /// <returns>Returnt de klant als het gelukt is. Returnt false als er een fout met de database is.</returns>
        public Klant GetKlantByTicketnr(int ticketnr)
        {
            Klant k;
            string query = "SELECT * FROM klant WHERE klantnr IN (SELECT klantnr FROM ticket WHERE ticketnr = " + ticketnr.ToString() + ")";
            try
            {
                OracleCommand command = new OracleCommand(query, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                if (reader.Read())
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
                    return k;
                }
                else
                {
                    return null;
                }
            }
            catch (OracleException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt de lijst met tickets uit de database.
        /// </summary>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public List<Ticket> GetTickets()
        {
            List<Ticket> result = new List<Ticket>();
            //try
            //{
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
                        (string)reader["locatie"],
                        (string)reader["opmerking"],
                        (string)reader["categorie"]
                        );
                    result.Add(t);
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
            //    connection.Close();
            //}
        }

        /// <summary>
        /// Haalt de ticket uit de database met het meegegeven ticketnummer.
        /// </summary>
        /// <param name="ticketnr">Het ticketnummer</param>
        /// <returns>Returnt de ticket als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Ticket GetTicketByTicketnr(int ticketnr)
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM ticket WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                        int ticketnra = Convert.ToInt32(reader["ticketnr"]);
                        int klantnr = Convert.ToInt32(reader["klantnr"]);
                        int verw_oplostijd = Convert.ToInt32(reader["verw_oplostijd"]);
                        double kosten = Convert.ToDouble(reader["verw_kosten"]);
                        TicketStatus status = (TicketStatus)Convert.ToInt32(reader["status"]);
                        string loc = (string)reader["locatie"];
                        string opm = (string)reader["opmerking"];
                        string categorie = (string)reader["categorie"];
                Ticket t = new Ticket(ticketnra, klantnr, verw_oplostijd, kosten, status, loc, opm, categorie);
                return t;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Niet geimplementeerd
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Ticket> GetTickets(TicketFilter filter)
        {
            return null;
        }

        /// <summary>
        /// Haalt een lijst met tickets uit de database van de meegegeven klant
        /// </summary>
        /// <param name="klantnr">Het klantnummer.</param>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
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
                    int ticketnr = Convert.ToInt32(reader["ticketnr"]);
                        int klantnra = Convert.ToInt32(reader["klantnr"]);
                        int verw_oplostijd = Convert.ToInt32(reader["verw_oplostijd"]);
                        double verw_kosten = Convert.ToDouble(reader["verw_kosten"]);
                        TicketStatus status = (TicketStatus)Convert.ToInt32(reader["status"]);
                        string locatie = (string)reader["locatie"];
                        string opmerking = (string)reader["opmerking"];
                        string categorie = (string)reader["categorie"];
                    t = new Ticket(
                        ticketnr, klantnra, verw_oplostijd, verw_kosten, status, locatie, opmerking, categorie
                        );
                    result.Add(t);
                }
                return result;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt een lijst met registraties uit de database van de meegegeven ticket.
        /// </summary>
        /// <param name="ticketnr">Het ticketnummer.</param>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout met de database is.</returns>
        public List<Registratie> GetRegistratiesByTicket(int ticketnr)
        {
            List<Registratie> result = new List<Registratie>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM ticketregistratie WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Registratie r;
                while (reader.Read())
                {
                    r = new Registratie();
                    r.Mednr = Convert.ToInt32(reader["mednr"]);
                    DateTime dateTime = Convert.ToDateTime(reader["datumtijd"]);
                    int day = Convert.ToInt32(dateTime.Day);
                    int year = Convert.ToInt32(dateTime.Year);
                    int month = dateTime.Month;
                    //switch (dateTime.Month)
                    //{
                    //    case "JAN":
                    //        month = 1;
                    //        break;
                    //    case "FEB":
                    //        month = 2;
                    //        break;
                    //    case "MAR":
                    //        month = 3;
                    //        break;
                    //    case "APR":
                    //        month = 4;
                    //        break;
                    //    case "MAY":
                    //        month = 5;
                    //        break;
                    //    case "JUN":
                    //        month = 6;
                    //        break;
                    //    case "JUL":
                    //        month = 7;
                    //        break;
                    //    case "AUG":
                    //        month = 8;
                    //        break;
                    //    case "SEP":
                    //        month = 9;
                    //        break;
                    //    case "OCT":
                    //        month = 10;
                    //        break;
                    //    case "NOV":
                    //        month = 11;
                    //        break;
                    //    case "DEC":
                    //        month = 12;
                    //        break;
                    //}
                    r.Datum = new DateTime(year, month, day);
                    //r.Datum = Convert.ToDateTime(reader["datumtijd"]);
                    r.Vermelding = (string)reader["melding"];
                    result.Add(r);
                }
                return result;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt een lijst van reparatieregistraties uit de database van de meegegeven ticket.
        /// </summary>
        /// <param name="ticketnr">Het ticketnummer</param>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
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
                    r.Datum = Convert.ToDateTime(reader["datumtijd"]);
                    r.Vermelding = (string)reader["melding"];
                    r.AantalUren = Convert.ToInt32(reader["uren"]);
                    r.VervangenOnderdelen.Clear();

                    string vervangenOnderdelenQuery = "SELECT * FROM vervonderd WHERE rrnr = '" + Convert.ToDecimal(reader["rrnr"]).ToString() + "'";
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
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt een lijst van producten uit de database met de meegegeven klant.
        /// </summary>
        /// <param name="klantnr">Het klantnummer.</param>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public List<Product> GetProductsByKlant(int klantnr)
        {
            List<Product> result = new List<Product>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM product WHERE klantnr = " + klantnr.ToString(); ;
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Product p;
                while (reader.Read())
                {
                    string dateTime = (string)(reader["aankoop_date"]); 
                    int day = Convert.ToInt32(dateTime.Substring(0, 2));
                    int year = Convert.ToInt32(dateTime.Substring(7, 2));
                    int month = 0;
                    switch (dateTime.Substring(3, 3))
                    {
                        case "JAN":
                            month = 1;
                            break;
                        case "FEB":
                            month = 2;
                            break;
                        case "MAR":
                            month = 3;
                            break;
                        case "APR":
                            month = 4;
                            break;
                        case "MAY":
                            month = 5;
                            break;
                        case "JUN":
                            month = 6;
                            break;
                        case "JUL":
                            month = 7;
                            break;
                        case "AUG":
                            month = 8;
                            break;
                        case "SEP":
                            month = 9;
                            break;
                        case "OCT":
                            month = 10;
                            break;
                        case "NOV":
                            month = 11;
                            break;
                        case "DEC":
                            month = 12;
                            break;
                    }
                    DateTime date = new DateTime(year, month, day);
                    p = new Product(
                        Convert.ToInt32(reader["serienummer"]),
                        Convert.ToInt32(reader["klantnr"]),
                        (string)(reader["garantie_duur"]),
                        (string)reader["omschrijving"],
                        date
                        );
                    result.Add(p);
                }
                return result;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt het eventuele product uit de database dat bij het meegegeven ticket hoort.
        /// </summary>
        /// <param name="ticketnr">Het ticketnummer.</param>
        /// <returns>Returnt het product als het gelukt is. Returnt null als er een fout met de database is opgetreden.</returns>
        public Product GetProductByTicket(int ticketnr)
        {
            try
            {
                connection.Open();
                string query = String.Format("SELECT * FROM product WHERE serienummer IN (SELECT serienr FROM ticket WHERE ticketnr = {0})", ticketnr);
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                string dateTime = (string)(reader["aankoop_date"]);
                int day = Convert.ToInt32(dateTime.Substring(0, 2));
                int year = Convert.ToInt32(dateTime.Substring(7, 2));
                int month = 0;
                switch (dateTime.Substring(3, 3))
                {
                    case "JAN":
                        month = 1;
                        break;
                    case "FEB":
                        month = 2;
                        break;
                    case "MAR":
                        month = 3;
                        break;
                    case "APR":
                        month = 4;
                        break;
                    case "MAY":
                        month = 5;
                        break;
                    case "JUN":
                        month = 6;
                        break;
                    case "JUL":
                        month = 7;
                        break;
                    case "AUG":
                        month = 8;
                        break;
                    case "SEP":
                        month = 9;
                        break;
                    case "OCT":
                        month = 10;
                        break;
                    case "NOV":
                        month = 11;
                        break;
                    case "DEC":
                        month = 12;
                        break;
                }
                DateTime date = new DateTime(year, month, day);
                Product p = new Product(
                        Convert.ToInt32(reader["serienummer"]),
                        Convert.ToInt32(reader["klantnr"]),
                        (string)reader["garantie_duur"],
                        (string)reader["omschrijving"],
                        date
                        );
                return p;
            }
            catch (OracleException)
            {
                return null;
            }
            finally 
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Haalt een lijst met de ticketcategorieen uit de database.
        /// </summary>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
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
                    string omschrijving = "";
                    try
                    {
                        omschrijving = (string)reader["omschrijving"];
                    }
                    catch
                    {

                    }
                    c = new TicketCategorie(
                        (string)reader["naam"],
                        omschrijving
                        );
                    result.Add(c);
                }
                return result;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Maakt een nieuwe klant aan in de database met de meegegven informatie.
        /// </summary>
        /// <param name="voornaam"></param>
        /// <param name="tussenvoegsel"></param>
        /// <param name="achternaam"></param>
        /// <param name="woonplaats"></param>
        /// <param name="postcode"></param>
        /// <param name="straatnaam"></param>
        /// <param name="huisnummer"></param>
        /// <param name="email"></param>
        /// <param name="telefoon"></param>
        /// <param name="land"></param>
        /// <returns>Returnt de klant als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Klant SaveKlant(string voornaam, string tussenvoegsel, string achternaam, string woonplaats, string postcode, string straatnaam, int huisnummer, string email, string telefoon, string land)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO klant (klantnr, voornaam, achternaam, tussenvoegsel,  woonplaats, straat, huisnummer, postcode, email, telefoon, land, verwijderd) " +
                    "VALUES (klantseq.nextval, '" + voornaam + " " + "', '" + achternaam + "', '" + tussenvoegsel + " " + "', '" + woonplaats + "', '" + straatnaam + "', " + huisnummer.ToString() + ", '" + postcode + "', '" + email + "', '" + telefoon + "', '" + land + "', 0)";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(klantnr) AS klantnr FROM klant";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                Klant k = new Klant(
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
                return k;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Maakt een nieuwe ticket aan in de database met de meegegeven informatie.
        /// </summary>
        /// <param name="klantnr"></param>
        /// <param name="productnr"></param>
        /// <param name="verwachteKosten"></param>
        /// <param name="verwachteReparatieTijd"></param>
        /// <param name="status"></param>
        /// <param name="registratie"></param>
        /// <param name="probleem"></param>
        /// <param name="afdelingsAfkorting"></param>
        /// <param name="categorie"></param>
        /// <returns>Returnt de ticket als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Ticket SaveTicket(int klantnr, int productnr, double verwachteKosten, int verwachteReparatieTijd, TicketStatus status, Registratie registratie, string probleem, string afdelingsAfkorting, string categorie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO ticket (ticketnr, status, opmerking, locatie, verw_oplostijd, verw_kosten, klantnr, serienr, categorie) VALUES " +
                    "(ticketseq.nextval, " + ((int)status).ToString() + ", '" + probleem + "', '" + afdelingsAfkorting + "', " + verwachteReparatieTijd.ToString() + ", " + verwachteKosten.ToString() + ", " + klantnr.ToString() + ", " + productnr + ", '" + categorie + "')";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(ticketnr) AS ticketnr FROM ticket";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();

                Ticket t = new Ticket(
                    Convert.ToInt32(reader["ticketnr"]),
                    klantnr,
                    verwachteReparatieTijd,
                    verwachteKosten,
                    status,
                    afdelingsAfkorting,
                    probleem,
                    categorie
                    );
                return t;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Maakt een nieuwe ticket aan in de database met de meegegeven informatie.
        /// </summary>
        /// <param name="klantnr"></param>
        /// <param name="verwachteKosten"></param>
        /// <param name="verwachteReparatieTijd"></param>
        /// <param name="status"></param>
        /// <param name="registratie"></param>
        /// <param name="probleem"></param>
        /// <param name="afdelingsAfkorting"></param>
        /// <param name="categorie"></param>
        /// <returns>Returnt de ticket als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Ticket SaveTicket(int klantnr, double verwachteKosten, int verwachteReparatieTijd, TicketStatus status, Registratie registratie, string probleem, string afdelingsAfkorting, string categorie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO ticket (ticketnr, status, opmerking, locatie, verw_oplostijd, verw_kosten, klantnr, categorie) VALUES " +
                    "(ticketseq.nextval, " + ((int)status).ToString() + ", '" + probleem + "', '" + afdelingsAfkorting + "', " + verwachteReparatieTijd.ToString() + ", " + verwachteKosten.ToString() + ", " + klantnr.ToString() + ", '" + categorie + "')";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(ticketnr) AS ticketnr FROM ticket";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                Ticket t = new Ticket(
                    Convert.ToInt32(reader["ticketnr"]),
                    klantnr,
                    verwachteReparatieTijd,
                    verwachteKosten,
                    status,
                    afdelingsAfkorting,
                    probleem,
                    categorie
                    );
                 return t;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Wijzigt de status van de meegegeven ticket in de meegegeven status.
        /// </summary>
        /// <param name="ticketnr"></param>
        /// <param name="status"></param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
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
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Maakt een nieuwe registratie aan horend bij het meegegeven ticket.
        /// </summary>
        /// <param name="ticketnr"></param>
        /// <param name="registratie"></param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool SaveRegistratie(int ticketnr, Registratie registratie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO ticketregistratie (trnr, mednr, ticketnr, datumtijd, melding) VALUES (registratieseq.nextval, " +
                    registratie.Mednr + ", " + ticketnr + ", to_timestamp('" + registratie.Datum.Year.ToString() + "-" + registratie.Datum.Month + "-" + registratie.Datum.Day + " " + registratie.Datum.Hour + ":" + registratie.Datum.Minute + ":" + registratie.Datum.Second + "','YYYY-MM-DD HH24:MI:SS'), '" + registratie.Vermelding + "')";
                OracleCommand command = new OracleCommand(query, connection);    
                command.ExecuteNonQuery();
                return true;
            }
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Maakt een nieuwe reparatieregistratie aan horend bij het meegegeven ticket.
        /// </summary>
        /// <param name="ticketnr"></param>
        /// <param name="registratie"></param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool SaveReparatieRegistratie(int ticketnr, ReparatieRegistratie registratie)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO reparatieregistratie (rrnr, mednr, ticketnr, datumtijd, melding, uren) VALUES (registratieseq.nextval, " +
                    registratie.Mednr + ", " + ticketnr + ", to_timestamp('" + registratie.Datum.Year.ToString() + "-" + registratie.Datum.Month + "-" + registratie.Datum.Day + " " + registratie.Datum.Hour + ":" + registratie.Datum.Minute + ":" + registratie.Datum.Second + "','YYYY-MM-DD HH24:MI:SS'), '" + registratie.Vermelding + "', " + registratie.AantalUren.ToString() + ")";
                OracleCommand command = new OracleCommand(query, connection);
                
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(rrnr) AS rrnr FROM reparatieregistratie";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                string rrnr = Convert.ToDecimal(reader["rrnr"]).ToString();

                foreach (VervangenOnderdeel vo in registratie.VervangenOnderdelen)
                {
                    string vervangenOnderdelenQuery = "INSERT INTO vervonderd (vervonderdnr, productnaam, kosten, rrnr) VALUES (VERVONDERDSEQ.nextval, '" + vo.Productnaam + "', " + vo.Kosten.ToString() + ", " + rrnr + ")";
                    command = new OracleCommand(vervangenOnderdelenQuery, connection);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Maakt een nieuwe medewerker aan met de meegegeven informatie.
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="password"></param>
        /// <param name="telefoon"></param>
        /// <param name="filliaal"></param>
        /// <param name="functienummer"></param>
        /// <returns>Returnt de medewerker als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Medewerker SaveMedewerker(string naam, string password, string telefoon, string filliaal, int functienummer)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO medewerker (mednr, naam, functie, afd, password, telnr) VALUES (medewerkerseq.nextval, " +
                    "'" + naam + "', " + functienummer.ToString() + ", '" + filliaal + "', '" + password + "', '" + telefoon + "')";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(mednr) AS mednr FROM medewerker";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();
                
                Medewerker m = new Medewerker(
                    Convert.ToInt32(reader["mednr"]),
                    naam,
                    password,
                    telefoon,
                    filliaal,
                    (MedewerkerFunctie)functienummer
                    );
            
                connection.Close();
                return m;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Maakt een nieuw product aan met de meegegeven informatie.
        /// </summary>
        /// <param name="serienummer"></param>
        /// <param name="klantnr"></param>
        /// <param name="garantieTijd"></param>
        /// <param name="omschrijving"></param>
        /// <param name="verkoopDatum"></param>
        /// <returns>Returnt het product als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Product SaveProduct(int serienummer, int klantnr, string garantieTijd, string omschrijving, DateTime verkoopDatum)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO product (serienummer, omschrijving, aankoop_date, garantie_duur, klantnr) VALUES "
                    + "(" + serienummer.ToString() + ", '" + omschrijving +
                    "', to_timestamp('" + verkoopDatum.Year.ToString() + "-" + verkoopDatum.Month + "-" + verkoopDatum.Day + " " + verkoopDatum.Hour + ":" + verkoopDatum.Minute + ":" + verkoopDatum.Second + "','YYYY-MM-DD HH24:MI:SS'), " +
                    garantieTijd +
                    ", " + klantnr.ToString() + ")";
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Max(serienummer) AS productnr FROM product";
                command = new OracleCommand(selectQuery, connection);
                OracleDataReader reader = command.ExecuteReader();
                reader.Read();

                Product p = new Product(
                    Convert.ToInt32(reader["productnr"]),
                    klantnr, 
                    garantieTijd, 
                    omschrijving, 
                    verkoopDatum
                    );
                return p;
            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Zoekt de medewerker horend bij het meegegeven gebruikersnaam + paswoord.
        /// </summary>
        /// <param name="gebruikersnaam"></param>
        /// <param name="pw"></param>
        /// <returns>Returnt de medewerker als het gelukt is. Returnt null als de gegevens niet kloppen of als er een fout is opgetreden met de database</returns>
        public Medewerker LogIn(int gebruikersnaam, string pw)
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM medewerker WHERE mednr = " + gebruikersnaam.ToString() +"AND password = '"+pw +"'";
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Medewerker m = null;    
                if (reader.Read())
                {
                    m = new Medewerker(
                        Convert.ToInt32(reader["mednr"]),
                        (string)reader["naam"],
                        (string)reader["password"],
                        (string)reader["telnr"],
                        (string)reader["afd"],
                        (MedewerkerFunctie)Convert.ToInt32(reader["functie"])
                        );
                }
                return m;

            }
            catch (OracleException e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Verwijderd de meegegeven klant.
        /// </summary>
        /// <param name="klantnr">Het klantnummer</param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool RemoveKlant(int klantnr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM klant WHERE klantnr = " + klantnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Verwijdert de meegegeven ticket.
        /// </summary>
        /// <param name="ticketnr">Het ticketnummer</param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool RemoveTicket(int ticketnr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM ticket WHERE ticketnr = " + ticketnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Verwijdert de meegegeven medewerker.
        /// </summary>
        /// <param name="mednr">Het medewerkernummer</param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool RemoveMedewerker(int mednr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM medewerker WHERE mednr = " + mednr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Verwijdert het meegegeven product.
        /// </summary>
        /// <param name="productnr">Het productnummer.</param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool RemoveProduct(int productnr)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM product WHERE serienr = " + productnr.ToString();
                OracleCommand command = new OracleCommand(query, connection);
                return command.ExecuteNonQuery() == 1;
            }
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Wijzigt de meegegeven klant in de database.
        /// </summary>
        /// <param name="klant">De nieuwe klant</param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool WijzigKlant(Klant klant)
        {
            try
            {
                connection.Open();
                string query = String.Format("UPDATE klant SET voornaam = '{0}', tussenvoegsel = '{1}', achternaam = '{2}', woonplaats = '{3}', straat = '{4}', huisnummer = {5}, postcode = '{6}', email = '{7}', telefoon = '{8}', land = '{9}' WHERE klantnr = '{10}'", klant.Voornaam, klant.Tussenvoegsel, klant.Achternaam, klant.Woonplaats, klant.Straatnaam, klant.Huisnummer.ToString(), klant.Postcode, klant.Email, klant.Telefoon, klant.Land, klant.Nr.ToString());
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (OracleException e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Wijzigt de meegegeven ticket in de database.
        /// </summary>
        /// <param name="ticket">De nieuwe ticket</param>
        /// <param name="serienr">Het serienummer</param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool WijzigTicket(Ticket ticket, int serienr)
        {
            try
            {
                connection.Open();
                String query = String.Format("UPDATE ticket SET status = {0}, opmerking = '{1}', locatie = '{2}', verw_oplostijd = {3}, klantnr = {4}, serienr = '{5}', categorie = '{6}' WHERE ticketnr = {7}", ((int)ticket.Status).ToString(), ticket.Probleem, ticket.AfdelingsAfkorting, ticket.VerwachteReparatieTijd.ToString(), ticket.Klantnr.ToString(), serienr.ToString(), ticket.Categorie, ticket.Ticketnr.ToString());
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (OracleException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
       
        /// <summary>
        /// Haalt de medewerker uit de database met het meegegeven nummer.
        /// </summary>
        /// <param name="medewerkernummer">Het medewerkernummer</param>
        /// <returns>Returnt de medewerker als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Medewerker GetMederwerkerByNummer(int medewerkernummer)
        {
            try
            {
                connection.Open();
                string query = String.Format("SELECT * FROM medewerker where mednr = '{0}'", medewerkernummer.ToString());
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Medewerker m = null;
                if (reader.Read())
                {
                    m = new Medewerker(
                        Convert.ToInt64(reader["mednr"]),
                        (string)reader["naam"],
                        (string)reader["password"],
                        (string)reader["telnr"],
                        (string)reader["afd"],
                        (MedewerkerFunctie)Convert.ToInt32(reader["functie"]));
                }
                return m;
            }
            catch (OracleException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }    
        }

        /// <summary>
        /// Haalt de medewerker uit de database met de meegegeven naam.
        /// </summary>
        /// <param name="medewerkernaam">De naam</param>
        /// <returns>Returnt de medewerker als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public Medewerker GetMedewerkerByNaam(string medewerkernaam)
        {
            try
            {
                connection.Open();
                string query = String.Format("SELECT * FROM medewerker where naam = '{0}'", medewerkernaam);
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Medewerker m = null;
                if (reader.Read())
                {
                    m = new Medewerker(
                        Convert.ToInt64(reader["mednr"]),
                        (string)reader["naam"],
                        (string)reader["password"],
                        (string)reader["telnr"],
                        (string)reader["afd"],
                        (MedewerkerFunctie)Convert.ToInt32(reader["functie"]));
                }
                return m;
            }
            catch (OracleException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Wijzigt de medewerker in de database.
        /// </summary>
        /// <param name="medewerker">De nieuwe medewerker</param>
        /// <returns>Returnt true als het gelukt is. Returnt false als er een fout is opgetreden.</returns>
        public bool WijzigMedewerker(Medewerker medewerker)
        {
            try
            {
                connection.Open();
                string query = String.Format("UPDATE medewerker SET functie = {0}, afd = '{1}', telnr = '{2}', password = '{3}' WHERE mednr = {4}", ((int)medewerker.Functie).ToString(), medewerker.Filiaal, medewerker.Telefoon, medewerker.Password, medewerker.Mednr.ToString());
                OracleCommand command = new OracleCommand(query, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (OracleException)
            { 
                return false;
            }
            finally { connection.Close(); }
        }

        /// <summary>
        /// Haalt de lijst van medewerkers uit de database.
        /// </summary>
        /// <returns>Returnt de lijst als het gelukt is. Returnt null als er een fout is opgetreden.</returns>
        public List<Medewerker> GetMedewerkers()
        {
            try
            {
                connection.Open();
                string query = String.Format("SELECT * FROM medewerker");
                OracleCommand command = new OracleCommand(query, connection);
                OracleDataReader reader = command.ExecuteReader();
                Medewerker m = null;
                List<Medewerker> medLijst = new List<Medewerker>();
                while (reader.Read())
                {
                    m = new Medewerker(
                        Convert.ToInt64(reader["mednr"]),
                        (string)reader["naam"],
                        (string)reader["password"],
                        (string)reader["telnr"],
                        (string)reader["afd"],
                        (MedewerkerFunctie)Convert.ToInt32(reader["functie"]));
                    medLijst.Add(m);
                }
                return medLijst;
            }
            catch (OracleException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }                
    }
}
