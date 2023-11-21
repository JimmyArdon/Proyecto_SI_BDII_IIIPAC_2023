namespace WebApiPrestamos.Dtos
{
    public class EmailConfigurationDto
    {
        public string SmtServer { get; set; }
        public int SmtPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string FromName { get; set; }
        public string FromAddres { get; set; }
    }
}
