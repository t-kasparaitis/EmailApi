using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ContactCustomerForm
    {
        [Required]
        [EmailAddress]
        public string Recipient { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = false)]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "A subject is required.")]
        public string Subject { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = false)]
        [StringLength(10000, MinimumLength = 2, ErrorMessage = "A message is required.")] // String Max Length here might be a consideration for abuse/data storage
        public string Body { get; set; } = String.Empty;
    }
}
