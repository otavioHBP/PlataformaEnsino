using System.Net;
using System.Net.Mail;

namespace Api1.Services
{
    public class EmailService
    {
        public bool Send(
            string toName,
            string toEmail,
            string subject,
            string body,
            string fromName = "Equipe Palhares.net",
            string fromEmail = "emailpalhares@gmail.com") 
        {
            var smtpClient = new SmtpClient(Configurations.Smtp.Host, Configurations.Smtp.Port);

            smtpClient.Credentials = new NetworkCredential(Configurations.Smtp.UserName, Configurations.Smtp.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail,toName));   
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;


            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}
