using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
        
        public void OnGet()
        {
        }

        [Required]
        [EmailAddress]
        public string Recipient { get; set; } = String.Empty;
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Subject { get; set; } = String.Empty;
        [Required]
        [StringLength(10000, MinimumLength = 2)] // String Max Length here might be a consideration for abuse/data storage
        public string Body { get; set; } = String.Empty;

        // note with 1 server hosting front-end and 2nd server hosting front-end, this is in essence a 3rd party API call
        // trying to use AJAX would make the call result in redirection as the call is not from the same origin
        public IActionResult OnPostAsync()
        {
            // create a dictionary (unique key-value pairs) for our user e-mail input
            var content = new Dictionary<string, string>
            {
                {"recipient", Request.Form[nameof(Recipient)]},
                {"subject", Request.Form[nameof(Subject)]},
                {"body", Request.Form[nameof(Body)]}
            };

            // use Singleton-created http client to make 3rd party API call + convert dictionary of user input
            // PostAsync will run in the background, but we don't wait for results which allows return Page() to give the illusion of immediate response!
            _client.PostAsync("http://localhost:5000/Email", new FormUrlEncodedContent(content));
            
            // our API will log to the DB, however if the API is down info won't be captured - for such logging additional logic would be required

            return Page();
        }
    }
}