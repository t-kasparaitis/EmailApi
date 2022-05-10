using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FrontEnd.Pages
{
    public class ContactUsModel : PageModel
    {
        private HttpClient _client;
        private readonly ILogger<ContactUsModel> _logger;
        public ContactUsModel(HttpClient client, ILogger<ContactUsModel> logger)
        {
            _client = client;
            _logger = logger;
        }
        
        public void OnGet()
        {
        }

        public string Sender { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string Body { get; set; } = String.Empty;

        // run an asynchronous post to avoid redirection
        // note with 1 server hosting front-end and 2nd server hosting front-end, this is in essence a 3rd party API call
        // trying to use AJAX would make the call result in redirection as the call is not from the same origin
        public async Task<IActionResult> OnPostAsync()
        {
            // create a dictionary (unique key-value pairs) for our user e-mail input
            var content = new Dictionary<string, string>
            {
                {"sender", Request.Form[nameof(Sender)]},
                {"subject", Request.Form[nameof(Subject)]},
                {"body", Request.Form[nameof(Body)]}
            };

            // use Singleton-created http client to make 3rd party API call + convert dictionary of user input
            await _client.PostAsync("http://localhost:5000/Email/send", new FormUrlEncodedContent(content));

            // our API will log to the DB, however if the API is down info won't be captured - for such logging additional logic would be required

            return Page();
        }

    }
}