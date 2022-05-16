using System.ComponentModel.DataAnnotations;

namespace WebApi.Data
{
    public class AttemptEntity
    {
        [Key]
        public Guid AttemptId { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public string Status { get; set; } = String.Empty;
        [Required]
        public string Sender { get; set; } = String.Empty;
        [Required]
        public string Recipient { get; set; } = String.Empty;
        [Required]
        public string Subject { get; set; } = String.Empty;
        [Required]
        public string Body { get; set; } = String.Empty;
    }
}