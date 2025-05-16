namespace DEPI_GraduationProject.Services
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; } = null!;      // not "SmtpServer"
        public int SmtpPort { get; set; }                  // make sure it's int
        public string SmtpUser { get; set; } = null!;      // not "SenderEmail"
        public string SmtpPass { get; set; } = null!;      // not "SenderPassword"
        public string FromEmail { get; set; } = null!;     // not "SenderEmail"
    }
}
