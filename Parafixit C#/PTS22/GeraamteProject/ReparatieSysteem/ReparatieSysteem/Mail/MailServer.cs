using System;
using System.Net.Sockets;
using System.IO;
using System.Net.Mail;

namespace ReparatieSysteem
{
    /// <summary>
    /// Deze klasse verzorgt het verzenden van emails.
    /// In de constructor kunnen het ip-adres, de poort en de timeoutwaarde (seconden) ingesteld worden.
    /// Gebruik deze klasse door een MailServer object aan te maken en de methode SendMail(new MailMessage(afzender, ontvanger, onderwerp, emailbody)) aan te roepen.
    /// Deze methode returned true als het verzenden lukt, en anders false.
    /// 
    /// Om deze klasse te gebruken moet je dit bovenaan de klasse zetten die hem gebruikt:
    /// using System.Net.Mail;
    /// en de namespace hierboven veranderen naar die van jouw.
    /// </summary>
    public class MailServer : IMailServer
    {
        // vaste variabelen
        private string mvarSMTPServerAddress;
        private int mvarSMTPTimeOut;
        private int mvarSMTPRemotePort;

        // benodigde services
        private TcpClient tclSMTP;
        private NetworkStream nstSMTP;
        private StreamReader strSMTP;
        private StreamWriter stwSMTP;
        private DateTime dteTimeOutCheck;

        // constructor
        public MailServer()
        {
            mvarSMTPServerAddress = "192.168.1.138";
            mvarSMTPTimeOut = 60;
            mvarSMTPRemotePort = 25;
        }

        // klasse die het mailen doet
        public bool SendMail(MailMessage message)
        {
            // verbinden met de smtp server
            tclSMTP = new TcpClient();
            try
            {
                tclSMTP.Connect(mvarSMTPServerAddress, mvarSMTPRemotePort);
            }
            catch
            {
                return false;
            }
            nstSMTP = tclSMTP.GetStream();
            stwSMTP = new StreamWriter(nstSMTP);
            strSMTP = new StreamReader(nstSMTP);

            // wachten op een respons van de server
            if (WaitForResponse("220"))
            {
                // bij een respons van de server, de server begroeten
                stwSMTP.WriteLine("HELO " + mvarSMTPServerAddress);
                stwSMTP.Flush();
            }
            else
            {
                tclSMTP.Close();
                return false;
            }

            // wachten op een respons van de server
            if (WaitForResponse("250"))
            {
                // bij een respons van de server de afzender sturen naar de server
                stwSMTP.WriteLine("mail from:" + message.From.ToString());
                stwSMTP.Flush();
            }
            else
            {
                tclSMTP.Close();
                return false;
            }

            // wachten op een respons van de server
            if (WaitForResponse("250"))
            {
                // bij een respons van de server de ontvanger sturen naar de server
                stwSMTP.WriteLine("rcpt to:" + message.To[0].ToString());
                stwSMTP.Flush();
            }
            else
            {
                tclSMTP.Close();
                return false;
            }

            // wachten op een respons van de server
            if (WaitForResponse("250"))
            {
                // bij een respons tegen de server zeggen dat er data gaat komen
                stwSMTP.WriteLine("data");
                stwSMTP.Flush();
            }
            else
            {
                tclSMTP.Close();
                return false;
            }

            // wachten op een respons van de server
            if (WaitForResponse("354"))
            {
                // data klaarzetten voor verzenden
                string strSMTPData = "";
                strSMTPData = "From:" + message.From + Environment.NewLine;
                strSMTPData += "To:" + message.To + Environment.NewLine;
                strSMTPData += "Subject:" + message.Subject + Environment.NewLine + Environment.NewLine;
                strSMTPData += message.Body + Environment.NewLine + "." + Environment.NewLine;
                // data verzenden
                stwSMTP.Write(strSMTPData);
                stwSMTP.Flush();
            }
            else
            {
                tclSMTP.Close();
                return false;
            }

            // wachten op een respons van de server
            if (WaitForResponse("250"))
            {
                //returns true if the send process succeeds
                tclSMTP.Close();
                return true;
            }
            else
            {
                tclSMTP.Close();
                return false;
            }
            // als er iets fout gaat, false returnen
        }

        // deze methode wacht op een respons van de server
        private bool WaitForResponse(string strResponseCode)
        {
            // huidige tijd opslaan
            dteTimeOutCheck = DateTime.Now;
            // de timespan opslaan, dat is hoe lang we al gewacht hebben op een respons
            TimeSpan tsp = DateTime.Now - dteTimeOutCheck;
            // deze code loopen totdat de timespan groter is dan de timeout
            while (tsp.Seconds < mvarSMTPTimeOut)
            {
                // kijken of de server een bericht naar ons verzonden heft
                if (nstSMTP.DataAvailable)
                {
                    // als dat zo is, pak dat bericht
                    string strIncomingData = strSMTP.ReadLine();
                    // kijken of de responscode van de server overeenkomt met degene die we verwachten
                    if (strIncomingData.Substring(0, strResponseCode.Length) == strResponseCode)
                        return true;
                }
                // de timespan herbereken
                tsp = DateTime.Now - dteTimeOutCheck;
            }
            // de timeout is verlopen, false returnen
            return false;
        }
    }
}
