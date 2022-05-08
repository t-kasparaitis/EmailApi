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
        // Model Binding explained: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-6.0
        // public ActionResult<EmailDetails> PostEmail([FromForm] EmailDetails model)
        public IActionResult Post([FromForm] EmailDetails model)
        {
            // This is clearly wrong, but the premise is that the front end info is passed to a setter for the model
            // after which the model's info is passed  to the SendEmail method?
            //EmailDetails details = new EmailDetails();
            //details.Sender = model.Sender;
            // Create an instance of the EmailService class from DLL and invoke SendEmail method:
            // TODO: think about resource cleanup, either using w/ IDisposable or try-finally block
            EmailService emailService = new EmailService();
            emailService.SendEmail(
                model.Sender,
                _emailOptions.Recipient, // all e-mails go to the same inbox, pulled from settings
                model.Subject,
                model.Body,
//                // TODO: pull values as user input from front-end:
//                "tkasparaitis@spoofed.com", // sender
//                "leah@company.com", // recipient
//                "Purchase Order #102832", // e-mail subject
//                @"Hi Leah,

//Thank you for the purchase of our product.

//If you have any questions or concerns please feel free to reach out to our customer support at support@spoofed.com.

//Kind Regards,
//Team", // e-mail body
                _emailOptions.SmtpHost,
                _emailOptions.SmtpPort,
                _emailOptions.SmtpUser,
                _emailOptions.SmtpPass);
            return Ok(new {message = "E-mail sent"}); // change to appropriate response later, account for errors
        }
    }
}
