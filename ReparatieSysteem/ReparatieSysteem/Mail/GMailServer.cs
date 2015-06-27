using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ReparatieSysteem.Mail
{
    class GMailServer : IMailServer
    {
        SmtpClient smtp;

        public GMailServer()
        {
            smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("fontyspts7gserver@gmail.com", "swpts7control");
        }

        public bool SendMail(MailMessage message)
        {
            try
            {
                smtp.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
