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
        Task<string> SendEmail(string sender, string recipient, string subject, string body,
            string smtpHost, int smtpPort, string smtpUser, string smtpPass);
    }

    public class EmailService : IEmailService
    {

        public async Task<string> SendEmail(string sender, string recipient, string subject, string body, 
            string smtpHost, int smtpPort, string smtpUser, string smtpPass)
        {
            // create message using MailKit & SmtpClient for message transfer
            using (var email = new MimeMessage())
            using (var smtp = new SmtpClient())

            // Note: resource cleanup is managed by using keyword which is equivalent to try-catch-finally!
            // Stackoverflow seems to say that containing the logic in a block is necessary to account for unexpected exits
            {
                email.From.Add(MailboxAddress.Parse(sender));
                email.To.Add(MailboxAddress.Parse(recipient));
                email.Subject = subject;

                email.Body = new TextPart(TextFormat.Plain) { Text = body };
                // email.Body = new TextPart(TextFormat.Html) { Text = body }; // alternative for html e-mail

                smtp.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(smtpUser, smtpPass);

                // catch & log exceptions if there's an error, or return default status
                string status = "sending successful";
                try { await smtp.SendAsync(email); } 
                catch (Exception e) { status = e.Message; }
                smtp.Disconnect(true);
                return status;
            }
        }
    }
}