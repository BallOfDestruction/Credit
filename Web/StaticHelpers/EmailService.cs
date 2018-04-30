using System.Net;
using System.Net.Mail;

namespace Web.StaticHelpers
{
    public class EmailService
    {
        public static void Send(EmailModel emailModel)
        {
            var client = new SmtpClient("mail.zmk-armada.ru", 25);
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential("support@zmk-armada.ru", "Xfgl65*8");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailModel.From);
            mailMessage.To.Add(emailModel.To);
            mailMessage.Body = emailModel.Body;
            mailMessage.Subject = emailModel.Subject;
            client.Send(mailMessage);
        }
    }
}
