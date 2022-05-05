using Email;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models;
using WebApi.Options;


namespace WebApi.Controllers
{

    // GET /email
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailOptions _emailOptions;

        public EmailController(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        // CRUD operations on EF SQLite db that will have our e-mail core & time stamp/fail-pass info

        // POST /email/send
        [HttpPost("send")]
        // IActionResult explained: https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0
        public ActionResult<EmailDetails> PostEmail(EmailDetails model)
        {
            // Create an instance of the EmailService class from DLL and invoke SendEmail method:
            // TODO: think about resource cleanup, either using w/ IDisposable or try-finally block
            EmailService emailService = new EmailService();
            emailService.SendEmail(
                // TODO: pull values as user input from front-end:
                "tkasparaitis@spoofed.com", // sender
                "leah@company.com", // recipient
                "Purchase Order #102832", // e-mail subject
                @"Hi Leah,

Thank you for the purchase of our product.

If you have any questions or concerns please feel free to reach out to our customer support at support@spoofed.com.

Kind Regards,
Team", // e-mail body
       // TODO: pull values from appsettings rather than hardcoding:
                _emailOptions.SmtpHost,
                _emailOptions.SmtpPort,
                _emailOptions.SmtpUser,
                _emailOptions.SmtpPass);
                //"smtp.mailtrap.io", // smtpHost url from mailtrap.io
                //2525, //smtpPort number - only a few ports but they vary by provider
                //"eb21bf2238088a", // smtp user name/login
                //"39ccdc35823c5f"); // smtp password
            return Ok(new {message = "E-mail sent"}); // change to appropriate response later, account for errors
        }
    }
}
