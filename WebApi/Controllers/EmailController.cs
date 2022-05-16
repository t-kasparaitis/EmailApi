using Email;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options; // this is where the fields & setting name is for pulling from appsettings.json
using WebApi.Models;
using WebApi.Options;


namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        // using options we are able to pull from appsettings.json instead of hardcoding
        private readonly EmailOptions _emailOptions;

        public EmailController(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        // POST /email
        [HttpPost]
        // data passed from front-end form must match EmailDetails model, or else it is rejected
        public async Task<IActionResult> Post([FromForm] EmailDetails model)
        {
            //• Email sender, recipient, subject, and body (not attachments), and date must be logged/stored
            //indefinitely with status of send attempt.

            // create new email & pass in parameters from model & appsettings:
            EmailService emailService = new EmailService();
            EmailPayload emailPayload = new EmailPayload
            {
                Sender = _emailOptions.Sender,
                Recipient = model.Recipient,
                Subject = model.Subject,
                Body = model.Body,
                SmtpHost = _emailOptions.SmtpHost,
                SmtpPort = _emailOptions.SmtpPort,
                SmtpUser = _emailOptions.SmtpUser,
                SmtpPass = _emailOptions.SmtpPass
            };

            // if attempts reaches 4 or we get "sending successful" we stop trying to transmit the email
            int attempts = 0;
            string status = String.Empty;
            while (attempts < 4 && status.Equals("sending successful") != true)
            {
                attempts++;
                status = await emailService.SendEmail(emailPayload);

                // log sender; recipient; subject; body; date(datetime? utcoffset?), status

            }

            // Note: if the amount of time to receive a response matters, we would need a callback function.
            // However, per HTTP responses 202 is for accepting a request and 200 is for it succeeding.
            // For failure we return 400 as request errors should be client-based if our service is up.
            if (status.Equals("sending successful")) 
            {
                return Ok(new { message = "E-mail sent to server" });
            }
            else 
            {
                return BadRequest(status);
            }
        }
    }
}
