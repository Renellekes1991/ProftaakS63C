using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace ReparatieSysteem
{
    public interface IMailServer
    {
        bool SendMail(MailMessage message);
    }
}
