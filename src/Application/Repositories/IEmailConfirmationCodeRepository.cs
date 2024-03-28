using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface IEmailConfirmationCodeRepository
    {
        Task<EmailConfirmationCode?> GetByEmail(string email);
        Task Upsert(EmailConfirmationCode confirmationCode);
        Task Delete(string email);
    }
}