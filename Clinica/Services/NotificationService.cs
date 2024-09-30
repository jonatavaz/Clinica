// Services/NotificationService.cs
using System.Net;
using System.Net.Mail;

public class NotificationService
{
    public void SendEmail(string to, string subject, string body)
    {
        var fromAddress = new MailAddress("seu_email@example.com", "Clinica");
        var toAddress = new MailAddress(to);
        const string fromPassword = "sua_senha";

        using (var smtp = new SmtpClient
        {
            Host = "smtp.example.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        })
        {
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}

