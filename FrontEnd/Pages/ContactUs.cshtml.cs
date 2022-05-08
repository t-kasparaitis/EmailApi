using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages
{
    public class ContactUsModel : PageModel
    {
        private readonly ILogger<ContactUsModel> _logger;

        public ContactUsModel(ILogger<ContactUsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }


        // This should fix the task running in the background so user can move around?
        // If not can use ajax to send the request
        public IActionResult OnPostSubmit()
        {
            return Page();
        }
    }
}