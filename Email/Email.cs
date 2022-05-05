using MailKit.Net.Smtp;
using MailKit.Security;
// using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Email
{
    public interface IEmailService
    {
        // could also specify the parameter to be MimeMessage rather than all these strings
        // instead creating the Message inside the calling app
        void SendEmail(string sender, string recipient, string subject, string body,
            string smtpHost, int smtpPort, string smtpUser, string smtpPass);
    }

    public class EmailService : IEmailService
    {

        public void SendEmail(string sender, string recipient, string subject, string body, 
            string smtpHost, int smtpPort, string smtpUser, string smtpPass)
        {
            // create message using MailKit 
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(sender));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = subject;

            email.Body = new TextPart(TextFormat.Plain) { Text = body };
            // alternative for html email:
            // email.Body = new TextPart(TextFormat.Html) { Text = body }; 

            // send email
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=net-6.0
            // ^^ handling transmission success/fail
            using var smtp = new SmtpClient();
            smtp.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(smtpUser, smtpPass);
            smtp.Send(email);
            
            // implement log + if it does not succeed implement recursive email:
            // if sucess or attempts > 2, then exit, else run Send() with the same parameters
            smtp.Disconnect(true);
        }
    }
}