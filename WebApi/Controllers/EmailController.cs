﻿using Email;
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
        [HttpPost("send")]
        // data passed from front-end form must match EmailDetails model, or else it is rejected
        public IActionResult Post([FromForm] EmailDetails model)
        {
            //• Email sender, recipient, subject, and body (not attachments), and date must be logged/stored
            //indefinitely with status of send attempt.
            //• If email fails to send it should either be retried until success or a max of 3 times whichever
            //comes first, and can be sent in succession or over a period of time.
            // TODO: think about resource cleanup, either using w/ IDisposable or try-finally block
            EmailService emailService = new EmailService();
            emailService.SendEmail(
                model.Sender,
                _emailOptions.Recipient,
                model.Subject,
                model.Body,
                _emailOptions.SmtpHost,
                _emailOptions.SmtpPort,
                _emailOptions.SmtpUser,
                _emailOptions.SmtpPass);
            return Ok(new {message = "E-mail sent"}); // change to appropriate response later, account for errors
        }
    }
}
