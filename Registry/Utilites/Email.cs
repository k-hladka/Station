using Google.Apis.Auth.OAuth2;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;
namespace Registry.Utilites
{
    public class Email
    {
        private string from = "a7516518@gmail.com";
        private string password = "vwzj tkgd pavd umpc";
        public Email(string to, string header, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(this.from);
                mail.To.Add(to);
                mail.Subject = header;
                mail.Body = body;
                mail.IsBodyHtml = true;
                

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(this.from, this.password);
                    smtp.Send(mail);
                }
            }
        }
    }
}
