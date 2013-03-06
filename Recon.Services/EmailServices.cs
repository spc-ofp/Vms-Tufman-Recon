using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Recon.Services
{
    public class EmailServices
    {
        public void SendEmail(string subject, string content, string toAddress)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("no-reply@spc.int");
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Body = content;
            SmtpClient SmtpServer = new SmtpClient("mail.spc.external", 25);
            SmtpServer.SendAsync(mail, toAddress);
        }
    }
}
