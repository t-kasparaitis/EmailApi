namespace WebApi.Options
{
    public class EmailOptions
    {
        public const string DefaultEmail = "DefaultEmail";

        public string SmtpHost { get; set; } = String.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = String.Empty;
        public string SmtpPass { get; set; } = String.Empty;
    }
}
