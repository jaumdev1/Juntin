using System.Net;
using System.Net.Mail;
using Juntin.Application.Interfaces.Email;


public class SendEmail : ISendEmail
{
    public async Task Execute(string email, string subject, string message)
    {
        using var smtpClient = new SmtpClient("smtp.sendgrid.net")
        {
            Port = 587,
            Credentials = new NetworkCredential("apikey", ""),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("noreplyjuntin@gmail.com"),
            Subject = subject,
            Body = message,
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}