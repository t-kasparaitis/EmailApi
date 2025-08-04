using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Models;

namespace FrontEnd.Pages
{
    public class ContactCustomerModel : PageModel
    {
        private HttpClient _client;
        private readonly ILogger<ContactCustomerModel> _logger;
        public ContactCustomerModel(HttpClient client, ILogger<ContactCustomerModel> logger)
        {
            _client = client;
            _logger = logger;
        }
        public ContactCustomerForm ContactCustomerForm { get; set; }
        
        public void OnGet()
        {
        }

        // note with 1 server hosting front-end and 2nd server hosting front-end, this is in essence a 3rd party API call
        // trying to use AJAX would make the call result in redirection as the call is not from the same origin
        // our API will log to the DB, however if the API is down info won't be captured - for such logging additional logic would be required
        public IActionResult OnPostAsync(ContactCustomerForm contactCustomerForm)
        {
            if (ModelState.IsValid) 
            {
                // create a dictionary (unique key-value pairs) for our user e-mail input
                var content = new Dictionary<string, string>
                {
                    {"recipient", contactCustomerForm.Recipient},
                    {"subject", contactCustomerForm.Subject},
                    {"body", contactCustomerForm.Body}
                };

                // use Singleton-created http client to make 3rd party API call + convert dictionary of user input
                // PostAsync will run in the background, but we don't wait for results which allows return Page() to give the illusion of immediate response!
                _client.PostAsync("http://localhost:5000/Email", new FormUrlEncodedContent(content));

                return RedirectToPage();
            }

            return Page();
        }
    }
}