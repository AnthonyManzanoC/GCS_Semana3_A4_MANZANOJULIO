using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DonJulioSuper.Utilities
{
    public static class EmailHelper
    {
        public static void SendEmail(string emailDestino, string subject, string body)
        {
            const string EmailOrigen = "erickfloressm17@gmail.com";
            string EmailPassword = "zcza ymzx cizl ghww"; // Tu contraseña o app password

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential(EmailOrigen, EmailPassword);
                client.EnableSsl = true;
                MailMessage mail = new MailMessage(EmailOrigen, emailDestino, subject, body);
                client.Send(mail);
            }
        }
    }
}