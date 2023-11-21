namespace WebApiPrestamos.Services
{
    public interface IEmailSenderServices
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}