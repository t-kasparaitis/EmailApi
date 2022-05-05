namespace EmailWebAPi
{
    public class Email
    {
        public string sender { get; set; }
        public string recipient { get; set; }

        public string subject { get; set; }
        public string body { get; set; }
        public string smtpHost { get; set; }
        public int smtpPort { get; set; }
        public string smtpUser { get; set; }
        public string smtpPass { get; set; }
    }
}
