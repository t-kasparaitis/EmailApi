using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Email
{
    
    public interface IEmailService
    {
        Task<string> SendEmail(EmailPayload email);
    }

    // Email objects allow us to resend the same e-mail easily, or even future proof to
    // create a variety of e-mails even with differents hosts etc. and send them from a queue
    public class EmailPayload
    {
        public string Sender { get; set; } = String.Empty;
        public string Recipient { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string Body { get; set; } = String.Empty;
        public string SmtpHost { get; set; } = String.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = String.Empty;
        public string SmtpPass { get; set; } = String.Empty;

    }

    public class EmailService : IEmailService
    {

        public async Task<string> SendEmail(EmailPayload payload)
        {
            // create message using MailKit & SmtpClient for message transfer
            using (var message = new MimeMessage())
            using (var smtp = new SmtpClient())

            // Note: resource cleanup is managed by using keyword which is equivalent to try-catch-finally!
            // Stackoverflow seems to say that containing the logic in a block is necessary to account for unexpected exits
            {
                message.From.Add(MailboxAddress.Parse(payload.Sender));
                message.To.Add(MailboxAddress.Parse(payload.Recipient));
                message.Subject = payload.Subject;

                message.Body = new TextPart(TextFormat.Plain) { Text = payload.Body };
                // email.Body = new TextPart(TextFormat.Html) { Text = body }; // alternative for html e-mail

                smtp.Connect(payload.SmtpHost, payload.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(payload.SmtpUser, payload.SmtpPass);

                // catch & log exceptions if there's an error, or return default status
                string status = "sending successful";
                try { await smtp.SendAsync(message); }
                catch (Exception e) { status = e.Message; }
                smtp.Disconnect(true);
                return status;
            }
        }
    }
}