namespace Connectify.src.Application.Services
{
    public interface ISendEmailService
    {
        Task SendConfirmationCode(string to, string code);
    }
}